using System.Collections.Generic;
using System.Diagnostics.Contracts;
using AISTek.DataFlow.MasterNode.DataModel;
using AISTek.DataFlow.MasterNode.Scheduler.Contracts;

namespace AISTek.DataFlow.MasterNode.Scheduler
{
    /// <summary>
    /// A job manager's contract
    /// </summary>
    [ContractClass(typeof(ContractForIJobScheduler))]
    public interface IJobScheduler
    {
        #region Job management methods

        /// <summary>
        /// Creates new instance of <see cref="Job"/>.
        /// </summary>
        /// <param name="name">
        /// Job's name.
        /// </param>
        /// <returns>
        /// New instance of <see cref="Job"/>.
        /// </returns>
        Job CreateJob(string name);

        /// <summary>
        /// Registers newly created job in manager and sets job's UID.
        /// </summary>
        /// <param name="job">
        /// An instance of <see cref="Job"/>. 
        /// </param>
        void RegisterJob(Job job);

        /// <summary>
        /// Starts executin of specified job.
        /// </summary>
        /// <param name="job">
        /// An instance of <see cref="Job"/>. 
        /// </param>
        void StartJob(Job job);

        /// <summary>
        /// Cancels execution of specified job.
        /// </summary>
        /// <param name="job">
        /// An instance of <see cref="Job"/>. 
        /// </param>
        void CancelJob(Job job);

        /// <summary>
        /// Reinitializes cancelled job.
        /// </summary>
        /// <param name="job">
        /// An instance of <see cref="Job"/>. 
        /// </param>
        void RestartJob(Job job);

        /// <summary>
        /// Removes job from scheduler.
        /// </summary>
        /// <param name="job">
        /// An instance of <see cref="Job"/>. 
        /// </param>
        void DeleteJob(Job job); 

        #endregion

        #region Job queryes
        
        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> of <see cref="Job"/>s which contains all registered jobs and their data.
        /// </summary>
        IEnumerable<Job> Jobs { get; } 

        #endregion

        #region Job state changed events
        
        /// <summary>
        /// Occurs when a job is completed.
        /// </summary>
        event JobEventHandler OnJobCompleted;

        /// <summary>
        /// Occurs when job's execution fails.
        /// </summary>
        event JobEventHandler OnJobFailed; 

        #endregion
    }
}


