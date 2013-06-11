using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace AISTek.DataFlow.Shared.Classes.Contracts
{
    [DebuggerStepThrough]
    [ContractClassFor(typeof(IJobManager))]
    internal class ContractForIJobManager : IJobManager
    {
        #region Implementation of IJobManager

        /// <summary>
        /// Creates a job with specified name and sets it as current.
        /// </summary>
        /// <param name="name">
        /// Job's name.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Job"/>.
        /// </returns>
        [DebuggerStepThrough]
        public Job CreateJob(string name)
        {
            Contract.Requires(!string.IsNullOrEmpty(name));

            throw new NotImplementedException();
        }

        /// <summary>
        /// Opens an existing job and sets it as current.
        /// </summary>
        /// <param name="id">
        /// Job's UID.
        /// </param>
        [DebuggerStepThrough]
        public void OpenJob(Guid id)
        {
            Contract.Requires(id != Guid.Empty);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a task in current job.
        /// </summary>
        /// <param name="task">
        /// An instance of <see cref="Task"/> which defines the task to create.
        /// </param>
        /// <returns>
        /// An unique identifier for created task.
        /// </returns>
        [DebuggerStepThrough]
        public Guid CreateTask(Task task)
        {
            Contract.Requires(task != null);

            throw new NotImplementedException();
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
        [DebuggerStepThrough]
        public void UpdateTask(Guid id, Task task)
        {
            Contract.Requires(id != Guid.Empty);
            Contract.Requires(task != null);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes the task from current job.
        /// </summary>
        /// <param name="id">
        /// Task's UID.
        /// </param>
        [DebuggerStepThrough]
        public void RemoveTask(Guid id)
        {
            Contract.Requires(id != Guid.Empty);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Starts execution of the current job.
        /// </summary>
        [DebuggerStepThrough]
        public void StartJob()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Cancels execution of the current job.
        /// </summary>
        [DebuggerStepThrough]
        public void CancelJob()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reinitializes current job after cancelling, allowing to start it again.
        /// </summary>
        [DebuggerStepThrough]
        public void RestartJob()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes current job from scheduler. Job must not be in state "Processing".
        /// </summary>
        [DebuggerStepThrough]
        public void DeleteJob()
        {
            throw new NotImplementedException();
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
        [DebuggerStepThrough]
        public IEnumerable<Job> FindJobs(string name)
        {
            Contract.Requires(!string.IsNullOrEmpty(name));

            throw new NotImplementedException();
        }

        /// <summary>
        /// Get list of all jobs in scheduler.
        /// </summary>
        /// <returns>
        /// List of all jobs in scheduler.
        /// </returns>
        public IEnumerable<Job> GetAllJobs()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets an instance of <see cref="Job"/> which describes current job.
        /// </summary>
        public Job CurrentJob
        {
            [DebuggerStepThrough]
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets a state of the the job.
        /// </summary>
        /// <param name="jobId">
        /// Unique identifier of a job.
        /// </param>
        /// <returns>
        /// A value of <see cref="JobState"/> that contains the state of the job.
        /// If the job with speicifed GUID doesn't exists, then a value of <see cref="JobState.NotExists"/>
        /// </returns>
        public JobState QueryJobState(Guid jobId)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        #endregion
    }
}
