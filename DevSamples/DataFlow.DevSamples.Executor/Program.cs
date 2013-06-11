using System;
using System.IO;
using System.Linq;
using System.Reflection;
using AISTek.DataFlow.DevSamples.Base;
using AISTek.DataFlow.Shared.Classes;
using AISTek.DataFlow.Shared.Client;
using AISTek.DataFlow.Shared.Japi;
using Japi = AISTek.DataFlow.Shared.Japi;
using Job = AISTek.DataFlow.Shared.Classes.Job;

namespace AISTek.DataFlow.DevSamples.Executor
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("AITek DataFlow XRun");
            try
            {
                var gType = GetGeneratorType(args);
                if(gType == null)
                    return;

                var generator = (IJobGenerator)Activator.CreateInstance(gType);
                generator.Initialize();
                var job = generator.CreateJob();
                ExecuteJob(job);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error!");
                Console.WriteLine(e.ToString());
            }
        }

        private static Type GetGeneratorType(string[] args)
        {
            if(args.Length <= 0)
            {
                Console.WriteLine("Usage: xrun jobFile.dll [classname]");
                return null;
            }

            var assembly = Assembly.LoadFile(Path.Combine(Directory.GetCurrentDirectory(), string.Format("{0}.dll", args[0])));
            var types = (from t in assembly.GetTypes()
                         let targetType = typeof(IJobGenerator)
                         where (from i in t.GetInterfaces()
                                where i == targetType
                                select i).Count() > 0
                         select t).ToList();
            if(types.Count == 0)
            {
                Console.WriteLine("Assembly {0} doesn't contain any valid implementations of {1}",
                                  assembly.FullName, typeof(IJobGenerator).FullName);
                return null;
            }

            if(types.Count == 1)
            {
                return types.First();
            }

            if(args.Length > 1)
            {
                var ts = (from t in types
                          where t.FullName.EndsWith(args[1], StringComparison.InvariantCultureIgnoreCase)
                          select t).ToList();
                if(ts.Count == 0)
                {
                    Console.WriteLine("Assembly {0} doesn't contain any valid implementations of {1} with name {2}",
                        assembly.FullName, typeof(IJobGenerator).FullName, args[1]);
                    return null;
                }

                return ts.First();
            }

            Console.WriteLine("There are {0} valid impementations of {1} in assembly {2}:",
                types.Count, typeof(IJobGenerator).FullName, assembly.FullName);
            foreach(var type in types)
            {
                Console.WriteLine("   " + type.FullName);
            }

            Console.WriteLine("Specify an implementation using syntax: xrun jobFile.dll jobClass");
            return null;
        }

        private static void ExecuteJob(Japi.Job job)
        {
            var stagesProvider = new StagesProvider();
            var repositoryClientFactory = new RepositoryClientFactory();
            var jobManagerClientFactory = new JobManagerClientFactory();

            using(var repository = repositoryClientFactory.CreateClient())
            {
                using(var jobManager = jobManagerClientFactory.CreateClient(new JobManagerCallback()))
                {
                    foreach(var stage in stagesProvider.Stages)
                    {
                        Console.WriteLine(stage.Name + "...");
                        stage.Execute(job, Console.WriteLine, repository, jobManager);
                    }    
                }
            }
        }

        private class JobManagerCallback : IJobManagerCallback
        {
            public void JobCompleted(Job job)
            {
                Console.WriteLine("Callback: job completed.");
            }

            public void JobFailed(Job job)
            {
                Console.WriteLine("Callback: job failed.");
            }
        }
    }
}
