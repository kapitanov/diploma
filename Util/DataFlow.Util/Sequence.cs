using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AISTek.DataFlow.Util
{
    public static class Sequence
    {
        public static IEnumerable<T> Empty<T>()
        {
            yield break;
        }

        public static IEnumerable<T> Items<T>(T item)
        {
            yield return item;
            yield break;
        }

        public static IEnumerable<T> Items<T>(params T[] items)
        {
            foreach(var item in items)
                yield return item;
            yield break;
        }

        public static List<T> ListOf<T>(params T[] items)
        {
            return Items(items).ToList();
        }

        public static IEnumerable<T> Times<T>(this int times, Func<T> func)
        {
            for (var i = 0; i < times; i++)
                yield return func();
            yield break;
        }
    
        public static IEnumerable<int> Numbers(int startWith = 0, int incrementValue = 1)
        {
            var x = startWith;
            while (true)
            {
                yield return x;
                x += incrementValue;
            }
        }

        public static IEnumerable<TResult> Zip<TX, TY, TResult>(this IEnumerable<TX> xs, IEnumerable<TY> ys, Func<TX, TY, TResult> func)
        {
            var xEnumerator = xs.GetEnumerator();
            var yEnumerator = ys.GetEnumerator();

            while (xEnumerator.MoveNext() || yEnumerator.MoveNext())
            {
                yield return func(xEnumerator.Current, yEnumerator.Current);
            }
        }
    }
}
