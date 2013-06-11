using System.ServiceModel;
using System.ServiceModel.Channels;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.Shared.Client
{
    /// <summary>
    /// Factory for clients for <see cref="ITaskProvider"/> service.
    /// </summary>
    public class TaskProviderClientFactory : ITaskProviderClientFactory
    {
        #region Implementation of ITaskProviderClientFactory

        /// <summary>
        /// Creates a new instance of the <see cref="ITaskProviderChannel"/> implementation 
        /// using the specified callback object.
        /// </summary>
        /// <returns>
        /// A new instance of the <see cref="ITaskProviderChannel"/> implementation.
        /// </returns>
        public ITaskProviderChannel CreateClient()
        {
            return new TaskProviderClient();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ITaskProviderChannel"/> implementation 
        /// using the specified callback object and configuration name.
        /// </summary>
        /// <param name="endpointConfigurationName">
        /// The name of the client endpoint information in the application configuration file.
        /// </param>
        /// <returns>
        /// A new instance of the <see cref="ITaskProviderChannel"/> implementation.
        /// </returns>
        public ITaskProviderChannel CreateClient(string endpointConfigurationName)
        {
            return new TaskProviderClient(endpointConfigurationName);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ITaskProviderChannel"/> implementation 
        /// using the specified callback object, configuration name, and service endpoint address.
        /// </summary>
        /// <param name="endpointConfigurationName">
        /// The name of the client endpoint information in the application configuration file.
        /// </param>
        /// <param name="remoteAddress">
        /// The address of the service endpoint to use.
        /// </param>
        /// <returns>
        /// A new instance of the <see cref="ITaskProviderChannel"/> implementation.
        /// </returns>
        public ITaskProviderChannel CreateClient(string endpointConfigurationName, string remoteAddress)
        {
            return new TaskProviderClient(endpointConfigurationName, remoteAddress);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ITaskProviderChannel"/> implementation 
        /// using the specified callback object, configuration name, and service endpoint address.
        /// </summary>
        /// <param name="endpointConfigurationName">
        /// The name of the client endpoint information in the application configuration file.
        /// </param>
        /// <param name="remoteAddress">
        /// The address of the service endpoint to use.
        /// </param>
        /// <returns>
        /// A new instance of the <see cref="ITaskProviderChannel"/> implementation.
        /// </returns>
        public ITaskProviderChannel CreateClient(string endpointConfigurationName, EndpointAddress remoteAddress)
        {
            return new TaskProviderClient(endpointConfigurationName, remoteAddress);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ITaskProviderChannel"/> implementation 
        /// using the specified callback object, binding, and service endpoint address.
        /// </summary>
        /// <param name="callback">
        /// The callback object to the channel to the service.
        /// </param>
        /// <param name="binding">
        /// The binding with which to call the service.
        /// </param>
        /// <param name="remoteAddress">
        /// The service endpoint address to use.
        /// </param>
        /// <returns>
        /// A new instance of the <see cref="ITaskProviderChannel"/> implementation.
        /// </returns>
        public ITaskProviderChannel CreateClient(Binding binding, EndpointAddress remoteAddress)
        {
            return new TaskProviderClient(binding, remoteAddress);
        }

        #endregion
    }
}


