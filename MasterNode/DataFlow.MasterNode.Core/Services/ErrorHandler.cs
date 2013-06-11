using System;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using AISTek.DataFlow.MasterNode.DataModel;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace AISTek.DataFlow.MasterNode.Core.Services
{
    internal sealed class ErrorHandler : IErrorHandler
    {
        #region Implementation of IErrorHandler

        /// <summary>
        /// Enables the creation of a custom <see cref="T:System.ServiceModel.FaultException`1"/> that is returned from an exception in the course of a service method.
        /// </summary>
        /// <param name="error">
        /// The <see cref="T:System.Exception"/> object thrown in the course of the service operation.
        /// </param>
        /// <param name="version">
        /// The SOAP version of the message.</param>
        /// <param name="fault">
        /// The <see cref="T:System.ServiceModel.Channels.Message"/> object that is returned to the client, or service, in the duplex case.
        /// </param>
        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            // Shield the unknown exception  
            var faultException = new FaultException("Server error encountered. All details have been logged.");
            var messageFault = faultException.CreateMessageFault();

            fault = Message.CreateMessage(version, messageFault, faultException.Action);  
        }

        /// <summary>
        /// Enables error-related processing and returns a value that indicates whether the dispatcher aborts the session and the instance context in certain cases. 
        /// </summary>
        /// <returns>
        /// true if Windows Communication Foundation (WCF) should not abort the session (if there is one) and instance context if the instance context is 
        /// not <see cref="F:System.ServiceModel.InstanceContextMode.Single"/>; otherwise, false. The default is false.
        /// </returns>
        /// <param name="error">
        /// The exception thrown during processing.
        /// </param>
        public bool HandleError(Exception error)
        {
            Debug.Print("Service error: {0} | {1}", error.GetType(), error);
            if(Debugger.IsAttached)
                Debugger.Break();
            Logger.Write(LogCategories.Error(error.ToString(), LogCategories.Core));
            return ExceptionPolicy.HandleException(error, PolicyName);
        }

        #endregion

        private const string PolicyName = "Service policy";
    }
}
