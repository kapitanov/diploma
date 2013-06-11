using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AISTek.DataFlow.MasterNode.Core.Logging
{
    internal sealed class CoreTraceListener : TraceListener
    {
        public CoreTraceListener(int maxTraceLength)
        {
            this.maxTraceLength = maxTraceLength;
        }

        public IQueryable<Event> Events { get { return events.AsQueryable(); } }

        #region Overrides of TraceListener

        public override void Write(string message)
        {
            CreateEvent(message);
        }

        public override void WriteLine(string message)
        {
            CreateEvent(message);
        }

        #endregion

        #region Private members

        private readonly IList<Event> events = new List<Event>();
        private readonly int maxTraceLength;

        private void CreateEvent(string text)
        {
            lock (events)
            {
                while(events.Count >= maxTraceLength)
                {
                    events.RemoveAt(0);
                }

                events.Add(Event.New(text));
            }
        }

        #endregion
    }
}


