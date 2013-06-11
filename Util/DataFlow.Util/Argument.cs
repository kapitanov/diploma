using System;
using System.Diagnostics;

namespace AISTek.DataFlow.Util
{
    /// <summary>
    /// Helper class for method's arguments' checking 
    /// </summary>
    public static class Argument
    {
        /// <summary>
        /// Throws <see cref="ArgumentNullException"/> if 
        /// <paramref name="parameter"/> is null.
        /// </summary>
        /// <param name="parameter">
        /// Parameter to check.
        /// </param>
        /// <param name="parameterName">
        /// Exception's message (usually parameter's name).
        /// </param>
        [DebuggerStepThrough]
        public static void NotNull(object parameter, string parameterName)
        {
            if (parameter == null)
                throw new ArgumentNullException(parameterName);
        }

        /// <summary>
        /// Throws <see cref="ArgumentNullException"/> if 
        /// <paramref name="parameter"/> is default(<typeparam name="T" />).
        /// </summary>
        /// <param name="parameter">
        /// Parameter to check.
        /// </param>
        /// <param name="parameterName">
        /// Exception's message (usually parameter's name).
        /// </param>
        [DebuggerStepThrough]
        public static void NotDefault<T>(T parameter, string parameterName)
            where T : struct
        {
            if (parameter.Equals(default(T)))
                throw new ArgumentNullException(parameterName);
        }

        /// <summary>
        /// Throws <see cref="ArgumentNullException"/> if 
        /// <paramref name="parameter"/> is null or empty.
        /// </summary>
        /// <param name="parameter">
        /// Parameter to check.
        /// </param>
        /// <param name="parameterName">
        /// Exception's message (usually parameter's name).
        /// </param>
        [DebuggerStepThrough]
        public static void NotNullOrEmpty(string parameter, string parameterName)
        {
            NotNull(parameter, parameterName);

            if (string.IsNullOrEmpty(parameter))
                throw new ArgumentNullException(
                    parameterName,
                    "Parameter couldn't be string.Empty");
        }

    }
}
