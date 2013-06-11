using System;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.ComputeNode.Service.SystemResourceProviders
{
    internal class CpuResources : MarshalByRefObject, ICpuResources
    {
        #region Implementation of ICpuResources

        /// <summary>
        /// Gets count of processor cores availible for runtime.
        /// </summary>
        public uint CoresCount{get { return (uint) Environment.ProcessorCount; }}

        /// <summary>
        /// Gets a value indicating whether current runtime is 32bit X86.
        /// </summary>
        public bool IsX86 { get { return is32Bit; } }

        /// <summary>
        /// Gets a value indicating whether current runtime is 64bit X64.
        /// </summary>
        public bool IsX64 { get { return !is32Bit; } }

        #endregion

        private readonly bool is32Bit = IntPtr.Size == 4;
    }
}


