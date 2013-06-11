using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace AISTek.DataFlow.ComputeNode.Service
{
    [RunInstaller(true)]
    public class Installer : System.Configuration.Install.Installer
    {
        public Installer()
        {
            processInstaller = new ServiceProcessInstaller
                                   {
                                       Account = ServiceAccount.NetworkService
                                   };
            serviceInstaller = new ServiceInstaller
                                   {
                                       StartType = ServiceStartMode.Manual,
                                       ServiceName = ServiceName,
                                       DisplayName = DisplayName,
                                       Description = Description
                                   };

            Installers.Add(serviceInstaller);
            Installers.Add(processInstaller);
        }

        private const string ServiceName = "DFCNode";
        private const string DisplayName = "AISTek DataFlow ComputeNode";
        private const string Description = "";
        private readonly ServiceProcessInstaller processInstaller;
        private readonly ServiceInstaller serviceInstaller;
    }
}
