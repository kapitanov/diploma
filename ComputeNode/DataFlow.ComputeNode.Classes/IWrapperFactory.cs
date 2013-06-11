using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.ComputeNode.Classes
{
    /// <summary>
    /// Provides methods for creating runtime wrappers for specified interfaces.
    /// Wrappers implements specified interfaces and derives <see cref="System.MarshalByRefObject"/>
    /// in order to be able to pass though <see cref="System.AppDomain"/>s' boundaries.
    /// </summary>
    public interface IWrapperFactory
    {
        /// <summary>
        /// Creates wrapper for <see cref="IRepository"/>.
        /// </summary>
        /// <param name="service">
        /// An instance of <see cref="IRepository"/> to wrap.
        /// </param>
        /// <returns>
        /// An instance of <see cref="IRepository"/> that wraps specified instance of <see cref="IRepositoryService"/>.
        /// </returns>
        IRepository CreateWrapper(IRepository service);
    }
}


