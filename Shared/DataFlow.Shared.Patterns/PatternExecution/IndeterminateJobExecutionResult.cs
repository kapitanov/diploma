using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AISTek.DataFlow.Shared.Patterns.PatternExecution
{
    internal class IndeterminateJobExecutionResult : IJobExecutionResult
    {
        public JobExecutionState State { get { return JobExecutionState.Indeterminate; } }

        public Classes.ErrorSummary Errors { get { throw new InvalidOperationException(); } }
    }
}
