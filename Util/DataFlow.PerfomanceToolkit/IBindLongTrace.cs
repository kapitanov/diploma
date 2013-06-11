using System;

namespace AISTek.DataFlow.PerfomanceToolkit
{
    public interface IBindLongTrace
    {
        IStartTrace BindToConsole();
        IStartTrace BindToTrace();
        IStartTrace BindToDebug();
        IStartTrace BindToCallback(Action<string> onStartCallback, Action<string, long> onStopCallback);
    }
}
