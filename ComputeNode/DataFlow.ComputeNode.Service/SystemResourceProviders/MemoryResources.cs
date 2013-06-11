using System;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.ComputeNode.Service.SystemResourceProviders
{
    internal class MemoryResources : MarshalByRefObject, IMemoryResources
    {
        #region Implementation of IMemoryResources

        /// <summary>
        /// Gets an amount of total memory present in system (in Mb).
        /// </summary>
        public uint TotalMemory { get { return 0; } }

        /// <summary>
        /// Gets an amount of free memory in system availible for runtime (in Mb).
        /// </summary>
        public uint FreeMemory { get { return 0; } }

        #endregion
    }
}


