using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.Shared.Patterns.PatternExecution
{
    internal interface IJobExecutionResult
    {
        JobExecutionState State { get; }
        
        ErrorSummary Errors { get; }
    }
}
