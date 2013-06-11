using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AISTek.DataFlow.Shared.Classes
{
    /// <summary>
    /// Task DTO's data contract
    /// </summary>
    [DataContract(Namespace = Namespaces.Scheduler.Namespace)]
    [Serializable]
    public class Task
    {
        /// <summary>
        /// Initializes new instance of <see cref="Task"/>
        /// </summary>
        public Task()
        {
            EntryPoint = new EntryPoint();
            InputFiles = new List<FileLink>();
            OutputFiles = new List<FileLink>();
            Inputs = new List<Task>();
            Outputs = new List<Task>();
            Parameters = new Dictionary<string, string>();
        }

        /// <summary>
        /// Task's unique identifier
        /// </summary>
        [DataMember(IsRequired = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Task's unique identifier
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Name { get; set; }

        /// <summary>
        /// Task's entrypoint definition.
        /// </summary>
        [DataMember(IsRequired = true)]
        public EntryPoint EntryPoint { get; set; }

        /// <summary>
        /// List of input file links.
        /// </summary>
        [DataMember(IsRequired = true)]
        public List<FileLink> InputFiles { get; set; }

        /// <summary>
        /// List of output file links.
        /// </summary>
        [DataMember(IsRequired = true)]
        public List<FileLink> OutputFiles { get; set; }

        /// <summary>
        /// List of input tasks.
        /// </summary>
        [DataMember]
        public List<Task> Inputs { get; set; }

        /// <summary>
        /// List of output tasks.
        /// </summary>
        [DataMember]
        public List<Task> Outputs { get; set; }

        /// <summary>
        /// Addional task's parameters.
        /// </summary>
        [DataMember(IsRequired = true)]
        public Dictionary<string, string> Parameters { get; set; }

        /// <summary>
        /// Возвращает объект <see cref="T:System.String"/>, который представляет текущий объект <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// Объект <see cref="T:System.String"/>, представляющий текущий объект <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return string.Format("(\"{0}\", {{{1}}})", Name, Id);
        }
    }
}


