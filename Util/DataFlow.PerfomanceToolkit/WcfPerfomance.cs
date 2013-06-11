using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Diagnostics;

namespace AISTek.DataFlow.PerfomanceToolkit
{
    public static class WcfPerfomance
    {
        [Conditional("PERF_TRACE")]
        public static void MonitorEvents(OperationContext operationContext, string serviceName)
        {
            new WcfEventsMonitor(serviceName).BindTo(operationContext);
        }
    }
}
