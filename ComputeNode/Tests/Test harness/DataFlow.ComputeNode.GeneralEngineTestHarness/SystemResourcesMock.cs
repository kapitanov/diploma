using System;
using System.Security;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.ComputeNode.GeneralEngineTestHarness
{
    internal class SystemResourcesMock : MarshalByRefObject, ISystemResources
    {
        #region Implementation of ISystemResources

        /// <summary>
        /// Gets an instance of <see cref="ICpuResources"/> implementation that enumerates CPU resources.
        /// </summary>
        public ICpuResources Cpu { get { return new CpuResourcesMock(); } }

        /// <summary>
        /// Gets an instance of <see cref="IMemoryResources"/> implementation that enumerates RAM resources.
        /// </summary>
        public IMemoryResources Memory { get { return new MemoryResourcesMock(); } }

        /// <summary>
        /// Gets an instance of <see cref="IRuntimeResources"/> implementation that enumerates runtime resources.
        /// </summary>
        public IRuntimeResources Runtime { get { return new RuntimeResourcesMock(); } }

        #endregion

        private class CpuResourcesMock : ICpuResources
        {
            #region Implementation of ICpuResources

            /// <summary>
            /// Gets count of processor cores availible for runtime.
            /// </summary>
            public uint CoresCount { get { return 2; } }

            /// <summary>
            /// Gets a value indicating whether current runtime is 32bit X86.
            /// </summary>
            public bool IsX86 { get { return false; } }

            /// <summary>
            /// Gets a value indicating whether current runtime is 64bit X64.
            /// </summary>
            public bool IsX64 { get { return true; } }

            #endregion
        }

        private class MemoryResourcesMock : IMemoryResources
        {
            #region Implementation of IMemoryResources

            /// <summary>
            /// Gets an amount of total memory present in system (in Mb).
            /// </summary>
            public uint TotalMemory { get { return 4 * 1024; } }

            /// <summary>
            /// Gets an amount of free memory in system availible for runtime (in Mb).
            /// </summary>
            public uint FreeMemory { get { return 2 * 1024; } }

            #endregion
        }

        private class RuntimeResourcesMock : IRuntimeResources
        {
            #region Implementation of IRuntimeResources

            /// <summary>
            /// Gets version of CLR runtime for current process.
            /// </summary>
            public Version ClrVersion { get { return Environment.Version; } }

            /// <summary>
            /// Gets version of DataFlow runtime for current process.
            /// </summary>
            public Version RuntimeVersion { get { return Environment.Version; } }

            /// <summary>
            /// Gets an instance of <see cref="PermissionSet"/> describing security permissions for current application domain.
            /// </summary>
            public PermissionSet Permissions { get { return null; } }

            #endregion
        }
    }
}
