using System;
using AISTek.DataFlow.Shared.Classes;
using Job = AISTek.DataFlow.Shared.Japi.Job;

namespace AISTek.DataFlow.DevSamples.Executor
{
    public class DeleteFiles : Stage
    {
        public DeleteFiles()
            : base("Deleting files")
        { }

        public override void Execute(
            Job job,
            Action<string> updateStatus,
            IRepository repositoryService,
            IJobManager jobManager)
        {
            Console.WriteLine("Press any key to begin deleting files...");
            Console.ReadKey();
            foreach(var file in job.Files)
            {
                updateStatus(string.Format("Deleting file: \"{0}\"...", file.Name));
                repositoryService.Delete(file.Link.Id);
            }
        }
    }
}
