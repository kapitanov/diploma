using System;
using System.Collections.Generic;
using System.Diagnostics;
using AISTek.DataFlow.MasterNode.DataModel;
using System.Diagnostics.Contracts;

namespace AISTek.DataFlow.MasterNode.Scheduler.Contracts
{
    [DebuggerStepThrough]
    [ContractClassFor(typeof(IJobScheduler))]
    internal class ContractForIJobScheduler : IJobScheduler
    {
        #region Implementation of IJobScheduler

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
            Contract.Requires(!string.IsNullOrEmpty(name));
            throw new NotImplementedException();
        }

        /// <summary>
        /// Registers newly created job in manager and sets job's UID.
        /// </summary>
        /// <param name="job">
        /// An instance of <see cref="Job"/>. 
        /// </param>
        public void RegisterJob(Job job)
        {
            Contract.Requires(job != null);
            Contract.Requires(job.State == JobState.Creating);
            Contract.Ensures(job.State == JobState.Ready);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Starts executin of specified job.
        /// </summary>
        /// <param name="job">
        /// An instance of <see cref="Job"/>. 
        /// </param>
        public void StartJob(Job job)
        {
            Contract.Requires(job != null);
            Contract.Requires(job.State == JobState.Ready);
            Contract.Ensures(job.State == JobState.Processing);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Cancels execution of specified job.
        /// </summary>
        /// <param name="job">
        /// An instance of <see cref="Job"/>. 
        /// </param>
        public void CancelJob(Job job)
        {
            Contract.Requires(job != null);
            Contract.Requires(job.State == JobState.Processing);
            Contract.Ensures(job.State == JobState.Cancelled);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Reinitializes cancelled job.
        /// </summary>
        /// <param name="job">
        /// An instance of <see cref="Job"/>. 
        /// </param>
        public void RestartJob(Job job)
        {
            Contract.Requires(job != null);
            Contract.Requires(job.State == JobState.Cancelled);
            Contract.Ensures(job.State == JobState.Ready);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes job from scheduler.
        /// </summary>
        /// <param name="job">
        /// An instance of <see cref="Job"/>. 
        /// </param>
        public void DeleteJob(Job job)
        {
            Contract.Requires(job != null);
            Contract.Requires(job.State == JobState.Ready ||
                              job.State == JobState.Cancelled ||
                              job.State == JobState.Failed ||
                              job.State == JobState.Completed ||
                              job.State == JobState.Deleted);
            Contract.Ensures(job.State == JobState.Deleted);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> of <see cref="Job"/>s which contains all registered jobs and their data.
        /// </summary>
        public IEnumerable<Job> Jobs
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Occurs when a job is completed.
        /// </summary>
        public event JobEventHandler OnJobCompleted
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Occurs when job's execution fails.
        /// </summary>
        public event JobEventHandler OnJobFailed
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        #endregion
    }
}


