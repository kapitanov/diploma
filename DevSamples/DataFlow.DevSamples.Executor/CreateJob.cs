using System;
using AISTek.DataFlow.Shared.Classes;
using Japi = AISTek.DataFlow.Shared.Japi;

namespace AISTek.DataFlow.DevSamples.Executor
{
    public class CreateJob : Stage
    {
        public CreateJob()
            : base("Creating job...")
        { }


        public override void Execute(
            Japi.Job job,
            Action<string> updateStatus,
            IRepository repositoryService,
            IJobManager jobManager)
        {
            job.Id = jobManager.CreateJob(job.Name).Id;
        }
    }
}
