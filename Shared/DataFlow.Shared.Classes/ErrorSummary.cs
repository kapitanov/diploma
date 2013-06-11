using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AISTek.DataFlow.Shared.Classes
{
    [Serializable]
    [DataContract(Namespace = Namespaces.Scheduler.Namespace)]
    public class ErrorSummary
    {
        public ErrorSummary()
        {
            JobName = string.Empty;
            Errors = new List<ErrorReport>();
        }

        public ErrorSummary(Job job, IEnumerable<ErrorReport> errors)
        {
            JobId = job.Id;
            JobName = job.Name;
            Errors = new List<ErrorReport>(errors);
        }

        [DataMember(IsRequired = true)]
        public Guid JobId { get; set; }

        [DataMember(IsRequired = true)]
        public string JobName { get; set; }

        [DataMember(IsRequired = true)]
        public List<ErrorReport> Errors { get; set; }
    }
}
