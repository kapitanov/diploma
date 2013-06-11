using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AISTek.DataFlow.Shared.Patterns.PatternExecution
{
    internal class CleanupExecutionStage : ExecutionStage
    {
        public CleanupExecutionStage (JobRuntime runtime)
            : base(runtime)
        { }

        public override string Name { get { return "CleanUp"; } }

        public override void Execute()
        {
            // Delete job
            Runtime.Connection.JobManager.DeleteJob();

            // Delete files
            foreach (var file in Job.Files)
            {
                Runtime.Connection.Repository.Delete(file.Link.Id);
            }
        }
    }
}
