using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace AISTek.DataFlow.Util
{
    public static class ListHelper
    {
        public static void MoveTo<T>(this IList<T> source, IList<T> destination, T item)
        {
            Contract.Requires(source != null);
            Contract.Requires(destination != null);
            Contract.Requires(!Equals(item, default(T)));

            lock (source)
            {
                lock (destination)
                {
                    source.Remove(item);
                    destination.Add(item);
                }
            }
        }

        public static void MoveBack<T>(this IList<T> source, IList<T> destination, T item)
        {
            Contract.Requires(source != null);
            Contract.Requires(destination != null);
            Contract.Requires(!Equals(item, default(T)));

            lock (destination)
            {
                lock (source)
                {
                    source.Remove(item);
                    destination.Add(item);
                }
            }
        }
    }
}
