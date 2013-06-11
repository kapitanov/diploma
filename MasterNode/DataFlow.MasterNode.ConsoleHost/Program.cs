using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using AISTek.DataFlow.MasterNode.DataModel;
using AISTek.DataFlow.MasterNode.Repository;
using AISTek.DataFlow.MasterNode.Scheduler;
using AISTek.DataFlow.PresentationExtensions;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace AISTek.DataFlow.MasterNode.ConsoleHost
{
    public class Program : Core.IFileSystemMapper
    {
        static void Main(string[] args)
        {
            using (ConsoleMessage.Highlight())
                Console.WriteLine("Starting host...");
            var program = new Program(args);
            program.Initialize();
            program.Startup();
        }

        private Program(string[] args)
        { }

        public void Initialize()
        {
            var unityConfigurationSection = ConfigurationManager.GetSection(UnitySectionName);
            Container = InitializeContainer((UnityConfigurationSection)unityConfigurationSection);
            Logger.Write(LogCategories.Information("AISTek DataFlow MasterNode host started.", LogCategories.Core));
            fileSystemRoot =
                Path.Combine(
                Directory.GetParent(
                Directory.GetCurrentDirectory()).FullName, @"data");

            Trace.Listeners.Add(new Tracing.TraceListener());
            Debug.Listeners.Add(new ConsoleTraceListener());
        }

        public void Startup()
        {
            var repositoryBaseUri = new Uri("http://localhost:8080/Repository/Repository.svc");
            var taskProviderBaseUri = new Uri("http://localhost:8080/Scheduler/TaskProvider.svc");
            var jobManagerBaseUri = new Uri("http://localhost:8080/Scheduler/JobManager.svc");

            using (var repositoryHost = new UnityServiceHost(typeof(RepositoryService), repositoryBaseUri))
           // using (var repositoryHost = new UnityServiceHost(Container.Resolve<RepositoryService>(), repositoryBaseUri))
            using (var taskProviderHost = new UnityServiceHost(typeof(TaskProvider), taskProviderBaseUri))
            using (var jobManagerHost = new UnityServiceHost(typeof(JobManager), jobManagerBaseUri))
            {
                repositoryHost.Open();
                Console.WriteLine("Repository service is started on \"{0}\"", repositoryBaseUri);

                taskProviderHost.Open();
                Console.WriteLine("Task provider service is started on \"{0}\"", taskProviderBaseUri);

                jobManagerHost.Open();
                Console.WriteLine("Job manager service is started on \"{0}\"", jobManagerBaseUri);


                Console.ReadLine();
            }
        }


        #region Unity container initialization

        private IUnityContainer InitializeContainer(UnityConfigurationSection configurationSection)
        {
            var container = new UnityContainer();
            RegisterInternalServices(container);
            var containerElement = configurationSection.Containers.Default;

            if (containerElement != null)
                containerElement.Configure(container);
            else
                throw new Core.CoreApplicationException("Unable to configure Unity.");
            return container;
        }

        private void RegisterInternalServices(IUnityContainer container)
        {
            container.RegisterInstance<Core.IFileSystemMapper>(this);
        }

        internal static IUnityContainer Container { get; private set; }

        #endregion

        private const string UnitySectionName = "unity";
        private string fileSystemRoot;

        public string MapPath(string path)
        {
            return Path.Combine(fileSystemRoot, path);
        }
    }
}
