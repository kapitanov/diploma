using System;
using AISTek.DataFlow.Shared.Classes;
using Japi = AISTek.DataFlow.Shared.Japi;

namespace AISTek.DataFlow.DevSamples.Executor
{
    public class UploadFiles : Stage
    {
        public UploadFiles()
            : base("Uploading files")
        { }

        #region Overrides of Stage

        public override void Execute(
            Japi.Job job,
            Action<string> updateStatus,
            IRepository repositoryService,
            IJobManager jobManager)
        {
            foreach(var file in job.Files)
            {
                if(file is Japi.CreateFile)
                    updateStatus(string.Format("Creating file: {0}...", file.Name));
                else
                    updateStatus(string.Format("Uploading file: {0}...", file.Name));
                file.SaveFile(repositoryService);
            }
        }

        #endregion
    }
}
