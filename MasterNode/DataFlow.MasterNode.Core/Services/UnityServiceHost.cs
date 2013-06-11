using System;
using System.ServiceModel.Description;
using System.ServiceModel.Web;

namespace AISTek.DataFlow.MasterNode.Core.Services
{
    /// <summary>
    /// WCF service factory which instantiates services using 
    /// current application's Unity container.
    /// </summary>
    public class UnityServiceHost : WebServiceHost
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="System.ServiceModel.ServiceHost"/> class.
        /// </summary>
        public UnityServiceHost()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="System.ServiceModel.ServiceHost"/> class 
        /// with the type of service and its base addresses specified.
        /// </summary>
        /// <param name="serviceType">
        /// The type of hosted service.
        /// </param>
        /// <param name="baseAddresses">
        /// An <see cref="Array"/> of type <see cref="Uri"/> that contains the base addresses for the hosted service.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// serviceType is null.
        /// </exception>
        public UnityServiceHost(Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="System.ServiceModel.ServiceHost"/> class 
        /// with the instance of the service and its base addresses specified.
        /// </summary>
        /// <param name="singletonInstance">
        /// The instance of the hosted service.
        /// </param>
        /// <param name="baseAddresses">
        /// An <see cref="Array"/> of type <see cref="Uri"/> that contains the base addresses for the hosted service.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// singletonInstance is null.
        /// </exception>
        public UnityServiceHost(object singletonInstance, params Uri[] baseAddresses)
            : base(singletonInstance, baseAddresses)
        { }

        #region Overrides of System.ServiceModel.ServiceHost

        protected override void OnOpening()
        {
            AddBehaviour<UnityServiceBehavior>();
            AddBehaviour<ErrorHandlerBehavior>();
            
            base.OnOpening();
        }

        #endregion

        #region Helper methods

        private void AddBehaviour<T>()
                where T: class, IServiceBehavior, new()
        {
            if (Description.Behaviors.Find<T>() == null)
                Description.Behaviors.Add(new T());
        }

        #endregion
    }

}
