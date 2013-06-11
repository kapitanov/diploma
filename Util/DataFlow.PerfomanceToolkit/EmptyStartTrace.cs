using System;

namespace AISTek.DataFlow.PerfomanceToolkit
{
    internal struct EmptyStartTrace : IStartTrace
    {
        IDisposable IStartTrace.Start()
        {
            return new EmptyDisposable();
        }
    }
}
