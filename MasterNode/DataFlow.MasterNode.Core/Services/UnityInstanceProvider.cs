using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using AISTek.DataFlow.Util;

namespace AISTek.DataFlow.MasterNode.Core.Services
{
    /// <summary>
    /// Initializes instances of WCF services using current application's Unity container
    /// </summary>
    internal class UnityInstanceProvider : IInstanceProvider
    {
        public UnityInstanceProvider(Type serviceType)
        {
            this.serviceType = serviceType;
        }

        #region Implementation of IInstanceProvider

        /// <summary>
        /// Returns a service object given the specified <see cref="T:System.ServiceModel.InstanceContext" /> object.
        /// </summary>
        /// <param name="instanceContext">
        /// The current <see cref="T:System.ServiceModel.InstanceContext" /> object.
        /// </param>
        /// <returns>
        /// A user-defined service object.
        /// </returns>
        public object GetInstance(InstanceContext instanceContext)
        {
            return GetInstance(instanceContext, null);
        }

        /// <summary>
        /// Returns a service object given the specified <see cref="T:System.ServiceModel.InstanceContext" /> object.
        /// </summary>
        /// <param name="instanceContext">
        /// The current <see cref="T:System.ServiceModel.InstanceContext" /> object.
        /// </param>
        /// <param name="message">
        /// The message that triggered the creation of a service object.
        /// </param>
        /// <returns>
        /// The service object.
        /// </returns>
        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            var instance = CoreApplication.Container.Resolve(serviceType);
            return instance;
        }

        /// <summary>
        /// Called when an <see cref="T:System.ServiceModel.InstanceContext" /> object recycles a service object.
        /// </summary>
        /// <param name="instanceContext">
        /// The service's instance context.
        /// </param>
        /// <param name="instance">
        /// The service object to be recycled.
        /// </param>
        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        { }

        #endregion

        private readonly Type serviceType;
    }

}
