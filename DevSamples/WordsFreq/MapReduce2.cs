using System;
using System.Collections.Generic;
using System.Linq;
using AISTek.DataFlow.DevSamples.Base;
using AISTek.DataFlow.Sdk.Samples.MapReduce2;
using AISTek.DataFlow.Shared.Japi;
using AISTek.DataFlow.Util;
using File = AISTek.DataFlow.Shared.Japi.File;

namespace AISTek.DataFlow.Sdk.Samples.TextProcessing
{
    public class MapReduce2 : IJobGenerator
    {
        #region IJobGenerator Members

        public void Initialize()
        {
            Console.WriteLine("Enter paths to input files:");
            string str;
            while (true)
            {
                str = Console.ReadLine();
                if (str.Length == 0)
                    break;
                if (System.IO.File.Exists(str))
                    pathes.Add(str);
                else
                    Console.WriteLine("File \"{0}\" doesn't exist!", str);
            }

            Console.WriteLine("Enter amount of top entries: ");
            takeTop = int.Parse(Console.ReadLine());
        }

        public Shared.Japi.Job CreateJob()
        {
            var job = new Job { Name = "MapReduce" };

            var assembly = new UploadAssembly(typeof(Map).Assembly);

            // map
            Console.WriteLine("Generating \"map\" tasks..");
            var fstLayer = new List<Task>();
            var n = 0;
            foreach (var path in pathes)
            {
                fstLayer.Add(new Task
                {
                    EntryPoint = new EntryPoint
                    {
                        Assembly = assembly,
                        ClassName = typeof(Map).FullName
                    },
                    InputFiles = new List<File> { new UploadFilePath(path) },
                    Name = string.Format("MapReduce.Map({0})", n),
                    Parameters = new Dictionary<string, string> 
                    {
                        { "MapReduce.MapIndex", n.ToString() },
                        { "MapReduce.ItemsCount", takeTop.ToString() }
                    },
                    OutputFiles = Sequence.Items(new CreateFile(string.Format("PartialResult_{0}", n)))
                        .Cast<File>()
                        .ToList()
                });
                n++;
            }

            // reduce
            Console.WriteLine("Generating \"reduce\" tasks...");
            var resultTask = new Task
            {
                EntryPoint = new EntryPoint
                {
                    Assembly = assembly,
                    ClassName = typeof(Reduce).FullName
                },
                InputFiles = (from t in fstLayer
                              from f in t.OutputFiles
                              select f).ToList(),
                Name = "MapReduce.Reduce()",
                Parameters = new Dictionary<string, string> { { "MapReduce.ItemsCount", takeTop.ToString() } },
                OutputFiles = new List<File> { new CreateFile("Result") },
                Dependencies = fstLayer
            };

            job.Tasks.AddRange(fstLayer);
            job.Tasks.Add(resultTask);

            return job;
        }

        #endregion

        private readonly List<string> pathes = new List<string>();
        private int takeTop;
    }
}
