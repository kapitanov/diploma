using System;
using System.Diagnostics.Contracts;
using System.ServiceModel;
using AISTek.DataFlow.Shared.Classes.Contracts;

namespace AISTek.DataFlow.Shared.Classes
{
    /// <summary>
    /// Task manager service's contract
    /// </summary>
    [ContractClass(typeof(ContractForITaskProvider))]
    [ServiceKnownType(typeof(TimeSpan))]
    [ServiceKnownType(typeof(Exception))]
    [ServiceContract(
        Namespace = Namespaces.Scheduler.Namespace, 
        SessionMode = SessionMode.Required,
        ConfigurationName = Namespaces.Scheduler.TaskProvider.Configuration)]
    public interface ITaskProvider
    {
        /// <summary>
        /// Gets a <see cref="TimeSpan"/> that defines worker notifications interval.
        /// </summary>
        /// <returns>
        /// An instance of <see cref="TimeSpan"/> that contains worker notification period.
        /// </returns>
        [OperationContract(
            Action = Namespaces.Scheduler.TaskProvider.Actions.QueryNotifyInterval,
            ReplyAction = Namespaces.Scheduler.TaskProvider.Actions.QueryNotifyIntervalResponse)]
        TimeSpan QueryNotifyInterval();

        /// <summary>
        /// Request a task for execution.
        /// </summary>
        /// <returns>
        /// Blocks current thread until a task will be available for execution.
        /// </returns>
        [OperationContract(
            Action = Namespaces.Scheduler.TaskProvider.Actions.GetTask,
            ReplyAction = Namespaces.Scheduler.TaskProvider.Actions.GetTaskResponse)]
        Task GetTask();

        /// <summary>
        /// Informs scheduler that task with id = <paramref name="taskId"/> was rejected for execution.  
        /// </summary>
        /// <param name="taskId">
        /// Task's unique identifier.
        /// </param>
        [OperationContract(
            Action = Namespaces.Scheduler.TaskProvider.Actions.RejectTask,
            ReplyAction= Namespaces.Scheduler.TaskProvider.Actions.RejectTaskResponse)]
        void RejectTask(Guid taskId);

        /// <summary>
        /// Informs scheduler that task with id = <paramref name="taskId"/> has thrown an exception <paramref name="exception"/>.  
        /// </summary>
        /// <param name="taskId">
        /// Task's unique identifier.
        /// </param>
        /// <param name="errorReport">
        /// Error report.
        /// </param>
        [OperationContract(
            Action = Namespaces.Scheduler.TaskProvider.Actions.FailTask,
            ReplyAction = Namespaces.Scheduler.TaskProvider.Actions.FailTaskResponse)]
        void FailTask(Guid taskId, ErrorReport errorReport);

        /// <summary>
        /// Informs scheduler that task with id = <paramref name="taskId"/> has been successfully executed.
        /// </summary>
        /// <param name="taskId">
        /// Task's unique identifier.
        /// </param>
        [OperationContract(
            Action = Namespaces.Scheduler.TaskProvider.Actions.CompleteTask,
            ReplyAction = Namespaces.Scheduler.TaskProvider.Actions.CompleteTaskResponse)]
        void CompleteTask(Guid taskId);

        /// <summary>
        /// Informs scheduler that task with id = <paramref name="taskId"/> is being executed.
        /// </summary>
        /// <param name="taskId">
        /// Task's unique identifier.
        /// </param>
        [OperationContract(
            Action = Namespaces.Scheduler.TaskProvider.Actions.NotifyWorker,
            ReplyAction = Namespaces.Scheduler.TaskProvider.Actions.NotifyWorkerResponse)]
        void NotifyWorker(Guid taskId);
    }
}
