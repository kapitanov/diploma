using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using AISTek.DataFlow.MasterNode.DataModel;
using AISTek.DataFlow.Shared.Classes;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.Unity;
using Dto = AISTek.DataFlow.Shared.Classes;
using JobState = AISTek.DataFlow.MasterNode.DataModel.JobState;

namespace AISTek.DataFlow.MasterNode.Scheduler
{
    /// <summary>
    /// Implements service with contract <see cref="Dto.IJobManager"/>.
    /// </summary>
    [ServiceBehavior(
        ConfigurationName = Dto.Namespaces.Scheduler.JobManager.Configuration,
        InstanceContextMode = InstanceContextMode.PerSession,
        AutomaticSessionShutdown = true,
        IncludeExceptionDetailInFaults = true)]
    public class JobManager : Dto.IJobManager
    {
        #region Constructor

        /// <summary>
        /// Initializes new instance of <see cref="JobManager"/> class.
        /// </summary>
        /// <param name="scheduler">
        /// Injection parameter - internal service <see cref="IJobScheduler"/>
        /// </param>
        [InjectionConstructor]
        public JobManager(
            [Dependency]
            IJobScheduler scheduler)
        {
            this.scheduler = scheduler;
            var operationContext = OperationContext.Current;
            operationContext.GetCallbackChannel<Dto.IJobManagerCallback>();
            //var clientEndpoint = operationContext.IncomingMessageProperties[RemoteEndpointMessageProperty.Name]
            //                                  as RemoteEndpointMessageProperty;
            //remoteAddress = string.Format(
            //    "{0}:{1}",
            //    clientEndpoint.Address, clientEndpoint.Port);

            operationContext.Channel.Closed += OnChannelClosed;
            operationContext.Channel.Closing += OnChannelClosing;
        }

        #endregion

        #region Implementation of IJobManager

        /// <summary>
        /// Creates a job with specified name and sets it as current.
        /// </summary>
        /// <param name="name">
        /// Job's name.
        /// </param>
        /// <returns>
        /// An instance of <see cref="DataModel.Job"/>.
        /// </returns>
        public Dto.Job CreateJob(string name)
        {
            currentJob = scheduler.CreateJob(name);
            return currentJob.ToContract();
        }

        /// <summary>
        /// Opens an existing job and sets it as current.
        /// </summary>
        /// <param name="id">
        /// Job's UID.
        /// </param>
        public void OpenJob(Guid id)
        {
            var job = scheduler.Jobs.Where(j => j.Id == id).Single();
            currentJob = job;
        }

        /// <summary>
        /// Creates a task in current job.
        /// </summary>
        /// <param name="task">
        /// An instance of <see cref="DataModel.Task"/> which defines the task to create.
        /// </param>
        /// <returns>
        /// An unique identifier for created task.
        /// </returns>
        public Guid CreateTask(Dto.Task task)
        {
            var t = task.FromContract(currentJob.Tasks);
            t.Id = Guid.NewGuid();
            t.Job = currentJob;
            currentJob.Tasks.Add(t);

            return t.Id;
        }

        /// <summary>
        /// Updates the created task with UID = <paramref name="id"/> in the current job. 
        /// </summary>
        /// <param name="id">
        /// Task's UID.
        /// </param>
        /// <param name="task">
        /// Task's updated definition.
        /// </param>
        public void UpdateTask(Guid id, Dto.Task task)
        {
            if (currentJob.State == JobState.Creating ||
               currentJob.State == JobState.Ready ||
               currentJob.State == JobState.Completed ||
               currentJob.State == JobState.Failed)
            {

                var updatedTask = task.FromContract(currentJob.Tasks);
                updatedTask.Id = id;
                lock (currentJob)
                {
                    currentJob.Tasks.Remove(currentJob.Tasks.Where(t => t.Id == id).Single());
                    currentJob.Tasks.Add(updatedTask);
                }
            }
            else
            {
                throw new InvalidOperationException("currentJob is in wrong state.");
            }
        }

        /// <summary>
        /// Removes the task from current job.
        /// </summary>
        /// <param name="id">
        /// Task's UID.
        /// </param>
        public void RemoveTask(Guid id)
        {
            if (currentJob == null)
                throw new NullReferenceException("currentJob is not set.");

            if (currentJob.State == JobState.Creating ||
               currentJob.State == JobState.Ready ||
               currentJob.State == JobState.Completed ||
               currentJob.State == JobState.Failed)
            {
                lock (currentJob)
                {
                    var task = currentJob.Tasks.Where(t => t.Id == id).Single();
                    currentJob.Tasks.Remove(task);
                }
            }
            else
            {
                throw new InvalidOperationException("currentJob is in wrong state.");
            }
        }

        /// <summary>
        /// Starts execution of the current job.
        /// </summary>
        public void StartJob()
        {
            scheduler.RegisterJob(currentJob);
            scheduler.StartJob(currentJob);
        }

        /// <summary>
        /// Cancels execution of the current job.
        /// </summary>
        public void CancelJob()
        {
            scheduler.CancelJob(currentJob);
        }

        /// <summary>
        /// Reinitializes current job after cancelling, allowing to start it again.
        /// </summary>
        public void RestartJob()
        {
            scheduler.RestartJob(currentJob);
        }

        /// <summary>
        /// Deletes current job from scheduler. Job must not be in state "Processing".
        /// </summary>
        public void DeleteJob()
        {
            scheduler.DeleteJob(currentJob);
            currentJob = null;
        }

        /// <summary>
        /// Finds all jobs in scheduler which are not being processed and have specified name.
        /// </summary>
        /// <param name="name">
        /// Job's name to match.
        /// </param>
        /// <returns>
        /// List of all matching jobs.
        /// </returns>
        public IEnumerable<Dto.Job> FindJobs(string name)
        {
            return scheduler.Jobs.Where(
                job => job.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                .Select(job => job.ToContract())
                .ToList();
        }

        /// <summary>
        /// Get list of all jobs in scheduler.
        /// </summary>
        /// <returns>
        /// List of all jobs in scheduler.
        /// </returns>
        public IEnumerable<Dto.Job> GetAllJobs()
        {
            return scheduler.Jobs.Select(job => job.ToContract()).ToList();
        }

        /// <summary>
        /// Gets an instance of <see cref="DataModel.Job"/> which describes current job.
        /// </summary>
        public Dto.Job CurrentJob
        {
            get { return (currentJob != null) ? currentJob.ToContract() : null; }
        }

        /// <summary>
        /// Gets a state of the the job.
        /// </summary>
        /// <param name="jobId">
        /// Unique identifier of a job.
        /// </param>
        /// <returns>
        /// A value of <see cref="JobState"/> that contains the state of the job.
        /// If the job with specified GUID doesn't exists, then a value of <see cref="Dto.JobState.NotExists"/> is returned.
        /// </returns>
        public Dto.JobState QueryJobState(Guid jobId)
        {
            var job = (from j in scheduler.Jobs
                       where j.Id == jobId
                       select j).FirstOrDefault();
            if (job != null)
                return (Dto.JobState)((int)job.State);

            return Dto.JobState.NotExists;
        }

        /// <summary>
        /// Gets error report for failed job.
        /// </summary>
        /// <param name="jobId">
        /// Unique identifier of a job.
        /// </param>
        /// <returns>
        /// An instance of <see cref="ErrorSummary"/> class.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// This exception is thrown if the specified job is not in "failed" state.
        /// </exception>
        public ErrorSummary GetErrorReport(Guid jobId)
        {
            var job = scheduler.Jobs.Where(j => j.Id == jobId).First();
            if (job.State != JobState.Failed)
                throw new InvalidOperationException(
                    string.Format("Job {{{0}}} is in state \"{1}\" but it's excepted to be in \"Failed\" state.", jobId,
                                  job.State));
            lock (job)
            {
                var errors = from t in job.Tasks
                             where t.State == TaskState.Failed
                             where t.Error != null
                             select t.Error;
                return new ErrorSummary(job.ToContract(), errors);
            }
        }

        #endregion

        #region Private fields

        private DataModel.Job currentJob;

        private readonly IJobScheduler scheduler;

        #endregion

        #region Private methods

        #region Event handlers

        private void OnChannelClosed(object sender, EventArgs e)
        {
            Logger.Write(LogCategories.Information(string.Format("Channel closed to {0}", remoteAddress), LogCategories.TaskServices, LogCategories.JobManagerService));
        }

        private void OnChannelClosing(object sender, EventArgs e)
        {
            Logger.Write(LogCategories.Information(string.Format("Channel closing to {0}", remoteAddress), LogCategories.TaskServices, LogCategories.JobManagerService));
        }

        private string remoteAddress = null;

        #endregion

        #endregion
    }
}
