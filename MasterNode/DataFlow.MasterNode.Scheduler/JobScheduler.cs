using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using AISTek.DataFlow.MasterNode.DataModel;
using AISTek.DataFlow.MasterNode.Scheduler.JobBuilder;
using AISTek.DataFlow.Util;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.Unity;

namespace AISTek.DataFlow.MasterNode.Scheduler
{
    /// <summary>
    /// Job scheduler class.
    /// Manages <see cref="Job"/>s.
    /// </summary>
    public class JobScheduler : IJobScheduler
    {
        #region Constructor

        /// <summary>
        /// Initializes new instance of  <see cref="IJobScheduler"/> class.
        /// </summary>
        /// <param name="taskQueue">
        /// Injection parameter - an instance of <see cref="ITaskQueue"/>
        /// </param>
        /// <param name="jobBuildingPipeline">
        /// Injection parameter - an instance of <see cref="IJobBuildingPipeline"/>
        /// </param>
        [InjectionConstructor]
        public JobScheduler(
            [Dependency]
            ITaskQueue taskQueue,
            [Dependency]
            IJobBuildingPipeline jobBuildingPipeline)
        {
            Contract.Requires(taskQueue != null);
            Contract.Requires(jobBuildingPipeline != null);
            Contract.Ensures(this.taskQueue != null);
            Contract.Ensures(this.jobBuildingPipeline != null);

            this.taskQueue = taskQueue;
            this.jobBuildingPipeline = jobBuildingPipeline;
        }

        #endregion

        #region Implementation of IJobScheduler

        #region Methods

        /// <summary>
        /// Creates new instance of <see cref="Job"/>.
        /// </summary>
        /// <param name="name">
        /// Job's name.
        /// </param>
        /// <returns>
        /// New instance of <see cref="Job"/>.
        /// </returns>
        public Job CreateJob(string name)
        {
            var job = new Job(name) { Id = Guid.NewGuid(), State = JobState.Creating };
            jobs.Add(job);

            return job;
        }

        /// <summary>
        /// Registers newly created job in manager and sets job's UID.
        /// </summary>
        /// <param name="job">
        /// An instance of <see cref="Job"/>. 
        /// </param>
        public void RegisterJob(Job job)
        {
            Logger.Write(
                   LogCategories.Information(string.Format("Job {0} is created.", job),
                       LogCategories.TaskServices,
                       LogCategories.JobScheduler));

            foreach (var task in job.Tasks)
            {
                taskQueue.RegisterTask(task);
            }

            job.State = JobState.Ready;
        }

        /// <summary>
        /// Starts executin of specified job.
        /// </summary>
        /// <param name="job">
        /// An instance of <see cref="Job"/>. 
        /// </param>
        public void StartJob(Job job)
        {
            Logger.Write(
                   LogCategories.Information(string.Format("Job {0} started.", job),
                       LogCategories.TaskServices,
                       LogCategories.JobScheduler));

            // Subscribe to current job's events.
            SubscribeToJob(job);

            // Build the current job.
            jobBuildingPipeline.Build(job);

            foreach (var task in job.Tasks)
            {
                taskQueue.StartTask(task);
            }

            job.State = JobState.Processing;
        }

        /// <summary>
        /// Cancels execution of specified job.
        /// </summary>
        /// <param name="job">
        /// An instance of <see cref="Job"/>. 
        /// </param>
        public void CancelJob(Job job)
        {
            UnsubscribeFromJob(job);
            foreach (var task in job.Tasks)
            {
                taskQueue.CancelTask(task);
            }

            job.State = JobState.Cancelled;
        }

        /// <summary>
        /// Reinitializes cancelled job.
        /// </summary>
        /// <param name="job">
        /// An instance of <see cref="Job"/>. 
        /// </param>
        public void RestartJob(Job job)
        {
            job.InitJobState();
            foreach (var task in job.Tasks)
            {
                taskQueue.RestartTask(task);
            }

            job.State = JobState.Ready;
        }

        /// <summary>
        /// Removes job from scheduler.
        /// </summary>
        /// <param name="job">
        /// An instance of <see cref="Job"/>. 
        /// </param>
        public void DeleteJob(Job job)
        {
            foreach (var task in job.Tasks)
            {
                taskQueue.DeleteTask(task);
            }

            jobs.Remove(job);
            job.State = JobState.Deleted;
        }

        #endregion

        #region Job quering

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> of <see cref="Job"/>s which contains all registered jobs and their data.
        /// </summary>
        public IEnumerable<Job> Jobs { get { return jobs; } }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when a job is completed.
        /// </summary>
        public event JobEventHandler OnJobCompleted;

        /// <summary>
        /// Occurs when job's execution fails.
        /// </summary>
        public event JobEventHandler OnJobFailed;

        #region Event invokers

        private void InvokeOnJobCompleted(Job job)
        {
            var handler = OnJobCompleted;
            if (handler != null)
                handler(this, new JobEventArgs(job));
        }

        private void InvokeOnJobFailed(Job job)
        {
            var handler = OnJobFailed;
            if (handler != null)
                handler(this, new JobEventArgs(job));
        }

        #endregion

        #endregion

        #endregion

        #region Private fields

        private readonly ITaskQueue taskQueue;
        private readonly IJobBuildingPipeline jobBuildingPipeline;

        private readonly IList<Job> jobs = new List<Job>();

        #endregion

        #region Private methods

        #region Event handlers

        private void SubscribeToJob(Job job)
        {
            job.OnJobCompleted += Job_OnJobCompleted;
            job.OnJobFailed += Job_OnJobFailed;
        }

        private void UnsubscribeFromJob(Job job)
        {
            try
            {
                job.OnJobCompleted -= Job_OnJobCompleted;
            }
            catch (Exception e)
            {
                Logger.Write(
                   LogCategories.Error(string.Format("UnsubscribeFromJob({0}):\n{1}", job, e),
                       LogCategories.TaskServices,
                       LogCategories.JobScheduler));
            }
        }

        private void Job_OnJobCompleted(object sender, EventArgs e)
        {
            var job = sender as Job;
            InvokeOnJobCompleted(job);
            UnsubscribeFromJob(job);
        }

        private void Job_OnJobFailed(object sender, EventArgs e)
        {
            var job = sender as Job;
            InvokeOnJobFailed(job);
            UnsubscribeFromJob(job);
        }

        #endregion

        #endregion
    }
}
