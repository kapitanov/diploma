using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AISTek.DataFlow.Shared.Client;

namespace AISTek.DataFlow.Shared.Patterns.PatternExecution
{
    internal class JobExecutionSequence
    {
        public JobExecutionSequence(JobRuntime runtime)
        {
            if (runtime == null)
                throw new ArgumentNullException("runtime");

            this.runtime = runtime;
            stages = new ExecutionStage[] 
            {
                new BuildFileListExecutionStage(runtime),
                new UploadFilesExecutionStage(runtime),
                new CreateJobExecutionStage(runtime),
                new UploadTasksExecutionStage(runtime),
                new ExecuteJobExecutionStage(runtime),
                new ReadResultsExecutionStage(runtime),
                new CleanupExecutionStage(runtime)
            };
        }

        public void Execute()
        {
            foreach (var stage in stages)
            {
                stage.Execute();
            }
        }

        private readonly JobRuntime runtime;
        private readonly ExecutionStage[] stages;
    }
}
