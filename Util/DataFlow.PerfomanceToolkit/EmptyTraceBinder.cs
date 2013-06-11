using System;

namespace AISTek.DataFlow.PerfomanceToolkit
{
    internal struct EmptyTraceBinder : IBindTrace, IBindLongTrace
    {
        IDisposable IBindTrace.BindToConsole()
        {
            return new EmptyDisposable();
        }

        IStartTrace IBindLongTrace.BindToTrace()
        {
            return new EmptyStartTrace();
        }

        IStartTrace IBindLongTrace.BindToDebug()
        {
            return new EmptyStartTrace();
        }

        IStartTrace IBindLongTrace.BindToCallback(Action<string> onStartCallback, Action<string, long> onStopCallback)
        {
            return new EmptyStartTrace();
        }

        IStartTrace IBindLongTrace.BindToConsole()
        {
            return new EmptyStartTrace();
        }

        IDisposable IBindTrace.BindToTrace()
        {
            return new EmptyDisposable();
        }

        IDisposable IBindTrace.BindToDebug()
        {
            return new EmptyDisposable();
        }

        IDisposable IBindTrace.BindToCallback(Action<string, long> callback)
        {
            return new EmptyDisposable();
        }
    }
}
