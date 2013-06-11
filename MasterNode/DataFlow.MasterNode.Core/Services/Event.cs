using System;
using System.Runtime.Serialization;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.MasterNode.Core.Services
{
    [DataContract(Namespace = Namespaces.Scheduler.Namespace)]
    public sealed class Event
    {
        public static Event Create(Logging.Event e)
        {
            return new Event {TimeStamp = e.TimeStamp, Text = e.Text};
        }

        [DataMember(IsRequired = true)]
        public DateTime TimeStamp { get; set; }

        [DataMember(IsRequired = true)]
        public string Text { get; set; }
    }
}
