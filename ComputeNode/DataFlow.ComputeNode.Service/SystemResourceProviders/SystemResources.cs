using System;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.ComputeNode.Service.SystemResourceProviders
{
    public class SystemResources : MarshalByRefObject, ISystemResources
    {
        #region Implementation of ISystemResources

        /// <summary>
        /// Gets an instance of <see cref="ICpuResources"/> implementation that enumerates CPU resources.
        /// </summary>
        public ICpuResources Cpu { get { return new CpuResources(); } }

        /// <summary>
        /// Gets an instance of <see cref="IMemoryResources"/> implementation that enumerates RAM resources.
        /// </summary>
        public IMemoryResources Memory { get { return new MemoryResources(); } }

        /// <summary>
        /// Gets an instance of <see cref="IRuntimeResources"/> implementation that enumerates runtime resources.
        /// </summary>
        public IRuntimeResources Runtime { get { return new RuntimeResources(); } }

        #endregion
    }
}


