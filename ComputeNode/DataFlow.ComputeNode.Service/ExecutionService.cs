using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.ServiceProcess;
using System.Threading;
using AISTek.DataFlow.ComputeNode.Classes;
using Microsoft.Practices.Unity;

namespace AISTek.DataFlow.ComputeNode.Service
{
    public sealed class ExecutionService : ServiceBase, IExecutionService
    {
        [InjectionConstructor]
        public ExecutionService(
            [Dependency]
            IExecutionCore core,
            [Dependency]
            IExecutionCoreConfiguration configuration,
            [Dependency]
            IExecutionStatus status)
        {
            Contract.Requires(core != null);
            Contract.Requires(configuration != null);

            this.core = core;
            this.configuration = configuration;
            this.status = status;

            ServiceName = "ExecutionCore host";
            CanStop = true;
            CanPauseAndContinue = false;
            AutoLog = true;
        }

        private readonly IExecutionCore core;
        private readonly IExecutionCoreConfiguration configuration;
        private readonly IExecutionStatus status;
        private Thread workerThread;

        protected override void OnStart(string[] args)
        {
            try
            {
                status.StatusMessage = "Configurating...";
                core.Setup(configuration);
                workerThread = new Thread(WorkerThread);
                workerThread.Start();
            }
            catch (Exception e)
            {
                Debugger.Break();
                throw;
            }
        }

        private void WorkerThread()
        {
            while (true)
            {
                try
                {
                    var result = core.StartExecution();
                    status.StatusMessage = string.Format("Task {{{0}}} executed with result: \"{1}\"", result.TaskId,
                                                         result.State);
                }
                catch (ApplicationException)
                { }
                catch (ThreadAbortException)
                { }
            }
        }

        protected override void OnStop()
        {
            workerThread.Abort();
        }

        #region Implementation of IExecutionService

        void IExecutionService.Stop()
        {
            OnStop();
        }

        void IExecutionService.Start()
        {
            OnStart(new string[] { });
        }

        #endregion
    }
}
