using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace AISTek.DataFlow.Util
{
    public static class LoggingExtensions
    {
        [Conditional("DEBUG")]
        public static void MethodCall(this LogWriter writer, params object[] args)
        {
            Contract.Requires(writer != null);
            Contract.Requires(args != null);

            writer.Write(new LogEntry
                             {
                                 Message = StackTraceBuilder.MethodCall(args), 
                                 Severity = TraceEventType.Information, 
                                 Categories = { "Informaion" }
                             });
        }

        [Conditional("DEBUG")]
        public static void Information(this LogWriter writer, string message)
        {
            Contract.Requires(writer != null);
            Contract.Requires(message != null);

            writer.Write(new LogEntry
            {
                Message = message,
                Severity = TraceEventType.Information,
                Categories = { "Informaion" }
            });            
        }
    }
}
