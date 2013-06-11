using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AISTek.DataFlow.Shared.Classes
{
    /// <summary>
    /// Represents a job as a set of tasks.
    /// </summary>
    [DataContract(Namespace = Namespaces.Scheduler.Namespace)]
    [Serializable]
    public class Job
    {
        /// <summary>
        /// Job's unique identifier
        /// </summary>
        [DataMember(IsRequired = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Job's friendly name.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Name { get; set; }

        /// <summary>
        /// List of all tasks that belongs to current job.
        /// </summary>
        [DataMember(IsRequired = true)]
        public List<Task> Tasks { get; set; }

        /// <summary>
        /// Gets and sets job state.
        /// </summary>
        [DataMember(IsRequired = true)]
        public JobState State { get; set; }
    }
}


