using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AISTek.DataFlow.Shared.Classes.RepositoryService
{
    /// <summary>
    /// Remote file DTO contract.
    /// </summary>
    [DataContract(Namespace = Namespaces.Scheduler.Namespace)]
    [Serializable]
    public sealed class RemoteFileInfo
    {
        /// <summary>
        /// File's unique identifier.
        /// </summary>
        [DataMember(IsRequired = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// File's non-unique name.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Name { get; set; }

        /// <summary>
        /// File's UNC URI.
        /// </summary>
        [DataMember(IsRequired = true)]
        public Uri Uri { get; set; }
    }
}
