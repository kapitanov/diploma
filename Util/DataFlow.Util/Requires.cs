using System;
using System.Diagnostics;

namespace AISTek.DataFlow.Util
{
    /// <summary>
    /// Helper class for error checking
    /// </summary>
    public static class Requires
    {
        /// <summary>
        /// Throws <see cref="NullReferenceException"/> if 
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
                throw new NullReferenceException(parameterName);
        }

        /// <summary>
        /// Throws <see cref="NullReferenceException"/> if 
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
                throw new NullReferenceException(parameterName);
        }

        /// <summary>
        /// Throws <see cref="ArgumentException"/> if 
        /// <paramref name="type"/> is assignable to 
        /// <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">
        /// Destination type.
        /// </typeparam>
        /// <param name="type">
        /// Source type
        /// </param>
        /// <param name="message">
        /// Error message.
        /// </param>
        /// <example>
        /// <code>
        /// class Base {}
        /// class Derived1 : Base {}
        /// class Derived2 : Base {}
        /// </code>
        /// IsAssignable{Base}(Derived1) - Ok
        /// IsAssignable{Base}(Derived2) - Ok
        /// IsAssignable{Derived1}(Derived2) - error
        /// </example>
        [DebuggerStepThrough]
        public static void IsAssignable<T>(Type type, string message)
        {
            var typeTo = typeof(T);
            if (!typeTo.IsAssignableFrom(type))
                throw new ArgumentException(
                        "\"{0}\" is not convertable into \"{1}\". {2}".FormatWith(
                            type,
                            typeof(T),
                            message));
        }

        /// <summary>
        /// Throws <see cref="ArgumentException"/> 
        /// if <paramref name="expression"/> is false.
        /// </summary>
        /// <param name="expression">
        /// Expression to check.
        /// </param>
        /// <param name="message">
        /// Error message
        /// </param>
        [DebuggerStepThrough]
        public static void IsTrue(bool expression, string message)
        {
            if (!expression)
                throw new ArgumentException(message);
        }
    }
}
