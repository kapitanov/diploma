using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AISTek.DataFlow.Shared.Patterns.PatternExecution
{
    internal class IntermediateJobExecutionResult : IJobExecutionResult
    {
        public IntermediateJobExecutionResult(TimeSpan timing)
        {
            Timing = timing;
        }

        public JobExecutionState State { get { return JobExecutionState.Executed; } }

        public Classes.ErrorSummary Errors { get { throw new InvalidOperationException(); } }

        public TimeSpan Timing { get; private set; }
    }
}
