using System.ServiceModel;
using System.ServiceModel.Channels;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.Shared.Client
{
    /// <summary>
    /// Factory for clients for <see cref="IJobManager"/> service.
    /// </summary>
    public class JobManagerClientFactory : IJobManagerClientFactory
    {
        /// <summary>
        /// Creates a new instance of the <see cref="IJobManagerClient"/> implementation 
        /// using the specified callback object.
        /// </summary>
        /// <param name="callback">
        /// The callback object to the channel to the service.
        /// </param>
        /// <returns>
        /// A new instance of the <see cref="IJobManagerClient"/> implementation.
        /// </returns>
        public IJobManagerClient CreateClient(IJobManagerCallback callback)
        {
            return new JobManagerClient(new InstanceContext(callback));
        }

        /// <summary>
        /// Creates a new instance of the <see cref="IJobManagerClient"/> implementation 
        /// using the specified callback object and configuration name.
        /// </summary>
        /// <param name="callback">
        /// The callback object to the channel to the service.
        /// </param>
        /// <param name="endpointConfigurationName">
        /// The name of the client endpoint information in the application configuration file.
        /// </param>
        /// <returns>
        /// A new instance of the <see cref="IJobManagerClient"/> implementation.
        /// </returns>
        public IJobManagerClient CreateClient(IJobManagerCallback callback, string endpointConfigurationName)
        {
            return new JobManagerClient(new InstanceContext(callback), endpointConfigurationName);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="IJobManagerClient"/> implementation 
        /// using the specified callback object, configuration name, and service endpoint address.
        /// </summary>
        /// <param name="callback">
        /// The callback object to the channel to the service.
        /// </param>
        /// <param name="endpointConfigurationName">
        /// The name of the client endpoint information in the application configuration file.
        /// </param>
        /// <param name="remoteAddress">
        /// The address of the service endpoint to use.
        /// </param>
        /// <returns>
        /// A new instance of the <see cref="IJobManagerClient"/> implementation.
        /// </returns>
        public IJobManagerClient CreateClient(IJobManagerCallback callback, string endpointConfigurationName, string remoteAddress)
        {
            return new JobManagerClient(new InstanceContext(callback), endpointConfigurationName, remoteAddress);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="IJobManagerClient"/> implementation 
        /// using the specified callback object, configuration name, and service endpoint address.
        /// </summary>
        /// <param name="callback">
        /// The callback object to the channel to the service.
        /// </param>
        /// <param name="endpointConfigurationName">
        /// The name of the client endpoint information in the application configuration file.
        /// </param>
        /// <param name="remoteAddress">
        /// The address of the service endpoint to use.
        /// </param>
        /// <returns>
        /// A new instance of the <see cref="IJobManagerClient"/> implementation.
        /// </returns>
        public IJobManagerClient CreateClient(IJobManagerCallback callback, string endpointConfigurationName, EndpointAddress remoteAddress)
        {
            return new JobManagerClient(new InstanceContext(callback), endpointConfigurationName, remoteAddress);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="IJobManagerClient"/> implementation 
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
        /// A new instance of the <see cref="IJobManagerClient"/> implementation.
        /// </returns>
        public IJobManagerClient CreateClient(IJobManagerCallback callback, Binding binding, EndpointAddress remoteAddress)
        {
            return new JobManagerClient(new InstanceContext(callback), binding, remoteAddress);
        }
    }
}


