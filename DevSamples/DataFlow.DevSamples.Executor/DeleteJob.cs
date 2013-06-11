using System;
using AISTek.DataFlow.Shared.Classes;
using Japi = AISTek.DataFlow.Shared.Japi;

namespace AISTek.DataFlow.DevSamples.Executor
{
    public class DeleteJob : Stage
    {
        public DeleteJob()
            : base("Deleting job...")
        { }


        public override void Execute(
            Japi.Job job,
            Action<string> updateStatus,
            IRepository repositoryService,
            IJobManager jobManager)
        {
            jobManager.DeleteJob();
        }
    }
}
