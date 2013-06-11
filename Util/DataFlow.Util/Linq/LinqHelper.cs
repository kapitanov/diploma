using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace AISTek.DataFlow.Util.Linq
{
    public static class LinqHelper
    {
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            Contract.Requires(enumeration != null);
            Contract.Requires(action != null);
            foreach (var item in enumeration)
            {
                action(item);
            }
        }
    }
}


