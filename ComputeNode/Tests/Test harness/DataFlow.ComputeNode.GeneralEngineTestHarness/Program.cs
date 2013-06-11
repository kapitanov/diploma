using System;
using System.Threading;
using AISTek.DataFlow.ComputeNode.Engine;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.ComputeNode.GeneralEngineTestHarness
{
    static class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Green;

                var engine = new SandboxEngine(typeof (GeneralEngine))
                                 {
                                     SystemResources = new SystemResourcesMock(),
                                     Repository = new RepositoryMock()
                                 };

                var task = new Task
                               {
                                   EntryPoint = new EntryPoint
                                                    {
                                                        AssemblyId = Guid.Empty,
                                                        QualifiedClassName = typeof(TaskMock).AssemblyQualifiedName
                                                    },
                                   Id = Guid.Empty,
                                   Name = "Task name"
                               };

                Console.WriteLine("press any key...");
                Console.ReadLine();

                engine.ExecuteTask(task);

                Console.WriteLine("Done!");
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e);
            }
        }
    }
}


