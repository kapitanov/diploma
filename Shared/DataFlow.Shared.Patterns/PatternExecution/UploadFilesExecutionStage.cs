using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AISTek.DataFlow.Shared.Patterns.PatternExecution
{
    internal class UploadFilesExecutionStage : ExecutionStage
    {
        public UploadFilesExecutionStage(JobRuntime runtime)
            : base(runtime)
        { }

        public override string Name { get { return "UploadFiles"; } }

        public override void Execute()
        {
            foreach (var file in Job.Files)
            {
                file.SaveFile(Runtime.Connection.Repository);
            }
        }
    }
}
