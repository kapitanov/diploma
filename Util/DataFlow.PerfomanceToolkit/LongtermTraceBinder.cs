using System;

namespace AISTek.DataFlow.PerfomanceToolkit
{
    internal struct LongtermTraceBinder : IBindLongTrace
    {
        public LongtermTraceBinder(string name)
        {
            this.name = name;
        }

        public IStartTrace BindToConsole()
        {
            return new DelayedStartTracer(name, MessageBuilder.WriteStartToConsole, MessageBuilder.WriteResultToConsole);
        }

        public IStartTrace BindToTrace()
        {
            return new DelayedStartTracer(name, MessageBuilder.WriteStartToTrace, MessageBuilder.WriteResultToTrace);
        }

        public IStartTrace BindToDebug()
        {
            return new DelayedStartTracer(name, MessageBuilder.WriteStartToDebug, MessageBuilder.WriteResultToDebug);
        }

        public IStartTrace BindToCallback(Action<string> onStartCallback, Action<string, long> onStopCallback)
        {
            return new DelayedStartTracer(name, onStartCallback, onStopCallback);
        }

        private readonly string name;


    }
}
