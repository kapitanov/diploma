using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace AISTek.DataFlow.Util
{
    internal static class StackTraceBuilder
    {
        public static string MethodCall(params object[] args)
        {
            Contract.Requires(args != null);
            return Build(new StackTrace().GetFrame(2), args);
        }

        public static string StackTrace(params object[] args)
        {
            Contract.Requires(args != null);

            var stackTrace = new StackTrace();
            var sb = new StringBuilder();
            foreach (var frame in stackTrace.GetFrames())
            {
                sb.AppendLine(Build(frame));
            }

            return Build(new StackTrace().GetFrame(1), args);
        }

        /// <summary>
        /// Builds stack trace line for specified <see cref="StackFrame"/> value.
        /// </summary>
        /// <param name="frame">
        /// Stack trace frame to process.
        /// </param>
        /// <param name="args">
        /// Array of method argument values
        /// </param>
        /// <returns>
        /// String representation of <paramref name="frame"/>.
        /// </returns>
        private static string Build(StackFrame frame, params object[] args)
        {
            Contract.Requires(frame != null);
            Contract.Requires(args != null);
            var method = frame.GetMethod();

            if (method == null)
                throw new InvalidOperationException("Unable to get stack frame's method");

            // Convert method definition
            var sb = new StringBuilder();
            if (method.IsConstructor)
                sb.AppendFormat("{0}.ctor", method.DeclaringType.FullName);
            else
                sb.AppendFormat("{0}.{1}", method.DeclaringType.FullName, method.Name);

            // Convert type parameters
            if (method.IsGenericMethod)
            {
                sb.Append("(<");
                var genericArguments = method.GetGenericArguments();
                for (var index = 0; index < genericArguments.Length; index++)
                {
                    sb.Append(genericArguments[index]);
                    if (index != genericArguments.Length - 1)
                        sb.Append(", ");
                }
                sb.Append(">");
            }

            // Convert arguments
            var parameters = method.GetParameters();
            sb.Append("(");
            if (parameters.Length == args.Length)
            {
                // One case - we have values for all method parameters in args array
                for (var index = 0; index < parameters.Length; index++)
                {
                    var parameter = parameters[index];
                    sb.AppendFormat("{0}: {1}",
                                    parameter.Name,
                                    args[index]);
                    if (index != parameters.Length - 1)
                        sb.Append(", ");
                }
            }
            else
            {
                // Other case - we don't have values for all method parameters in args array or args is empty
                for (var index = 0; index < parameters.Length; index++)
                {
                    var parameter = parameters[index];
                    sb.AppendFormat("{0} {1}",
                                    parameter.ParameterType,
                                    parameter.Name);
                    if (index != parameters.Length - 1)
                        sb.Append(", ");
                }
            }
            sb.Append(")");

            return sb.ToString();
        }
    }
}
