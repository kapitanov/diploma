namespace AISTek.DataFlow.PerfomanceToolkit
{
    public static class Perfomance
    {
#if PERF_TRACE

        public static IBindTrace Trace(string message, params object[] args)
        {
            return new TraceBinder(string.Format(message, args));
        }

        public static IBindLongTrace BeginTrace(string message, params object[] args)
        {
            return new LongtermTraceBinder(string.Format(message, args));
        }

        public static void Message(string message, params object[] args)
        {
            System.Diagnostics.Trace.WriteLine(MessageBuilder.BuildMessage(string.Format(message, args)));
        }

#else

        public static IBindTrace Trace(string message, params object[] args)
        {
            return new EmptyTraceBinder();
        }

        public static IBindLongTrace BeginTrace(string message, params object[] args)
        {
            return new EmptyTraceBinder();
        }

        public static void Message(string message, params object[] args)
        { }

#endif
    }
}
