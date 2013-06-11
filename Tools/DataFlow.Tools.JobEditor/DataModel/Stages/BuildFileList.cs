using System;
using System.Linq;
using System.Threading;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.Tools.JobEditor.DataModel.Stages
{
    public class BuildFileList : Stage
    {
        public BuildFileList()
            : base("Building actual list of files in job.")
        { }

        #region Overrides of Stage

        public override void Execute(
            JobDefinition job,
            Action<int, int> notifyProgress,
            Action<string> updateStatus,
            IRepository repositoryService,
            IJobManager jobManager)
        {
            var allFiles = job.Files.ToDictionary(file => file, _ => false);

            var totalItems = job.Tasks.Count;
            var currentItem = 0;
            foreach (var task in job.Tasks)
            {
                updateStatus(string.Format("Processing files used by task \"{0}\"...", task.Name));
                allFiles[task.EntryPoint.Assembly] = true;

                foreach (var file in task.EntryPoint.References)
                    allFiles[file] = true;

                foreach (var file in task.InputFiles)
                    allFiles[file] = true;

                foreach (var file in task.OutputFiles)
                    allFiles[file] = true;

                currentItem++;
                notifyProgress(currentItem, totalItems);
            }

            updateStatus("Building file list...");

            job.Files = allFiles.Where(pair => pair.Value)
                .Select(pair => pair.Key)
                .ToList();
        }

        #endregion
    }
}
