using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.ServiceModel;
using System.Threading;
using AISTek.DataFlow.ComputeNode.Classes;
using AISTek.DataFlow.PerfomanceToolkit;
using AISTek.DataFlow.Shared.Classes;
using AISTek.DataFlow.Shared.Client;
using AISTek.DataFlow.Util;
using Microsoft.Practices.Unity;

namespace AISTek.DataFlow.ComputeNode.ExecutionCore
{
    public sealed class GeneralCore : IExecutionCore
    {
        [InjectionConstructor]
        public GeneralCore(
            [Dependency]
            ITaskProviderClientFactory taskProviderClientFactory,
            [Dependency]
            IRepositoryClientFactory repositoryClientFactory,
            [Dependency]
            ISystemResources systemResources,
            [Dependency]
            IUnityContainer container,
            [Dependency]
            IExecutionStatus status,
            [Dependency]
            IWrapperFactory wrapperFactory)
        {
            Contract.Requires(taskProviderClientFactory != null);
            Contract.Requires(repositoryClientFactory != null);
            Contract.Requires(systemResources != null);
            Contract.Requires(container != null);
            Contract.Requires(wrapperFactory != null);

            this.taskProviderClientFactory = taskProviderClientFactory;
            this.repositoryClientFactory = repositoryClientFactory;
            this.systemResources = systemResources;
            this.container = container;
            this.status = status;
            this.wrapperFactory = wrapperFactory;
        }

        private readonly ITaskProviderClientFactory taskProviderClientFactory;
        private readonly IRepositoryClientFactory repositoryClientFactory;
        private readonly ISystemResources systemResources;
        private readonly IWrapperFactory wrapperFactory;
        private IExecutionEngine engine;
        private readonly IExecutionStatus status;
        private readonly IUnityContainer container;

        #region Implementation of IExecutionCore

        /// <summary>
        /// Configures the execution core.
        /// </summary>
        /// <param name="configuration">
        /// An instance of <see cref="IExecutionCoreConfiguration"/> that is provides configuration information.
        /// </param>
        public void Setup(IExecutionCoreConfiguration configuration)
        {
            container.RegisterInstance(typeof(Type), "EngineType", configuration.Sandbox);
            engine = container.Resolve<IExecutionEngine>();
        }

        /// <summary>
        /// Starts the task execution workflow and returns after its final.
        /// </summary>
        /// <returns>
        /// An instance of <see cref="ExecutionResult"/> that contains task execution workflow result.
        /// </returns>
        public ExecutionResult StartExecution()
        {
            while (true)
            {
                using (var taskProvider = taskProviderClientFactory.CreateClient())
                using (var repository = repositoryClientFactory.CreateClient())
                {
                    try
                    {
                        ExecutionLoop(taskProvider, repository);
                    }
                    catch (CommunicationException e)
                    {
                        Trace.WriteLine(string.Format("Communication failure, repeating...\n{0}", e));
                    }
                }
            }
        }

        #endregion

        private void ExecutionLoop(ITaskProvider taskProvider, IRepository repository)
        {
            while (true)
            {
                status.CurrentTask = null;
                status.Status = ExecutionStatus.WaitingForTask;
                engine.SystemResources = systemResources;
                engine.Repository = wrapperFactory.CreateWrapper(repository);
                Task task;
                try
                {
                    task = taskProvider.GetTask();
                    if(task == null)
                        continue;
                }
                catch (TimeoutException)
                {
                    (taskProvider as ITaskProviderChannel).Abort();
                    return;
                }

                var interval = taskProvider.QueryNotifyInterval();
                using (new Timer(
                    x => OnTimer((Tuple<ITaskProvider, Guid>) x),
                    Tuple.Create(taskProvider, task.Id),
                    TimeSpan.Zero,
                    interval))
                {
                    status.CurrentTask = task;
                    status.Status = ExecutionStatus.ProcessingTask;
                    try
                    {
                        using (Perfomance.Trace("GeneralCore::StartExecution()").BindToTrace())
                        {
                            engine.ExecuteTask(task);
                        }
                        ReportSuccess(taskProvider, task);
                    }
                    catch (TaskRejectedException)
                    {
                        ReportReject(taskProvider, task);
                    }
                    catch (TaskFailedException e)
                    {
                        ReportFailure(taskProvider, task, ErrorSource.Task, e.InnerException);
                    }
                    catch (Exception e)
                    {
                        ReportFailure(taskProvider, task, ErrorSource.Engine, e.InnerException);
                    }
                }
            }
        }

        private ExecutionResult ReportFailure(ITaskProvider taskProvider, Task task, ErrorSource source, Exception error)
        {
            taskProvider.FailTask(task.Id, new ErrorReport(task, error, source));
            return ExecutionResult.Failed(task.Id);
        }

        private ExecutionResult ReportReject(ITaskProvider taskProvider, Task task)
        {
            taskProvider.RejectTask(task.Id);
            return ExecutionResult.Rejected(task.Id);
        }

        private ExecutionResult ReportSuccess(ITaskProvider taskProvider, Task task)
        {
            taskProvider.CompleteTask(task.Id);
            return ExecutionResult.Success(task.Id);
        }

        private void OnTimer(Tuple<ITaskProvider, Guid> parameters)
        {
            parameters.Match((taskProvider, taskId) => taskProvider.NotifyWorker(taskId));
        }
    }
}
