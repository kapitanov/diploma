using System;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.Tools.JobEditor.DataModel.Stages
{
    public class CreateJob : Stage
    {
        public CreateJob()
            : base("Creating job...")
        {}

        #region Overrides of Stage

        public override void Execute(
            JobDefinition job, 
            Action<int, int> notifyProgress, 
            Action<string> updateStatus,
            IRepository repositoryService, 
            IJobManager jobManager)
        {
            updateStatus("Creating job...");
            job.Id = jobManager.CreateJob(job.Name).Id;
        }

        #endregion
    }
}
