using System.ServiceModel;
using System.ServiceModel.Channels;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.Shared.Client
{
    /// <summary>
    /// Defines methods for instancing clients for <see cref="IRepositoryService"/> service.
    /// </summary>
    public interface IRepositoryClientFactory
    {
        /// <summary>
        /// Creates a new instance of the <see cref="IRepositoryServiceChannel"/> implementation.
        /// </summary>
        /// <returns>
        /// A new instance of the <see cref="IRepositoryServiceChannel"/> implementation.
        /// </returns>
        IRepositoryClient CreateClient();

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
        IRepositoryClient CreateClient(string endpointConfigurationName);

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
        IRepositoryClient CreateClient(string endpointConfigurationName, string remoteAddress);

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
        IRepositoryClient CreateClient(string endpointConfigurationName, EndpointAddress remoteAddress);

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
        IRepositoryClient CreateClient(Binding binding, EndpointAddress remoteAddress);
    }
}


