using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AISTek.DataFlow.Shared.Patterns.PatternExecution
{
    internal class CreateJobExecutionStage : ExecutionStage
    {
        public CreateJobExecutionStage(JobRuntime runtime)
            : base(runtime)
        { }

        public override string Name { get { return "CreateJob"; } }

        public override void Execute()
        {
            Job.Id = Runtime.Connection.JobManager.CreateJob(Job.Name).Id;
        }
    }
}
