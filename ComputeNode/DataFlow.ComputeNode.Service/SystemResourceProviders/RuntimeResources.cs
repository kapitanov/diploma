using System;
using System.Reflection;
using System.Security;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.ComputeNode.Service.SystemResourceProviders
{
    internal class RuntimeResources : MarshalByRefObject, IRuntimeResources
    {
        #region Implementation of IRuntimeResources

        /// <summary>
        /// Gets version of CLR runtime for current process.
        /// </summary>
        public Version ClrVersion { get { return new Version(3, 5); } }

        /// <summary>
        /// Gets version of DataFlow runtime for current process.
        /// </summary>
        public Version RuntimeVersion { get { return Assembly.GetExecutingAssembly().GetName().Version; } }

        /// <summary>
        /// Gets an instance of <see cref="PermissionSet"/> describing security permissions for current application domain.
        /// </summary>
        public PermissionSet Permissions
        {
            get { return SecurityManager.ResolvePolicy(Assembly.GetExecutingAssembly().Evidence); }
        }

        #endregion
    }
}


