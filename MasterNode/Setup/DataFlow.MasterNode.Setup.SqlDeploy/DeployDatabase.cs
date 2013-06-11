using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace AISTek.DataFlow.MasterNode.Setup
{
    public sealed class DeployDatabase : Deployment
    {
        private DeployDatabase(string server, string uid, string pwd)
        {
            Server = server;
            UserId = uid;
            Password = pwd;
        }

        public static DeployDatabase Init(string server, string uid, string pwd)
        {
            return new DeployDatabase(server, uid, pwd);
        }

        public DeployDatabase Logger(Action<string> logger)
        {
            InitLog(logger);
            return this;
        }

        public string Server { get; private set; }
        public string UserId { get; private set; }
        public string Password { get; private set; }

        public void DeployFromXml(string path)
        {
            var webConfigPath = Path.Combine(path, @"web.config");
            var webConfig = XDocument.Load(webConfigPath);

            var settings = webConfig
                .Descendants("configuration")
                .Descendants("connectionStrings")
                .Descendants("add");

            var xmlDeploy = XDocument.Load(Path.Combine(path, @"Deploy\deploy.xml"));
            var deploys = xmlDeploy
                .Descendants("deployment")
                .Descendants("database")
                .Descendants("deploy")
                .Select(node => new
                                    {
                                        Database = node.Attribute("database").Value,
                                        ConfigItem = node.Attribute("config").Value,
                                        Script = node.Descendants("sql").Select(sql => sql.Value).ToList()
                                    });
            Array.ForEach(deploys.ToArray(), deploy =>
                         {
                             WriteToLog("Deploying database \"{0}\"...", deploy.Database);
                             DeployFromQuery(deploy.Script);

                             WriteToLog("Configuring database \"{0}\"...", deploy.Database);
                             UpdateConfiguration(settings, deploy.ConfigItem, deploy.Database);
                         });
            webConfig.Save(webConfigPath);
        }

        private void UpdateConfiguration(IEnumerable<XElement> elements, string item, string dababase)
        {
            var connectionString = new SqlConnectionStringBuilder
            {
                DataSource = Server,
                InitialCatalog = dababase,
                UserID = UserId,
                Password = Password
            }.ConnectionString;

            elements
                .Where(node => node.Attribute("name").Value == item)
                .First()
                .SetAttributeValue("connectionString", connectionString);
        }

        public void DeployFromQuery(IEnumerable<string> query)
        {
            var connectionString = new SqlConnectionStringBuilder
                         {
                             DataSource = Server,
                             InitialCatalog = "master",
                             UserID = UserId,
                             Password = Password
                         }.ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        command.CommandType = CommandType.Text;
                        foreach(var queryLine in query)
                        {
                            command.CommandText = queryLine;
                            command.ExecuteNonQuery();
                        }
                    }
                    catch(Exception e)
                    {
                        WriteToLog("SQL deployment error\n{0}", e.ToString());
                    }
                }
            }
        }

        public void DeployFromScript(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("Unable to open deployment script.", path);

            using (var reader = File.OpenText(path))
            {
                var query = reader.ReadToEnd();
                DeployFromQuery(new[] { query });
            }
        }
    }
}
