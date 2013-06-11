using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AISTek.DataFlow.Shared.Client;
using System.Threading;
using AISTek.DataFlow.Shared.Classes;
using AISTek.DataFlow.Shared.DataExchange;

namespace AISTek.DataFlow.Shared.Patterns.PatternExecution
{
    internal class JobRuntime
    {
        public JobRuntime(
            IDataFlowConnection connection,
            DataContractOptions dtoOptions,
            Japi.Job job,
            IJobResultReader jobResultReader)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            if (dtoOptions == null)
                throw new ArgumentNullException("dtoOptions");
            if (job  == null)
                throw new ArgumentNullException("job");
            if (jobResultReader == null)
                throw new ArgumentNullException("job");

            this.connection = connection;
            this.dtoOptions = dtoOptions;
            this.job = job;
            this.jobResultReader = jobResultReader;

            executionSequence = new JobExecutionSequence(this);
            JobStateIndeterminable();
        }

        public void Run()
        {
            executionSequence.Execute();
        }

        #region Internal job state mutation methods

        internal void JobStateIndeterminable()
        {
            ExecutionResult = new IndeterminateJobExecutionResult();
        }

        internal void JobStateExecuted(TimeSpan timing)
        {
            ExecutionResult = new IntermediateJobExecutionResult(timing);
        }

        internal void JobStateCompleted(IJobExecutionResult successfulResult)
        {
            if (successfulResult == null)
                throw new ArgumentNullException("successfulResult");
            if (successfulResult.State !=  JobExecutionState.Completed)
                throw new ArgumentException("Computation is not completed.");
   
            ExecutionResult = successfulResult;
        }

        internal void JobStateFailed(ErrorSummary errorSummary)
        {
            if (errorSummary == null)
                throw new ArgumentNullException("errorSummary");

            ExecutionResult = new FaultedJobExecutionResult(errorSummary);
        }

        #endregion

        #region Internal accessors

        internal IDataFlowConnection Connection { get { return connection; } }
        
        internal DataContractOptions DtoOptions { get { return dtoOptions; } }

        internal IJobResultReader ResultReader { get { return jobResultReader; } }

        internal IJobExecutionResult ExecutionResult { get; private set; }

        internal Japi.Job Job { get { return job; } }

        #endregion

        private readonly Japi.Job job;
        private readonly IJobResultReader jobResultReader;

        private readonly IDataFlowConnection connection;
        private readonly DataContractOptions dtoOptions;

        private readonly JobExecutionSequence executionSequence;
    }
}
