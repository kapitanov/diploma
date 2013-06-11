using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AISTek.DataFlow.Shared.Classes
{
    /// <summary>
    /// Task entrypoint DTO's data contract
    /// </summary>
    [DataContract(Namespace = Namespaces.Scheduler.Namespace)]
    [Serializable]
    public class EntryPoint
    {
        /// <summary>
        /// Initializes new instance of <see cref="EntryPoint"/>
        /// </summary>
        public EntryPoint()
        {
            QualifiedClassName = string.Empty;
            DependentAssemblyIds = new List<Guid>();
        }

        /// <summary>
        /// Assembly file's unique identifier
        /// </summary>
        [DataMember(IsRequired = true)]
        public Guid AssemblyId { get; set; }

        /// <summary>
        /// String that contains fully qualified name of entry point class.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string QualifiedClassName { get; set; }

        /// <summary>
        /// List of addional assembly dependencies.
        /// </summary>
        [DataMember(IsRequired = true)]
        public List<Guid> DependentAssemblyIds { get; set; }

        /// <summary>
        /// Возвращает объект <see cref="T:System.String"/>, который представляет текущий объект <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// Объект <see cref="T:System.String"/>, представляющий текущий объект <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return string.Format("\"{0}\" in {{{1}}}", QualifiedClassName, AssemblyId);
        }
    }
}


