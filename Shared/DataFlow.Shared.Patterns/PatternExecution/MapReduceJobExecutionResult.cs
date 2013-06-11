using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AISTek.DataFlow.Shared.Patterns.PatternExecution
{
    internal class MapReduceJobExecutionResult<TOutput> : IJobExecutionResult
    {
        public MapReduceJobExecutionResult(TOutput output, TimeSpan timing)
        {
            Output = output;
            Timing = timing;
        }

        public JobExecutionState State { get { return JobExecutionState.Completed; } }

        public Classes.ErrorSummary Errors { get { throw new InvalidOperationException(); } }

        public TOutput Output { get; private set; }

        public TimeSpan Timing { get; private set; }
    }
}
