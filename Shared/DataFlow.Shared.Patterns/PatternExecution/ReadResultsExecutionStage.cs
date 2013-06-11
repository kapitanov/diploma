using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AISTek.DataFlow.Shared.Patterns.PatternExecution
{
    internal class ReadResultsExecutionStage : ExecutionStage
    {
        public ReadResultsExecutionStage (JobRuntime runtime)
            : base(runtime)
        { }

        public override string Name { get { return "ReadResults"; } }

        public override void Execute()
        {
            if (Runtime.ExecutionResult.State == JobExecutionState.Executed)
            {
                var results = Runtime.ResultReader.ReadResult(Runtime);
                Runtime.JobStateCompleted(results);
            }
        }
    }
}
