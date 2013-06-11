using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using AISTek.DataFlow.MasterNode.Core.Logging;
using AISTek.DataFlow.MasterNode.DataModel;
using AISTek.DataFlow.Util;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace AISTek.DataFlow.MasterNode.Core
{
    /// <summary>
    /// Web application class.
    /// </summary>
    public class CoreApplication : HttpApplication, IContainerProvider, IFileSystemMapper
    {
        string IFileSystemMapper.MapPath(string path)
        {
            return Path.Combine(fileSystemRoot, path);
        }

        #region Event handlers

        protected virtual void Application_Start(object sender, EventArgs e)
        {
            fileSystemRoot = Path.Combine(Server.MapPath("~/"), @"App_Data");

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            
            var unityConfigurationSection = WebConfigurationManager.GetWebApplicationSection(UnitySectionName);
            
            Container = InitializeContainer((UnityConfigurationSection)unityConfigurationSection)
                .RegisterInstance<IContainerProvider>(this);
            
            Application.SetContainerProvider(this);
            Logger.Write(LogCategories.Information(string.Format("AISTek DataFlow core started at {0}", Server.MachineName), LogCategories.Core));
        }

        protected virtual void Application_Error(object sender, EventArgs e)
        {
            HandleApplicationError(Context.Server.GetLastError(), "Server.GetLastError()", "Core policy");
            Context.Server.ClearError();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleApplicationError(e.ExceptionObject as Exception, "AppDomain.UnhandledException", "Core policy");
        }

        private static void HandleApplicationError(Exception exception, string source, string policy)
        {
            var isCriticalError = ExceptionPolicy.HandleException(exception, policy);

            var createLogMessage = isCriticalError
                                       ?
                                           (Func<string, string[], LogEntry>) LogCategories.Critical
                                       :
                                           LogCategories.Error;
            string[] categories = { LogCategories.Core };
            Logger.Write(createLogMessage(
                string.Format(
                    "AISTek DataFlow core catched an exception {0} in {1}.\nException details:\n{2}", 
                    exception.GetType().FullName, source, exception), 
                categories));

            if(isCriticalError)
                throw new CoreApplicationException(string.Format("Critical exception at {0}", source), exception);
        }

        protected virtual void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            var handler = HttpContext.Current.Handler;
            if (handler != null)
            {
                Container.BuildUp(handler.GetType(), handler);
                var page = handler as Page;
                if (page != null)
                {
                    page.PreInit += Page_PreInit;
                    page.InitComplete += Page_InitComplete;
                }
            }
        }

        protected virtual void Page_PreInit(object sender, EventArgs e)
        {
            var page = (Page)sender;
            if (page.Master != null)
                Container.BuildUp(page.Master.GetType(), page.Master);
        }

        protected virtual void Page_InitComplete(object sender, EventArgs e)
        {
            var page = (Page)sender;

            foreach (var control in GetControlsTree(page))
            {
                try
                {
                    Container.BuildUp(control.GetType(), control);
                }
                catch (Exception exc)
                {
                    ExceptionPolicy.HandleException(exc, "Core policy");
                }
            }
        }

        protected virtual void Session_Start(object sender, EventArgs e)
        {

        }

        protected virtual void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected virtual void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected virtual void Session_End(object sender, EventArgs e)
        {

        }

        protected virtual void Application_End(object sender, EventArgs e)
        {
            if (Container != null)
            {
                Container.Dispose();
                Container = null;
            }
        }

        #endregion

        #region Unity container initialization

        private  IUnityContainer InitializeContainer(UnityConfigurationSection configurationSection)
        {
            var container = new UnityContainer();
            RegisterInternalServices(container);
            var containerElement = configurationSection.Containers.Default;
           
            if (containerElement != null)
                containerElement.Configure(container);
            else
                throw new CoreApplicationException("Unable to configure Unity.");
            return container;
        }

        private  void RegisterInternalServices(IUnityContainer container)
        {
            container.RegisterInstance<ITraceListener>(new TraceListenerService());
            container.RegisterInstance<IFileSystemMapper>(this);
        }

        internal static IUnityContainer Container { get; private set; }

        #endregion

        #region Implementation of IContainerProvider

        public IUnityContainer GetServiceContainer()
        {
            return Container;
        }

        #endregion

        #region Internal build-up methods

        internal  void BuildUp(object obj)
        {
            Argument.NotNull(obj, "obj");
            Requires.NotNull(Container, "Container");

            Container.BuildUp(obj.GetType(), obj);
        }

        private static IEnumerable<Control> GetControlsTree(Control root)
        {
            var list = new List<Control>();
            foreach (Control control in root.Controls)
            {
                list.Add(control);
                list.AddRange(GetControlsTree(control));
            }

            return list;
        }

        #endregion

        private const string UnitySectionName = "unity";
        private string fileSystemRoot;
    }
}
