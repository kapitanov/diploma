using System;
using System.Diagnostics;
using AISTek.DataFlow.PresentationExtensions;

namespace AISTek.DataFlow.MasterNode.ConsoleHost.Tracing
{
    internal sealed class ConsoleTraceListener : System.Diagnostics.TraceListener
    {
        public override void Write(string message)
        {
            WriteMessage(message);
        }

        public override void WriteLine(string message)
        {
            WriteMessage(message);
        }

        [Conditional("DEBUG")]
        private static void WriteMessage(string message)
        {
            using (ConsoleMessage.Forecolor(ConsoleColor.Gray))
                Console.WriteLine(message);
        }
    }
}
