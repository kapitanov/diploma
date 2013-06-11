using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace AISTek.DataFlow.PerfomanceToolkit
{
    internal struct TraceCounter : IDisposable
    {
        public TraceCounter(string name, Action<string, long> action)
        {
            Contract.Requires<ArgumentNullException>(name!= null);
            Contract.Requires<ArgumentNullException>(action != null);

            this.name = name;
            this.action = action;
            watch = Stopwatch.StartNew();
        }
        
        public void Dispose()
        {
            watch.Stop();
            action(name, watch.ElapsedMilliseconds);
        }

        private readonly string name;
        private readonly Action<string, long> action;
        private readonly Stopwatch watch;
    }
}
