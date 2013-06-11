using System;

namespace AISTek.DataFlow.MasterNode.Core.Logging
{
    public sealed class Event
    {
        public static Event New(string text)
        {
            return new Event(DateTime.Now, text);
        }

        public Event(DateTime timeStamp, string text)
        {
            TimeStamp = timeStamp;
            Text = text;
        }

        public DateTime TimeStamp { get; private set; }

        public string Text{ get; private set; }
    }
}


