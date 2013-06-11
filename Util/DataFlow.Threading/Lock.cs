using System;
using System.Diagnostics;
using System.Threading;
using AISTek.DataFlow.PerfomanceToolkit;

namespace AISTek.DataFlow.Threading
{
    public sealed class Lock : IDisposable
    {
        private Lock(object synchRoot, string lockName)
        {
            this.synchRoot = synchRoot;
            this.lockName = lockName;
            Monitor.Enter(synchRoot);
            TraceEnter();
        }

        public static IDisposable Enter(object synchRoot, string format = null, params object[] args)
        {
            if (format != null)
                return new Lock(synchRoot, string.Format(format, args));

            return new Lock(synchRoot, string.Empty);
        }

        public void Dispose()
        {
            Monitor.Exit(synchRoot);
            TraceExit();
        }

        [Conditional("TRACE")]
        private void TraceEnter()
        {
            Perfomance.Message("Entering critical section {0}", lockName);
        }

        [Conditional("TRACE")]
        private void TraceExit()
        {
            Perfomance.Message("Exiting critical section {0}", lockName);
        }


        private readonly object synchRoot;
        private readonly string lockName;
    }
}
