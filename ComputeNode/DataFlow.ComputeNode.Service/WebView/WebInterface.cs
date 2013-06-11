using System;
using System.Configuration;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml.Linq;
using AISTek.DataFlow.ComputeNode.Classes;
using Microsoft.Practices.Unity;

namespace AISTek.DataFlow.ComputeNode.Service.WebView
{
    [ServiceBehavior(
        ConfigurationName = "AISTek.DataFlow.ComputeNode.Service.WebView.WebInterface",
        InstanceContextMode = InstanceContextMode.Single)]
    internal class WebInterface : IWebInterface
    {
        public WebInterface(
            [Dependency]
            IExecutionStatus status,
            [Dependency]
            IWebInterfaceBuilder viewBuilder)
        {
            Contract.Requires(status != null);
            Contract.Requires(viewBuilder != null);

            this.status = status;
            this.viewBuilder = viewBuilder;

            viewBuilder.RootPath = 
                Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    "WebView");
        }

        private readonly IExecutionStatus status;
        private readonly IWebInterfaceBuilder viewBuilder;

        #region Implementation of IWebInterface

        public Stream ViewStatus()
        {
            SetHtmlResponse();
            switch (status.Status)
            {
                case Classes.ExecutionStatus.ProcessingTask:
                    if (status.CurrentTask == null)
                        return viewBuilder.BuildErrorView("Status is ExecutionStatus.ProcessingTask, but CurrentTask is null.");
                    return viewBuilder.BuildTaskView(status.StatusMessage, status.CurrentTask);

                case Classes.ExecutionStatus.WaitingForTask:
                    return viewBuilder.BuildNoTaskView(status.StatusMessage);

                default:
                    return viewBuilder.BuildErrorView("Incorrect ExecutionStatus");
            }
        }

        public Stream ViewCss()
        {
            SetCssResponse();
            return viewBuilder.GetCss();
        }

        #endregion

        private static void SetHtmlResponse()
        {
            var webOperationContext = WebOperationContext.Current;
            webOperationContext.OutgoingResponse.ContentType = "text/html";
            webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.OK;    
        }

        private static void SetCssResponse()
        {
            var webOperationContext = WebOperationContext.Current;
            webOperationContext.OutgoingResponse.ContentType = "text/css";
            webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.OK;
        }
    }
}


