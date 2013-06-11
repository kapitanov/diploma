using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using AISTek.DataFlow.Util;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace AISTek.DataFlow.MasterNode.DataModel
{
    /// <summary>
    /// Represents a job as a set of tasks.
    /// </summary>
    public sealed class Job
    {
        #region Constructors

        /// <summary>
        /// Initializes new instance of <see cref="Job"/>
        /// </summary>
        public Job()
            : this(string.Empty)
        { }

        /// <summary>
        /// Initializes new instance of <see cref="Job"/>
        /// </summary>
        /// <param name="name">
        /// Job's name.
        /// </param> 
        public Job(string name)
        {
            Name = name;
            Tasks = new List<Task>();
            State = JobState.Creating;
        }

        /// <summary>
        /// Initializes new instance of <see cref="Job"/>
        /// </summary>
        /// <param name="name">
        /// Job's name.
        /// </param>
        /// <param name="tasks">
        /// A list of all tasks which belongs to current job.
        /// </param> 
        public Job(string name, IEnumerable<Task> tasks)
        {
            Name = name;
            Tasks = new List<Task>(tasks);
            State = JobState.Creating;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets and sets an unique identifier of job.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Job's friendly name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// List of all tasks in current job.
        /// </summary>
        public IList<Task> Tasks { get; private set; }

        /// <summary>
        /// Gets and sets task's state.
        /// </summary>
        public JobState State { get; set; }

        #endregion

        #region Events

        /// <summary>
        /// This event is raised when current job is completed.
        /// </summary>
        public event EventHandler OnJobCompleted;

        /// <summary>
        /// This event is raised when current job is completed.
        /// </summary>
        public event EventHandler OnJobFailed;

        #endregion

        #region Public methods

        /// <summary>
        /// Gets an instance of <see cref="T:System.String"/> which represents current instance of <see cref="Job"/>.
        /// </summary>
        /// <returns>
        /// An instance of <see cref="T:System.String"/> which represents current instance of <see cref="Job"/>.
        /// </returns>
        public override string ToString()
        {
            return string.Format("(\"{0}\", {{{1}}})", Name, Id);
        }

        /// <summary>
        /// Initializes job's internal state before its tasks will be queued for execution.
        /// </summary>
        public void InitJobState()
        {
            Logger.Write(LogCategories.Information(string.Format("Initializing job {0}", this), LogCategories.TaskServices));
            lock (this)
            {
                incompletedTasksCount = (uint) Tasks.Count;
            }
        }

        /// <summary>
        /// Notifies job that <paramref name="task"/> has been successfully executed.
        /// </summary>
        /// <param name="task">
        /// Task that has been executed.
        /// </param>
        public void NotifyTaskCompleted(Task task)
        {
            Contract.Requires(task != null);

            Logger.Write(LogCategories.Information(string.Format("Task {0} in job {1} completed.", task, this), LogCategories.TaskServices));
            
            lock (this)
            {
                incompletedTasksCount--;
                if (incompletedTasksCount == 0)
                {
                    State = JobState.Completed;
                    Logger.Write(LogCategories.Information(string.Format("Job {0} completed.", this), LogCategories.TaskServices));
                    InvokeOnJobCompleted();
                }
            }
        }

        /// <summary>
        /// Notifies job that <paramref name="task"/> has been failed.
        /// </summary>
        /// <param name="task">
        /// Task that has been failed.
        /// </param>
        public void NotifyTaskFailed(Task task)
        {
            Contract.Requires(task != null);

            lock (this)
            {
                State = JobState.Failed;
                InvokeOnJobFailed();
            }

            Logger.Write(LogCategories.Error(string.Format("Task {0} in job {1} failed.", task, this), LogCategories.TaskServices));
        }

        #endregion

        #region CodeContract invariant method

        [ContractInvariantMethod]
        private void Invariant()
        {
            Contract.Invariant(Tasks != null);
            Contract.Invariant(incompletedTasksCount <= Tasks.Count);   
        }

        #endregion

        #region Private fields

        private uint incompletedTasksCount;

        #endregion

        #region Event invokers

        private void InvokeOnJobCompleted()
        {
            var handler = OnJobCompleted;
            if (handler != null)
                handler(this, new EventArgs());
        }

        private void InvokeOnJobFailed()
        {
            var handler = OnJobFailed;
            if (handler != null)
                handler(this, new EventArgs());
        }

        #endregion
    }
}
