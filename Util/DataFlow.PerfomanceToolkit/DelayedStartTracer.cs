using System;
using System.Diagnostics.Contracts;

namespace AISTek.DataFlow.PerfomanceToolkit
{
    internal struct DelayedStartTracer : IStartTrace
    {
        public DelayedStartTracer(
            string name,
            Action<string> onStart,
            Action<string, long> onStop)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(name));
            Contract.Requires<ArgumentNullException>(onStart != null);
            Contract.Requires<ArgumentNullException>(onStop != null);

            this.name = name;
            this.onStart = onStart;
            this.onStop = onStop;
        }

        public IDisposable Start()
        {
            onStart(name);
            return new TraceCounter(name, onStop);
        }

        private readonly string name;
        private readonly Action<string> onStart;
        private readonly Action<string, long> onStop;
    }
}
