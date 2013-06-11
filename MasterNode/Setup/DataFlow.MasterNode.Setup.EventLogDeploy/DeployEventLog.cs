using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace AISTek.DataFlow.MasterNode.Setup
{
    public class DeployEventLog : Deployment
    {
        private DeployEventLog()
        { }

        public static DeployEventLog Init()
        {
            return new DeployEventLog();
        }

        public DeployEventLog Logger(Action<string> logger)
        {
            InitLog(logger);
            return this;
        }

        public void DeployFromXml(string path)
        {
            var xmlDeploy = XDocument.Load(Path.Combine(path, @"Deploy\deploy.xml"));
            var deploys = from node in xmlDeploy.Descendants("deployment")
                              .Descendants("eventLog")
                              .Descendants("log")
                          select new
                                     {
                                         LogName = node.Attribute("name").Value,
                                         Source = node.Attribute("source").Value,
                                     };
            Array.ForEach(deploys.ToArray(), deploy =>
            {
                WriteToLog("Creating log source \"{0}\" for eventlog \"{1}\"...", deploy.Source, deploy.LogName);
                if (EventLog.SourceExists(deploy.Source))
                    WriteToLog("Eventlog source \"{0}\" already exists.", deploy.Source);
                else
                    EventLog.CreateEventSource(deploy.Source, deploy.LogName);
            });
        }
    }
}
