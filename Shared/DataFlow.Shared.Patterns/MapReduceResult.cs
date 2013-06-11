using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using AISTek.DataFlow.Shared.Classes;
using AISTek.DataFlow.Shared.Patterns.PatternExecution;
using AISTek.DataFlow.Shared.Client;
using AISTek.DataFlow.Shared.DataExchange;

namespace AISTek.DataFlow.Shared.Patterns
{
    internal class MapReduceResult<TOutput> : IResult<TOutput>
    {
        public MapReduceResult(Japi.Job job, DataContractOptions dtoOptions)
        {
            if (job == null)
                throw new ArgumentNullException("job");
            if (dtoOptions == null)
                throw new ArgumentNullException("dtoOptions");

            this.job = job;
            this.dtoOptions = dtoOptions;
        }

        public IResult<TOutput> Startup(IDataFlowConnection connection)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                var runtime = new JobRuntime(connection, dtoOptions, job, new MapReduceJobResultReader<TOutput>());
                runtime.Run();

                if (runtime.ExecutionResult.State == JobExecutionState.Faulted)
                {
                    isFaulted = true;
                    var faultResult = runtime.ExecutionResult as FaultedJobExecutionResult;
                    if (faultResult != null)
                    {
                        errors = faultResult.Errors;
                    }
                }
                else if (runtime.ExecutionResult.State == JobExecutionState.Completed)
                {
                    isFaulted = false;
                    var outputResult = runtime.ExecutionResult as MapReduceJobExecutionResult<TOutput>;
                    if (outputResult != null)
                    {
                        result = outputResult.Output;
                        timing = outputResult.Timing;
                    }
                }

                syncObject.Set();
            });

            return this;
        }

        public TOutput Value
        {
            get
            {
                syncObject.WaitOne();

                if (!isFaulted)
                {
                    return result;
                }

                throw new JobExecutionException("Unable to execute job", errors);
            }
        }

        public TimeSpan Timing
        {
            get
            {
                syncObject.WaitOne();

                if (!isFaulted)
                {
                    return timing;
                }

                throw new JobExecutionException("Unable to execute job", errors);
            }
        }

        private ManualResetEvent syncObject = new ManualResetEvent(false);
        private bool isFaulted;
        private TOutput result;
        private TimeSpan timing;
        private ErrorSummary errors;
        private readonly Japi.Job job;
        private readonly DataContractOptions dtoOptions;
    }
}
