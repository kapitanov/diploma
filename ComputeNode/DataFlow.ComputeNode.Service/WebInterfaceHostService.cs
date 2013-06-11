using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceProcess;
using AISTek.DataFlow.ComputeNode.Service.WebView;
using Microsoft.Practices.Unity;

namespace AISTek.DataFlow.ComputeNode.Service
{
    public sealed class WebInterfaceHostService : ServiceBase
    {
        [InjectionConstructor]
        public WebInterfaceHostService(
            [Dependency]
            IWebInterface webInterface)
        {
            this.webInterface = webInterface;
            ServiceName = "WebInterface host";
            CanStop = true;
            CanPauseAndContinue = false;
            AutoLog = true;
        }

        private readonly IWebInterface webInterface;
        private ServiceHost host;

        protected override void OnStart(string[] args)
        {
            try
            {
                host = new WebServiceHost(webInterface);
                host.Open();
            }
            catch (Exception)
            {
                System.Diagnostics.Debugger.Break();
                throw;
            }
        }

        protected override void OnStop()
        {
            host.Close();
        }
    }
}
