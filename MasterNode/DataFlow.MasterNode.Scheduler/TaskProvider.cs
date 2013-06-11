using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.ServiceModel;
using System.Threading;
using AISTek.DataFlow.MasterNode.Core.Services;
using AISTek.DataFlow.MasterNode.DataModel;
using AISTek.DataFlow.MasterNode.Scheduler.Configuration;
using AISTek.DataFlow.Shared.Classes;
using AISTek.DataFlow.PerfomanceToolkit;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.Unity;
using Task = AISTek.DataFlow.Shared.Classes.Task;

namespace AISTek.DataFlow.MasterNode.Scheduler
{
    /// <summary>
    /// An implementation of <see cref="ITaskProvider"/> service contract.
    /// </summary>
    [ServiceBehavior(
        ConfigurationName = Namespaces.Scheduler.TaskProvider.Configuration,
        InstanceContextMode = InstanceContextMode.PerSession,
        AutomaticSessionShutdown = true)]
    public class TaskProvider : ServiceBase, ITaskProvider, IWorkerChannel
    {
        /// <summary>
        /// Initializes new instance of <see cref="TaskProvider"/> class. 
        /// </summary>
        /// <param name="taskQueue">
        /// Injection parameter - an instance of internal service <see cref="ITaskQueue"/>
        /// </param>
        /// <param name="schedulerConfiguration">
        /// Injection parameter - an instance of internal service <see cref="ISchedulerConfiguration"/>
        /// </param>
        [InjectionConstructor]
        public TaskProvider(
            [Dependency]
            ITaskQueue taskQueue,
            [Dependency]
            ISchedulerConfiguration schedulerConfiguration)
        {
            Contract.Requires(taskQueue != null);
            Contract.Ensures(this.taskQueue == taskQueue);

            this.taskQueue = taskQueue;
            var operationContext = OperationContext.Current;
            WcfPerfomance.MonitorEvents(operationContext, "TaskProvider");

            operationContext.Channel.Faulted += OnChannelFaulted;
            operationContext.Channel.Closing += OnChannelClosing;

            pingTimeout = schedulerConfiguration.TaskProvider.PingTimeout;
        }

        #region Dependencies

        private readonly ITaskQueue taskQueue;

        #endregion

        #region Implementation of ITaskQueue

        /// <summary>
        /// Gets a <see cref="TimeSpan"/> that defines worker notifications interval.
        /// </summary>
        /// <returns>
        /// An instance of <see cref="TimeSpan"/> that contains worker notification period.
        /// </returns>
        public TimeSpan QueryNotifyInterval()
        {
            return pingTimeout;
        }

        /// <summary>
        /// Request a task for execution.
        /// </summary>
        /// <returns>
        /// Blocks current thread until a task will be available for execution.
        /// </returns>
        public Task GetTask()
        {
            Contract.Requires<InvalidOperationException>(currentTask == null);

            var timeout = OperationContext.Current.Channel.OperationTimeout.Subtract(TimeSpan.FromMilliseconds(100));
            currentTask = taskQueue.PickTask(this, timeout);
            if (currentTask != null)
            {
                Logger.Write(
                    LogCategories.Information(string.Format("Task {0} assigned to {1}", currentTask, RemoteAddress),
                        LogCategories.TaskServices,
                        LogCategories.TaskProviderService));
                Trace.WriteLine(string.Format("Task {0} is assigned", currentTask));

                AssignTask();
                return currentTask.ToContract();
            }

            return null;
        }

        /// <summary>
        /// Informs taskQueue that task with id = <paramref name="taskId"/> was rejected for execution.  
        /// </summary>
        /// <param name="taskId">
        /// Task's unique identifier.
        /// </param>
        public void RejectTask(Guid taskId)
        {
            Contract.Requires<InvalidOperationException>(currentTask != null);
            Contract.Requires<InvalidOperationException>(taskId == currentTask.Id); ;
            Contract.Ensures(currentTask == null);

            taskQueue.TaskRejected(currentTask);

            Logger.Write(
                   LogCategories.Information(string.Format("Task {0} was rejected from {1}", currentTask, RemoteAddress),
                       LogCategories.TaskServices,
                       LogCategories.TaskProviderService));
            Trace.WriteLine(string.Format("Task {0} is rejected", currentTask));
            DiscardTask();
        }

        /// <summary>
        /// Informs scheduler that task with id = <paramref name="taskId"/> has thrown an exception <paramref name="exception"/>.  
        /// </summary>
        /// <param name="taskId">
        /// Task's unique identifier.
        /// </param>
        /// <param name="exception">
        /// Error report.
        /// </param>
        public void FailTask(Guid taskId, ErrorReport exception)
        {
            Contract.Requires<InvalidOperationException>(currentTask != null);
            Contract.Requires<InvalidOperationException>(taskId == currentTask.Id);
            Contract.Ensures(currentTask == null);

            taskQueue.TaskFailed(currentTask, exception);

            Logger.Write(
                   LogCategories.Information(string.Format("Task {0} failed on {1}", currentTask, RemoteAddress),
                       LogCategories.TaskServices,
                       LogCategories.TaskProviderService));
            Trace.WriteLine(string.Format("Task {0} is failed: {1}", currentTask, exception.Error));
            DiscardTask();
        }

        /// <summary>
        /// Informs taskQueue that task with id = <paramref name="taskId"/> has been successfully executed.
        /// </summary>
        /// <param name="taskId">
        /// Task's unique identifier.
        /// </param>
        public void CompleteTask(Guid taskId)
        {
            Contract.Requires<InvalidOperationException>(currentTask != null);
            Contract.Requires<InvalidOperationException>(taskId == currentTask.Id);
            Contract.Ensures(currentTask == null);

            taskQueue.TaskCompleted(currentTask);

            Logger.Write(
                   LogCategories.Information(string.Format("Task {0} completed on {1}", currentTask, RemoteAddress),
                       LogCategories.TaskServices,
                       LogCategories.TaskProviderService));
            Trace.WriteLine(string.Format("Task {0} is completed", currentTask));
            DiscardTask();
        }

        /// <summary>
        /// Informs scheduler that task with id = <paramref name="taskId"/> is being executed.
        /// </summary>
        /// <param name="taskId">
        /// Task's unique identifier.
        /// </param>
        public void NotifyWorker(Guid taskId)
        {
            if (taskId == currentTask.Id)
                Trace.WriteLine(string.Format("Task {0} notified", currentTask));
            else
                Trace.WriteLine(string.Format("Task {{{0}}} notified", taskId));
        }

        #endregion

        #region Private members

        #region Private fields

        private DataModel.Task currentTask;
        private readonly TimeSpan pingTimeout;

        #endregion

        #region Event handlers

        /// <summary>
        /// This method is called when <see cref="ICommunicationObject.Closing"/> event is raised.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains no event data.
        /// </param>
        private void OnChannelClosing(object sender, EventArgs e)
        {
            if (currentTask != null)
            {
                Logger.Write(
                     LogCategories.Error(string.Format("Task {0} rejected from {1} due to unexpected communication closing.", currentTask, RemoteAddress),
                         LogCategories.TaskServices,
                         LogCategories.TaskProviderService));

                DiscardTask();
            }
        }

        /// <summary>
        /// This method is called when <see cref="ICommunicationObject.Faulted"/> event is raised.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains no event data.
        /// </param>
        private void OnChannelFaulted(object sender, EventArgs e)
        {
            Logger.Write(
                LogCategories.Error(
                    string.Format("Communication failure on {0}", RemoteAddress),
                    LogCategories.TaskServices, LogCategories.TaskProviderService));

            if (currentTask != null)
            {
                Trace.Write(string.Format("TaskProvider: Communication failure, task {0} is rejected.", currentTask));
                Logger.Write(
                     LogCategories.Error(string.Format("Task {0} rejected from {1} due to communication failure.", currentTask, RemoteAddress),
                         LogCategories.TaskServices,
                         LogCategories.TaskProviderService));

                currentTask.State = TaskState.Processing;
                taskQueue.TaskRejected(currentTask);
                DiscardTask();
            }
        }

        /// <summary>
        /// This method is called when service is terminating and service instance object is being disposed.
        /// </summary>
        protected override void OnServiceTerminating()
        {
            Logger.Write(
                     LogCategories.Error(string.Format("Task {0} on {1}: OnServiceTerminating", currentTask, RemoteAddress),
                         LogCategories.TaskServices,
                         LogCategories.TaskProviderService));
            base.OnServiceTerminating();
        }

        #endregion

        #region Task assigning and discarding methods

        /// <summary>
        /// Assigns task and subscribes to current worker channel events.
        /// </summary>
        private void AssignTask()
        {
            Contract.Requires(currentTask != null);
            Contract.Requires(currentTask.Worker == null);
            Contract.Ensures(currentTask.Worker == this);

            var operationContext = OperationContext.Current;
            operationContext.Channel.Closing += OnChannelClosing;
            operationContext.Channel.Faulted += OnChannelFaulted;
            currentTask.Worker = this;
        }

        /// <summary>
        /// Clears task and unsubscribes from current worker channel events.
        /// </summary>
        private void DiscardTask()
        {
            Contract.Requires(currentTask != null);
            Contract.Requires(currentTask.Worker == this);
            Contract.Ensures(currentTask == null);

            try
            {
                var operationContext = OperationContext.Current;
                operationContext.Channel.Closing -= OnChannelClosing;
                operationContext.Channel.Faulted -= OnChannelFaulted;

                currentTask.Worker = null;
                currentTask = null;
            }
            catch
            { }
        }

        #endregion

        #endregion

        #region Implementation of ICancellable

        /// <summary>
        /// Interrupts operation.
        /// </summary>
        public void Cancel()
        {
        }

        #endregion

        #region Implementation of IWorkerChannel

        public bool IsOk
        {
            get
            {
                return OperationContext.Current.Channel.State == CommunicationState.Opened;
            }
        }

        #endregion
    }
}


