using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace AISTek.DataFlow.Util
{
    public static class ObjectHelper
    {
        /// <summary>
        /// Performs <see cref="string.Format(string,object[])"/> on specified format string.
        /// </summary>
        /// <param name="format">
        /// Format string.
        /// </param>
        /// <param name="args">
        /// Array of format arguments.
        /// </param>
        /// <returns>
        /// Result of <code>string.Format(<paramref name="format"/>, <paramref name="args"/>)</code>
        /// </returns>
        [DebuggerStepThrough]
        public static string FormatWith(this string format, params object[] args)
        {
            Contract.Requires(!string.IsNullOrEmpty(format));
            Contract.Requires(args != null);
            return string.Format(format, args);
        }

        /// <summary>
        /// Casts argument <paramref name="obj"/> to type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">
        /// Target type.
        /// </typeparam>
        /// <param name="obj">
        /// Source object.
        /// </param>
        /// <returns>
        /// Result of <code>(<typeparamref name="T"/>)<paramref name="obj"/></code>
        /// </returns>
        [DebuggerStepThrough]
        public static T As<T>(this object obj)
        {
            Contract.Requires(obj != null);
            return (T) obj;
        }
    }
}
