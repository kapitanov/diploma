using System;
using System.Diagnostics;

namespace AISTek.DataFlow.PerfomanceToolkit
{
    internal static class MessageBuilder
    {
        static MessageBuilder()
        {
            UseTimestamp = true;
            TraceCategory = typeof(Perfomance).FullName;
        }

        public static bool UseTimestamp { get; set; }

        public static string BuildResult(string name, long time)
        {
            if (UseTimestamp)
                return string.Format("[{0}]: {1} completed in {2} ms", DateTime.Now, name, time);
            return string.Format("{0} completed in {1} ms", name, time);
        }

        public static string BuildStart(string name)
        {
            if (UseTimestamp)
                return string.Format("[{0}]: {1} is starting", DateTime.Now, name);
            return string.Format("{0} is starting", name);
        }

        public static string BuildMessage(string name)
        {
            if (UseTimestamp)
                return string.Format("[{0}]: {1}", DateTime.Now, name);
            return name;
        }

        public static string TraceCategory { get; private set; }

        public static void WriteResultToConsole(string name, long time)
        {
            Console.WriteLine(BuildResult(name, time));
        }

        public static void WriteResultToTrace(string name, long time)
        {
            Trace.WriteLine(BuildResult(name, time), TraceCategory);
        }

        public static void WriteResultToDebug(string name, long time)
        {
            Debug.WriteLine(BuildResult(name, time), TraceCategory);
        }

        public static void WriteStartToConsole(string name)
        {
            Console.WriteLine(BuildStart(name));
        }

        public static void WriteStartToTrace(string name)
        {
            Trace.WriteLine(BuildStart(name), TraceCategory);
        }

        public static void WriteStartToDebug(string name)
        {
            Debug.WriteLine(BuildStart(name), TraceCategory);
        }
    }
}
