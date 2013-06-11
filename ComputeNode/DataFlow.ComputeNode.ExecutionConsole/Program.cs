using System;
using System.Configuration;
using System.Diagnostics;
using AISTek.DataFlow.ComputeNode.Classes;
using AISTek.DataFlow.ComputeNode.Service;
using AISTek.DataFlow.PresentationExtensions;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using ExecutionStatus = AISTek.DataFlow.ComputeNode.Classes.ExecutionStatus;

namespace AISTek.DataFlow.ComputeNode.ExecutionConsole
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Console.Title = "Execution console";
            using(ConsoleMessage.Highlight())
                Console.WriteLine("AISTek DataFlow ComputeNode\nExecution console\n");
            try
            {
                using(ConsoleMessage.Info())
                    Console.WriteLine("Loading configuration...");

                var container = InitializeContainer();
                IExecutionService executionService = container.Resolve<ExecutionService>();
                var status = container.Resolve<IExecutionStatus>();
                status.OnStatusMessageChanged += (_, e) =>
                {
                    using(ConsoleMessage.Info())
                        Console.WriteLine("[{0}]:  {1}", DateTime.Now, e.NewStatusMessage);
                };
                status.OnStatusChanged += (_, e) =>
                {
                    switch(e.NewStatus)
                    {
                    case ExecutionStatus.ProcessingTask:
                        using(ConsoleMessage.Info())
                        {
                            Console.WriteLine("[{0}]: Execution of task {1} started.", DateTime.Now, status.CurrentTask);
                            if(idleWatch != null)
                            {
                                idleWatch.Stop();
                                Console.WriteLine("Idle time: {0} ms", idleWatch.ElapsedMilliseconds);
                            }
                            processWatch = Stopwatch.StartNew();
                        }
                        break;
                    case ExecutionStatus.WaitingForTask:
                        using(ConsoleMessage.Info())
                        {
                            Console.WriteLine("[{0}]: Execution core is idle.", DateTime.Now);
                            idleWatch = Stopwatch.StartNew();
                            if(processWatch != null)
                            {
                                processWatch.Stop();
                                Console.WriteLine("Execution time: {0} ms", processWatch.ElapsedMilliseconds);
                            }
                        }

                        break;
                    default:
                        using(ConsoleMessage.Info())
                            Console.WriteLine("[{0}]: Execution service status changed into {1}", DateTime.Now, e.NewStatus);
                        break;
                    }
                };

                using(ConsoleMessage.Info())
                    Console.WriteLine("Starting execution service...");

                executionService.Start();

                using(ConsoleMessage.Info())
                    Console.WriteLine("Execution service is online.");
                while(true)
                {
                    using(ConsoleMessage.Highlight())
                        Console.Write(">");

                    var parser = new CommandParser();
                    var command = parser.Parse(Console.ReadLine());
                    switch(command.Name)
                    {
                    case "exit":
                        using(ConsoleMessage.Info())
                            Console.WriteLine("Stopping execution service...");
                        executionService.Stop();
                        return;
                    case "restart":
                        using(ConsoleMessage.Info())
                            Console.WriteLine("Stopping execution service...");
                        executionService.Stop();
                        using(ConsoleMessage.Info())
                            Console.WriteLine("Restarting execution service...");
                        executionService.Start();
                        return;
                    default:
                        using(ConsoleMessage.Warning())
                            Console.WriteLine("Command \"{0}\" is not recognized", command.Name);
                        break;
                    }
                }
            }
            catch(Exception e)
            {
                using(ConsoleMessage.Error())
                    Console.WriteLine(" *** ERROR ***\n{0}", e);
                System.Diagnostics.Debugger.Break();
                throw;
            }
        }

        private static IUnityContainer InitializeContainer()
        {
            var container = new UnityContainer();
            var unityConfigurationSection = ConfigurationManager.GetSection(UnitySectionName)
                                            as UnityConfigurationSection;

            var containerElement = unityConfigurationSection.Containers.Default;
            if(containerElement != null)
                containerElement.Configure(container);
            else
                throw new ConfigurationErrorsException("Micosoft Unity configuration is not found.");

            container.RegisterInstance<IUnityContainer>(container);

            return container;
        }

        private static Stopwatch idleWatch;
        private static Stopwatch processWatch;

        private const string UnitySectionName = "unity";
    }
}

