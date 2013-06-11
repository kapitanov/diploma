using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AISTek.DataFlow.Shared.Patterns.PatternExecution
{
    internal class BuildFileListExecutionStage : ExecutionStage
    {
        public BuildFileListExecutionStage(JobRuntime runtime)
            : base(runtime)
        {}

        public override string Name { get { return "BuildFileList"; } }

        public override void Execute()
        {
            var allFiles = Job.Files.ToDictionary(file => file, _ => false);

            foreach (var task in Job.Tasks)
            {
                allFiles[task.EntryPoint.Assembly] = true;

                foreach (var file in task.EntryPoint.References)
                    allFiles[file] = true;

                foreach (var file in task.InputFiles)
                    allFiles[file] = true;

                foreach (var file in task.OutputFiles)
                    allFiles[file] = true;
            }

            Job.Files = allFiles.Where(pair => pair.Value).Select(pair => pair.Key).ToList();
        }
    }
}
