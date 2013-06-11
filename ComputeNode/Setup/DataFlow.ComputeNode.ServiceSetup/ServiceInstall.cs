using System.ComponentModel;
using System.Configuration.Install;

namespace AISTek.DataFlow.ComputeNode.ServiceSetup
{
    [RunInstaller(true)]
    public sealed class ServiceInstall : Installer
    {
        protected override void OnAfterInstall(System.Collections.IDictionary savedState)
        {
            base.OnAfterInstall(savedState);
          //  Service.Install("");
        }

        protected override void OnBeforeUninstall(System.Collections.IDictionary savedState)
        {
            base.OnBeforeUninstall(savedState);
           // Service.Unistall();
        }

        public override void Install(System.Collections.IDictionary stateSaver)
        {
            System.Diagnostics.Debugger.Break();
            Service.Install("");
            base.Install(stateSaver);
        }

        public override void Commit(System.Collections.IDictionary savedState)
        {
            
            base.Commit(savedState);
        }

        //TARGETDIR
    }
}
