using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace AISTek.DataFlow.MasterNode.Setup
{
    [RunInstaller(true)]
    public class Installer : System.Configuration.Install.Installer
    {
        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver);
            var s = Context.Parameters["dir"];

            var targetDir = s.Substring(0, s.Length - 1);
            var module = Context.Parameters["module"];
            var moduleDir = Path.Combine(targetDir, module);
            var server = Context.Parameters["server"];
            var uid = Context.Parameters["uid"];
            var pwd = Context.Parameters["pwd"];

            Deploy(targetDir, moduleDir, server, uid, pwd);
        }

        private void Deploy(string webSiteRoot, string path, string server, string uid, string pwd)
        {
            //Action<string> logger = Context.LogMessage;
            Action<string> logger = str => MessageBox.Show(str);

            DeployIis.Init(webSiteRoot)
                     .Logger(logger)
                     .DeployFromXml(path);
            DeployDatabase.Init(server, uid, pwd)
                          .Logger(logger)
                          .DeployFromXml(path);
            DeployEventLog.Init()
                          .Logger(logger)
                          .DeployFromXml(path);
        }
    }
}
