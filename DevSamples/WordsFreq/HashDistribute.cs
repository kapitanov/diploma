using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AISTek.DataFlow.DevSamples.Base;
using AISTek.DataFlow.Sdk.Samples.HashDistribute;
using AISTek.DataFlow.Shared.Japi;
using File = AISTek.DataFlow.Shared.Japi.File;

namespace AISTek.DataFlow.Sdk.Samples.TextProcessing
{
    public class HashDistribute : IJobGenerator
    {
        public void Initialize()
        {
            Console.WriteLine("Enter paths to input files:");
            string str;
            while (true)
            {
                str = Console.ReadLine();
                if(str .Length == 0)
                    break;
                if(System.IO.File.Exists(str))
                    pathes.Add(str);
                else
                Console.WriteLine("File \"{0}\" doesn't exist!", str);
            }

            Console.WriteLine("Enter amount of nodes for second layer: ");
            nodes = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter amount of top entries: ");
            takeTop = int.Parse(Console.ReadLine());
        }

        public Job CreateJob()
        {
            var job = new Job { Name = "DF.SDK.ComputeWordFrequencies" };

            var assembly = new UploadAssembly(typeof(BuildWordsList).Assembly);

            // 1st layer:
            Console.WriteLine("Generating 1st layer tasks...");
            var fstLayer = new List<Task>();
            var n = 0;
            foreach(var path in pathes)
            {
                fstLayer.Add(new Task
                                 {
                                     EntryPoint = new EntryPoint
                                                      {
                                                          Assembly = assembly,
                                                          ClassName = typeof(BuildWordsList).FullName
                                                      },
                                     InputFiles = new List<File>
                                                      {
                                                          new UploadFile(path)
                                                      },
                                     Name = string.Format(typeof(BuildWordsList).FullName + "({0})", n),
                                     Parameters = new Dictionary<string, string> { { "Layer.NextSize", nodes.ToString() }, {"Node", n.ToString() } },
                                     OutputFiles = (from target in Enumerable.Range(0, nodes)
                                                    let name = string.Format("HashDistribute_{0}_to_{1})", n, target)
                                                    select new CreateFile(name)
                                                               {
                                                                   Metadata = new Dictionary<string, string>
                                                                                  {
                                                                                      {"Node",target.ToString()}
                                                                                  }
                                                               })
                                         .Cast<File>()
                                         .ToList()
                                 });
                n++;
            }

            // 2nd layer
            Console.WriteLine("Generating 2nd layer tasks...");
            var sndLayer = new List<Task>();
            for(n = 0; n < nodes; n++)
            {
                sndLayer.Add(new Task
                                 {
                                     EntryPoint = new EntryPoint
                                                      {
                                                          Assembly = assembly,
                                                          ClassName = typeof(TakeTopList).FullName
                                                      },
                                     InputFiles = (from t in fstLayer
                                                   from f in t.OutputFiles
                                                   where int.Parse(f.Metadata["Node"]) == n
                                                   select f)
                                         .ToList(),
                                     Name = string.Format(typeof(TakeTopList).FullName + "({0})", n),
                                     Parameters = new Dictionary<string, string> { { "TakeTop", takeTop.ToString() } },
                                     OutputFiles =
                                         new List<File> { new CreateFile(string.Format("TakeTopList_{0}", n)) },
                                     Dependencies = fstLayer
                                 });
            }

            // 3rd layer
            Console.WriteLine("Generating 3rd layer tasks...");
            var resultTask = new Task
                                 {
                                     EntryPoint = new EntryPoint
                                                      {
                                                          Assembly = assembly,
                                                          ClassName = typeof(TakeTopList).FullName
                                                      },
                                     InputFiles = (from t in sndLayer
                                                   from f in t.OutputFiles
                                                   select f)
                                         .ToList(),
                                     Name = typeof(TakeTopList).FullName + "(last)",
                                     Parameters = new Dictionary<string, string> { { "TakeTop", takeTop.ToString() } },
                                     OutputFiles = new List<File> { new CreateFile("Result") },
                                     Dependencies = sndLayer
                                 };

            job.Tasks.AddRange(fstLayer);
            job.Tasks.AddRange(sndLayer);
            job.Tasks.Add(resultTask);

            return job;
        }

        private readonly List<string> pathes = new List<string>();
        private int nodes;
        private int takeTop;
    }
}
