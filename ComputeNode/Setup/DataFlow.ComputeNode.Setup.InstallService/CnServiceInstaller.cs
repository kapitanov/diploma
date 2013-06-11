using System.ComponentModel;
using System.Configuration.Install;

namespace AISTek.DataFlow.ComputeNode.Setup.InstallService
{
    [RunInstaller(true)]
    public class CnServiceInstaller : Installer
    {
        protected override void OnAfterInstall(System.Collections.IDictionary savedState)
        {
            base.OnAfterInstall(savedState);

            var targetDir = savedState["TARGETDIR"];

        }
    }
}
