using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AISTek.DataFlow.Util;

namespace AISTek.DataFlow.MasterNode.Scheduler
{
    public interface IWorkerChannel : ICancellable
    {
        bool IsOk { get; }
    }
}
