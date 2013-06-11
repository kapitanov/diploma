using System;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace AISTek.DataFlow.MasterNode.Core.Services
{
    /// <summary>
    /// A WCF service factory which allows to use Unity to create service instances.
    /// </summary>
    public class ServiceFactory : WebServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            return new UnityServiceHost(serviceType, baseAddresses);
        }
    }
}
