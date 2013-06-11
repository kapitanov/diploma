using System;

namespace AISTek.DataFlow.PerfomanceToolkit
{
    public interface IBindTrace
    {
        IDisposable BindToConsole();
        IDisposable BindToTrace();
        IDisposable BindToDebug();
        IDisposable BindToCallback(Action<string, long> callback);
    }
}
