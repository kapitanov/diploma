using System;

namespace AISTek.DataFlow.PerfomanceToolkit
{
    internal struct TraceBinder : IBindTrace
    {
        public TraceBinder(string name)
        {
            this.name = name;
        }

        public IDisposable BindToConsole()
        {
            return new TraceCounter(name, MessageBuilder.WriteResultToConsole);
        }

        public IDisposable BindToTrace()
        {
            return new TraceCounter(name, MessageBuilder.WriteResultToTrace);
        }

        public IDisposable BindToDebug()
        {
            return new TraceCounter(name, MessageBuilder.WriteResultToDebug);
        }

        public IDisposable BindToCallback(Action<string, long> callback)
        {
            return new TraceCounter(name, callback);
        }

        private readonly string name;
    }
}
