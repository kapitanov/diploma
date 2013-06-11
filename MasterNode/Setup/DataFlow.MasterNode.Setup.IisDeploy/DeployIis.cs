using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Web.Administration;

namespace AISTek.DataFlow.MasterNode.Setup
{
    public class DeployIis : Deployment
    {
        private DeployIis(string webRoot)
        {
            this.webRoot = webRoot;
        }

        public static DeployIis Init(string webSiteRoot)
        {
            return new DeployIis(webSiteRoot);
        }

        public DeployIis Logger(Action<string> logger)
        {
            InitLog(logger);
            return this;
        }

        private readonly string webRoot;

        public void DeployFromXml(string path)
        {
            var xmlDeploy = XDocument.Load(Path.Combine(path, @"Deploy\deploy.xml"));
            var deploys = from node in xmlDeploy
                 .Descendants("deployment")
                 .Descendants("host")
                 .Descendants("site")
                          let app = node.Descendants("application").First()
                          select new
                                     {
                                         Site = node.Attribute("name").Value,
                                         DefaulAppPool = node.Attribute("pool").Value,
                                         Port = int.Parse(node.Attribute("port").Value),
                                         Application = app.Attribute("name").Value,
                                         AppPool = app.Attribute("pool").Value,
                                     };
            Array.ForEach(deploys.ToArray(), deploy =>
                                 {
                                     WriteToLog("Creating IIS site {0}:{1} at \"{2}\"...",
                                         deploy.Site, deploy.Port, webRoot);
                                     CreateWebSite(webRoot, deploy.Site, deploy.Port, deploy.DefaulAppPool);

                                     WriteToLog("Creating IIS application pool {0} for site {1}:{2}...",
                                         deploy.AppPool, deploy.Site, deploy.Port);
                                     CreateApplicationPool(deploy.AppPool);

                                     WriteToLog("Creating IIS application [{0}]{1}  for site {2}:{3} at \"{4}\"...",
                                         deploy.AppPool, deploy.Application, deploy.Site, deploy.Port, path);
                                     CreateApplication(deploy.Site, path, deploy.Application, deploy.AppPool);
                                 });
        }

        public void CreateWebSite(string path, string name, int port, string appPool)
        {
            var serverManager = new ServerManager();
            if (serverManager.Sites[name] == null)
            {
                var site = serverManager.Sites.Add(name, path, port);
                site.ApplicationDefaults.ApplicationPoolName = appPool;
                site.ServerAutoStart = true;
                serverManager.CommitChanges();
            }
        }

        public void CreateApplicationPool(string appPoolName)
        {
            var serverManager = new ServerManager();
            if (serverManager.ApplicationPools[appPoolName] == null)
            {
                var appPool = serverManager.ApplicationPools.Add(appPoolName);

                appPool.AutoStart = true;
                appPool.Cpu.ResetInterval = TimeSpan.Zero;
                appPool.Cpu.Action = ProcessorAction.NoAction;
                appPool.ManagedPipelineMode = ManagedPipelineMode.Integrated;
                appPool.ManagedRuntimeVersion = "V2.0";
                appPool.ProcessModel.IdentityType = ProcessModelIdentityType.NetworkService;
                appPool.ProcessModel.IdleTimeout = TimeSpan.Zero;
                appPool.ProcessModel.MaxProcesses = 1;
                appPool.ProcessModel.PingingEnabled = false;

                serverManager.CommitChanges();
            }
        }

        public void CreateApplication(string siteName, string path, string name, string applicationPool)
        {
            var serverManager = new ServerManager();
            var site = serverManager.Sites[siteName];
            if (site.Applications[name] == null)
            {
                var app = site.Applications.Add(@"/" + name, path);
                app.ApplicationPoolName = applicationPool;

                serverManager.CommitChanges();
            }
        }
    }
}
