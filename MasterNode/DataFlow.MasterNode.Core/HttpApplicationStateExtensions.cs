using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AISTek.DataFlow.Util;
using Microsoft.Practices.Unity;

namespace AISTek.DataFlow.MasterNode.Core
{
    /// <summary>
    /// Helper class for Unity container instance sharing
    /// </summary>
    internal static class HttpApplicationStateExtensions
    {
        /// <summary>
        /// Gets application container.
        /// </summary>
        /// <param name="application">
        /// Current http application's state.
        /// </param>
        /// <returns>
        /// An instance of <see cref="IUnityContainer"/> for current application.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// This exception is thrown when Unity container provider doesn't exists or 
        /// <see cref="IContainerProvider"/> is not set.
        /// </exception> 
        internal static IUnityContainer GetContainer(this HttpApplicationState application)
        {
            Argument.NotNull(application, "application");

            application.Lock();
            try
            {
                var provider = application[ContainerKey] as IContainerProvider;
                if (provider == null)
                {
                    throw new InvalidOperationException(
                        "Current application doesn't implement {0}.".FormatWith(typeof(IContainerProvider)));
                }

                var container = provider.GetServiceContainer();
                if (container == null)
                {
                    throw new InvalidOperationException("Unity container doesn't exists in current application.");
                }
                return container;
            }
            finally
            {
                application.UnLock();
            }
        }

        /// <summary>
        /// Sets current application's container provider.
        /// </summary>
        /// <param name="application">
        /// Current http application's state.
        /// </param>
        /// <param name="provider">
        /// An instance of <see cref="IContainerProvider"/>
        /// </param>
        internal static void SetContainerProvider(this HttpApplicationState application, IContainerProvider provider)
        {
            Argument.NotNull(application, "application");
            Argument.NotNull(provider, "provider");

            application.Lock();
            try
            {
                application.Add(ContainerKey, provider);
            }
            finally
            {
                application.UnLock();
            }
        }

        private const string ContainerKey = "Unity";
    }
}
