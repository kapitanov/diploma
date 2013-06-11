using System.ServiceModel;
using System.ServiceModel.Channels;
using AISTek.DataFlow.Shared.Classes;
using System;

namespace AISTek.DataFlow.Shared.Client
{
    /// <summary>
    /// Factory for clients for <see cref="IRepositoryService"/> service.
    /// </summary>
    public class RepositoryClientFactory : IRepositoryClientFactory
    {
        #region Implementation of IRepositoryClientFactory

        /// <summary>
        /// Creates a new instance of the <see cref="IRepositoryServiceChannel"/> implementation.
        /// </summary>
        /// <returns>
        /// A new instance of the <see cref="IRepositoryServiceChannel"/> implementation.
        /// </returns>
        public IRepositoryClient CreateClient()
        {
            return Wrap(() => new RepositoryServiceClient());
        }

        /// <summary>
        /// Creates a new instance of the <see cref="IRepositoryServiceChannel"/> implementation 
        /// using the configuration name.
        /// </summary>
        /// <param name="endpointConfigurationName">
        /// The name of the client endpoint information in the application configuration file.
        /// </param>
        /// <returns>
        /// A new instance of the <see cref="IRepositoryServiceChannel"/> implementation.
        /// </returns>
        public IRepositoryClient CreateClient(string endpointConfigurationName)
        {
            return Wrap(() => new RepositoryServiceClient(endpointConfigurationName));
        }

        /// <summary>
        /// Creates a new instance of the <see cref="IRepositoryServiceChannel"/> implementation 
        /// using the specified configuration name, and service endpoint address.
        /// </summary>
        /// <param name="endpointConfigurationName">
        /// The name of the client endpoint information in the application configuration file.
        /// </param>
        /// <param name="remoteAddress">
        /// The address of the service endpoint to use.
        /// </param>
        /// <returns>
        /// A new instance of the <see cref="IRepositoryServiceChannel"/> implementation.
        /// </returns>
        public IRepositoryClient CreateClient(string endpointConfigurationName, string remoteAddress)
        {
            return Wrap(() => new RepositoryServiceClient(endpointConfigurationName, remoteAddress));
        }

        /// <summary>
        /// Creates a new instance of the <see cref="IRepositoryServiceChannel"/> implementation 
        /// using the specified configuration name, and service endpoint address.
        /// </summary>
        /// <param name="endpointConfigurationName">
        /// The name of the client endpoint information in the application configuration file.
        /// </param>
        /// <param name="remoteAddress">
        /// The address of the service endpoint to use.
        /// </param>
        /// <returns>
        /// A new instance of the <see cref="IRepositoryServiceChannel"/> implementation.
        /// </returns>
        public IRepositoryClient CreateClient(string endpointConfigurationName, EndpointAddress remoteAddress)
        {
            return Wrap(() => new RepositoryServiceClient(endpointConfigurationName, remoteAddress));
        }

        /// <summary>
        /// Creates a new instance of the <see cref="IRepositoryServiceChannel"/> implementation 
        /// using the specified binding, and service endpoint address.
        /// </summary>
        /// <param name="binding">
        /// The binding with which to call the service.
        /// </param>
        /// <param name="remoteAddress">
        /// The service endpoint address to use.
        /// </param>
        /// <returns>
        /// A new instance of the <see cref="IRepositoryServiceChannel"/> implementation.
        /// </returns>
        public IRepositoryClient CreateClient(Binding binding, EndpointAddress remoteAddress)
        {
            return Wrap(() => new RepositoryServiceClient(binding, remoteAddress));
        }

        #endregion

        private static IRepositoryClient Wrap(Func<IRepositoryServiceChannel> func)
        {
            return new RepositoryClient(func());
        }
    }
}


