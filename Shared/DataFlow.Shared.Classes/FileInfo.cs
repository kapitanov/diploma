using System;
using System.Runtime.Serialization;

namespace AISTek.DataFlow.Shared.Classes
{
    /// <summary>
    /// File info DTO's data contract
    /// </summary>
    [DataContract(Namespace = Namespaces.Scheduler.Namespace)]
    [Serializable]
    public class FileInfo
    {
        /// <summary>
        /// File's unique identifier.
        /// </summary>
        [DataMember(IsRequired = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// File's nonunique name.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Name { get; set; }
    }
}


