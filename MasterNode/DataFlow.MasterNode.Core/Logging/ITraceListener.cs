using System.Linq;

namespace AISTek.DataFlow.MasterNode.Core.Logging
{
    public interface ITraceListener
    {
        IQueryable<Event> Debug { get; }

        IQueryable<Event> Trace { get; }
    }
}


