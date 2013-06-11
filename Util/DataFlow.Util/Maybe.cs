using System;

namespace AISTek.DataFlow.Util
{
    public static class Maybe
    {
        public static void IfNotNull<T>(this T value, Action<T> action)
            where T : class
        {
            if (value != null)
                action(value);
        }
    }
}
