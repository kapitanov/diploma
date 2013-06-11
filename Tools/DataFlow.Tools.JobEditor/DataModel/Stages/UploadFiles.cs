using System;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.Tools.JobEditor.DataModel.Stages
{
    public class UploadFiles : Stage
    {
        public UploadFiles()
            : base("Uploading files...")
        { }

        #region Overrides of Stage

        public override void Execute(
            JobDefinition job, 
            Action<int, int> notifyProgress, 
            Action<string> updateStatus,
            IRepository repositoryService, 
            IJobManager jobManager)
        {
            var index = 0;
            var count = job.Files.Count;
            foreach (var file in job.Files)
            {
                index++;
                notifyProgress(index, count);
                if(file is CreateFileRequest)
                    updateStatus(string.Format("Creating file: {0}...", file.Name));
                else
                    updateStatus(string.Format("Uploading file: {0}...", file.Name));
                file.SaveFile(repositoryService);
            }
        }

        #endregion
    }
}
