using System;

namespace AISTek.DataFlow.PerfomanceToolkit
{
    internal struct EmptyDisposable : IDisposable
    {
        void IDisposable.Dispose()
        { }
    }
}
