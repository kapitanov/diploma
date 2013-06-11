using System;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using AISTek.DataFlow.MasterNode.Core.Logging;
using AISTek.DataFlow.Shared.Classes;
using Microsoft.Practices.Unity;

namespace AISTek.DataFlow.MasterNode.Core.Services
{
    [ServiceBehavior(Namespace = Namespaces.Scheduler.Namespace)]
    public class ServerStatus : IServerStatus
    {
        [InjectionConstructor]
        public ServerStatus(ITraceListener traceListener)
        {
            this.traceListener = traceListener;
        }

        #region Implementation of IServerStatus

        public Version GetServerVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version;
        }

        public Event[] GetAllDebugEvents()
        {
            return (from e in traceListener.Debug select Event.Create(e)).ToArray();
        }

        #endregion

        private readonly ITraceListener traceListener;
    }
}
