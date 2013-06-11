using Microsoft.Practices.Unity;

namespace AISTek.DataFlow.MasterNode.Core
{
    /// <summary>
    /// Unity container instance provider
    /// </summary>
    internal interface IContainerProvider
    {
        /// <summary>
        /// Gets an instance of <see cref="IUnityContainer"/>.
        /// </summary>
        /// <returns>
        /// An instance of <see cref="IUnityContainer"/>.
        /// </returns>
        IUnityContainer GetServiceContainer();
    }
}
