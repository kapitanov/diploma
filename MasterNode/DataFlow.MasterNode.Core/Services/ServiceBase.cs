using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace AISTek.DataFlow.MasterNode.Core.Services
{
    public abstract class ServiceBase : IDisposable
    {
        protected ServiceBase()
        {
            //var operationContext = OperationContext.Current;
            //var clientEndpoint = operationContext.IncomingMessageProperties[
            //                         RemoteEndpointMessageProperty.Name]
            //                     as RemoteEndpointMessageProperty;

            //RemoteAddress = string.Format(
            //    "{0}:{1}",
            //    clientEndpoint.Address, clientEndpoint.Port);
            RemoteAddress = "Unknown remote address";
        
        }

        /// <summary>
        /// This method is calld when service is terminating and service instance object is being disposed.
        /// </summary>
        protected virtual void OnServiceTerminating()
        {}


        #region Implementation of IDisposable

        /// <summary>
        /// Выполняет определяемые приложением задачи, связанные с удалением, высвобождением или сбросом неуправляемых ресурсов.
        /// </summary>
        public void Dispose()
        {
            OnServiceTerminating();
        }

        #endregion

        /// <summary>
        /// Gets a formatted string that contains string representation of remote endpoint.
        /// </summary>
        protected string RemoteAddress { get; private set; }
    }
}
 