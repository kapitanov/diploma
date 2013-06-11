using System;
using System.Configuration;
using System.ServiceProcess;
using System.Windows.Forms;
using AISTek.DataFlow.ComputeNode.Service.WebView;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace AISTek.DataFlow.ComputeNode.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            try
            {
                using (var container = InitializeContainer())
                {
                    var servicesToRun = new ServiceBase[]
                                        {
                                            container.Resolve<ExecutionService>(),
                                            container.Resolve<WebInterfaceHostService>(),
                                        };
                    ServiceBase.Run(servicesToRun);
                }
            }
            catch (Exception e)
            {
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
            if (containerElement != null)
                containerElement.Configure(container);
            else
                throw new ConfigurationErrorsException("Micosoft Unity configuration is not found.");

            container.RegisterInstance(typeof(Type), "WebHostType", typeof(WebInterface));
            container.RegisterInstance<IUnityContainer>(container);

            return container;
        }

        private const string UnitySectionName = "unity";
    }
}