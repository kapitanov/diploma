using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.Shared.Patterns.PatternExecution
{
    internal class FaultedJobExecutionResult : IJobExecutionResult
    {
        public FaultedJobExecutionResult(ErrorSummary errors)
        {
            if (errors == null)
                throw new ArgumentNullException("errors");

            Errors = errors;
        }

        public JobExecutionState State { get { return JobExecutionState.Faulted; } }

        public ErrorSummary Errors { get; private set; }
    }
}
