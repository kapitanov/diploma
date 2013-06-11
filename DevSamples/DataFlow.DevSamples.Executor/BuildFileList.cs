using System;
using System.Linq;
using AISTek.DataFlow.Shared.Classes;
using Japi = AISTek.DataFlow.Shared.Japi;

namespace AISTek.DataFlow.DevSamples.Executor
{
    public class BuildFileList : Stage
    {
        public BuildFileList()
            : base("Building actual list of files in job")
        { }

        public override void Execute(
            Japi.Job job,
            Action<string> updateStatus,
            IRepository repositoryService,
            IJobManager jobManager)
        {
            var allFiles = job.Files.ToDictionary(file => file, _ => false);

            var totalItems = job.Tasks.Count;
            var currentItem = 0;
            foreach(var task in job.Tasks)
            {
                updateStatus(string.Format("Processing files used by task \"{0}\"...", task.Name));
                allFiles[task.EntryPoint.Assembly] = true;

                foreach(var file in task.EntryPoint.References)
                    allFiles[file] = true;

                foreach(var file in task.InputFiles)
                    allFiles[file] = true;

                foreach(var file in task.OutputFiles)
                    allFiles[file] = true;

                currentItem++;
            }

            updateStatus("Building file list...");

            job.Files = allFiles.Where(pair => pair.Value)
                .Select(pair => pair.Key)
                .ToList();
        }
    }
}
