using AISTek.DataFlow.ComputeNode.Classes;
using AISTek.DataFlow.ComputeNode.Service.Wrappers;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.ComputeNode.Service
{
    /// <summary>
    /// Implements methods for creating runtime wrappers for specified interfaces.
    /// </summary>
    public class WrapperFactory : IWrapperFactory
    {
        #region Implementation of IWrapperFactory

        /// <summary>
        /// Creates wrapper for <see cref="IRepositoryService"/>.
        /// </summary>
        /// <param name="service">
        /// An instance of <see cref="IRepositoryService"/> to wrap.
        /// </param>
        /// <returns>
        /// An instance of <see cref="IRepositoryService"/> that wraps specified instance of <see cref="IRepositoryService"/>.
        /// </returns>
        public IRepository CreateWrapper(IRepository service)
        {
            return new RepositoryWrapper(service);
        }

        #endregion
    }
}
