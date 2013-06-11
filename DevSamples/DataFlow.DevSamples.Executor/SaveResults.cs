using System;
using System.IO;
using System.Linq;
using AISTek.DataFlow.Shared.Classes;
using AISTek.DataFlow.Shared.Japi;
using Job = AISTek.DataFlow.Shared.Japi.Job;

namespace AISTek.DataFlow.DevSamples.Executor
{
    public class SaveResults : Stage
    {
        public SaveResults ()
            : base("Saving results")
        {}

        public override void Execute(
            Job job, 
            Action<string> updateStatus,
            IRepository repositoryService, 
            IJobManager jobManager)
        {
            foreach(var file in job.Files.OfType<CreateFile>())
            {
                updateStatus(string.Format("Downloading file: \"{0}\"...", file.Name));
                using(var stream = repositoryService.Download(file.Link.Id))
                {
                    using(var fstream = new FileStream(
                        Path.Combine(Directory.GetCurrentDirectory(), string.Format("{0}.data", file.Name)),
                        FileMode.Create))
                    {
                        var buff = new byte[256];
                        while(true)
                        {
                            var isCompleted = stream.Read(buff, 0, buff.Length) < buff.Length;
                            fstream.Write(buff, 0, buff.Length);
                            if(isCompleted)
                                break;
                        }
                    }
                }
            }
        }
    }
}
