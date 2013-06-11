using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AISTek.DataFlow.PresentationExtensions;

namespace AISTek.DataFlow.MasterNode.ConsoleHost.Tracing
{
    internal sealed class TraceListener : System.Diagnostics.TraceListener
    {
        public override void WriteLine(string message, string category)
        {
            if(category == Category.Services)
            {
                using(ConsoleMessage.Warning())
                {
                    Console.WriteLine(message);
                }
                return;
            }

            if(category == Category.Failure)
            {
                using(ConsoleMessage.Error())
                {
                    Console.WriteLine(message);
                }
                return;
            }

            Console.WriteLine(message);
        }

        public override void Write(string message)
        {
        }

        public override void WriteLine(string message)
        {

        }
    }
}
