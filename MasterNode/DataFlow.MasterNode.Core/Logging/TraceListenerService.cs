using System;
using System.Linq;

namespace AISTek.DataFlow.MasterNode.Core.Logging
{
    internal class TraceListenerService : ITraceListener, IDisposable
    {
        public TraceListenerService()
        {
            System.Diagnostics.Debug.Listeners.Add(debugListener);
            System.Diagnostics.Debug.Print("System.Diagnostics.Debug: listener of type {0} attached", debugListener.GetType().FullName);
        }

        #region Implementation of ITraceListener

        public IQueryable<Event> Debug
        {
            get
            {
                return debugListener.Events;
            }
        }

        public IQueryable<Event> Trace
        {
            get
            {
                return traceListener.Events;
            }
        }

        #endregion

        #region Private fields

        private readonly CoreTraceListener debugListener = new CoreTraceListener(100);
        private readonly CoreTraceListener traceListener = new CoreTraceListener(100);

        #endregion

        #region Implementation of IDisposable

        /// <summary>
        /// Выполняет определяемые приложением задачи, связанные с удалением, высвобождением или сбросом неуправляемых ресурсов.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            debugListener.Dispose();
            traceListener.Dispose();
        }

        #endregion
    }
}


