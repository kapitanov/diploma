using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.ServiceModel;
using AISTek.DataFlow.Shared.Classes.Contracts;

namespace AISTek.DataFlow.Shared.Classes
{
    /// <summary>
    /// Task manager service's contract
    /// </summary>
    [ContractClass(typeof(ContractForIJobManager))]
    [ServiceContract(
        Namespace = Namespaces.Scheduler.Namespace,
        ConfigurationName = Namespaces.Scheduler.JobManager.Configuration,
        SessionMode = SessionMode.Required,
        CallbackContract = typeof(IJobManagerCallback))]
    public interface IJobManager
    {
        /// <summary>
        /// Creates a job with specified name and sets it as current.
        /// </summary>
        /// <param name="name">
        /// Job's name.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Job"/>.
        /// </returns>
        [OperationContract(
            Action = Namespaces.Scheduler.JobManager.Actions.CreateJob,
            ReplyAction = Namespaces.Scheduler.JobManager.Actions.CreateJobResponse)]
        Job CreateJob(string name);

        /// <summary>
        /// Opens an existing job and sets it as current.
        /// </summary>
        /// <param name="id">
        /// Job's UID.
        /// </param>
        [OperationContract(
            IsOneWay = true,
            Action = Namespaces.Scheduler.JobManager.Actions.OpenJob)]
        void OpenJob(Guid id);

        /// <summary>
        /// Creates a task in current job.
        /// </summary>
        /// <param name="task">
        /// An instance of <see cref="Task"/> which defines the task to create.
        /// </param>
        /// <returns>
        /// An unique identifier for created task.
        /// </returns>
        [OperationContract(
            Action = Namespaces.Scheduler.JobManager.Actions.CreateTask,
            ReplyAction = Namespaces.Scheduler.JobManager.Actions.CreateTaskResponse)]
        Guid CreateTask(Task task);

        /// <summary>
        /// Updates the created task with UID = <paramref name="id"/> in the current job. 
        /// </summary>
        /// <param name="id">
        /// Task's UID.
        /// </param>
        /// <param name="task">
        /// Task's updated definition.
        /// </param>
        [OperationContract(
            IsOneWay = true,
            Action = Namespaces.Scheduler.JobManager.Actions.UpdateTask)]
        void UpdateTask(Guid id, Task task);

        /// <summary>
        /// Removes the task from current job.
        /// </summary>
        /// <param name="id">
        /// Task's UID.
        /// </param>
        [OperationContract(
            IsOneWay = true,
            Action = Namespaces.Scheduler.JobManager.Actions.RemoveTask)]
        void RemoveTask(Guid id);

        /// <summary>
        /// Starts execution of the current job.
        /// </summary>
        [OperationContract(
            IsOneWay = true,
            Action = Namespaces.Scheduler.JobManager.Actions.StartJob)]
        void StartJob();

        /// <summary>
        /// Cancels execution of the current job.
        /// </summary>
        [OperationContract(
            IsOneWay = true,
            Action = Namespaces.Scheduler.JobManager.Actions.CancelJob)]
        void CancelJob();

        /// <summary>
        /// Reinitializes current job after cancelling, allowing to start it again.
        /// </summary>
        [OperationContract(
            IsOneWay = true,
            Action = Namespaces.Scheduler.JobManager.Actions.RestartJob)]
        void RestartJob();

        /// <summary>
        /// Deletes current job from scheduler. Job must not be in state "Processing".
        /// </summary>
        [OperationContract(
            IsOneWay = true,
            Action = Namespaces.Scheduler.JobManager.Actions.DeleteJob)]
        void DeleteJob();

        /// <summary>
        /// Finds all jobs in scheduler which are not being processed and have specified name.
        /// </summary>
        /// <param name="name">
        /// Job's name to match.
        /// </param>
        /// <returns>
        /// List of all matching jobs.
        /// </returns>
        [OperationContract(
            Action = Namespaces.Scheduler.JobManager.Actions.FindJobs,
            ReplyAction = Namespaces.Scheduler.JobManager.Actions.FindJobsResponse)]
        IEnumerable<Job> FindJobs(string name);

        /// <summary>
        /// Get list of all jobs in scheduler.
        /// </summary>
        /// <returns>
        /// List of all jobs in scheduler.
        /// </returns>
        [OperationContract(
            Action = Namespaces.Scheduler.JobManager.Actions.GetAllJobs,
            ReplyAction = Namespaces.Scheduler.JobManager.Actions.GetAllJobsResponse)]
        IEnumerable<Job> GetAllJobs();

        /// <summary>
        /// Gets an instance of <see cref="Job"/> which describes current job.
        /// </summary>
        Job CurrentJob
        {
            [OperationContract(
                Action = Namespaces.Scheduler.JobManager.Actions.GetCurrentJob,
                ReplyAction = Namespaces.Scheduler.JobManager.Actions.GetCurrentJobResponse)]
            get;
        }

        /// <summary>
        /// Gets a state of the the job.
        /// </summary>
        /// <param name="jobId">
        /// Unique identifier of a job.
        /// </param>
        /// <returns>
        /// A value of <see cref="JobState"/> that contains the state of the job.
        /// If the job with specified GUID doesn't exists, then a value of <see cref="JobState.NotExists"/>
        /// </returns>
        [OperationContract(
            Action = Namespaces.Scheduler.JobManager.Actions.QueryJobState,
            ReplyAction = Namespaces.Scheduler.JobManager.Actions.QueryJobStateResponse)]
        JobState QueryJobState(Guid jobId);

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
        [OperationContract(
            Action = Namespaces.Scheduler.JobManager.Actions.GetErrorReport,
            ReplyAction = Namespaces.Scheduler.JobManager.Actions.GetErrorReportResponse)]
        ErrorSummary GetErrorReport(Guid jobId);
    }
}
