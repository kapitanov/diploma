using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Text;

namespace AISTek.DataFlow.Util
{
    /// <summary>
    /// Helper class for <see cref="Debug"/> tracing extensions. 
    /// </summary>
    /// <remarks>
    /// All methods of this class work only if assembly is built with "_DEBUG" constant.
    /// Otherwise, no actions will be performed.
    /// </remarks>
    public static class DebugTracing
    {
        /// <summary>
        /// Writes into <see cref="Debug"/> log a line that informs about object creation.
        /// This methods should be called inside a constructor.
        /// </summary>
        /// <param name="instance">
        /// A <code>this</code> value of object that called this method.
        /// </param>
        [Conditional("DEBUG")]
        public static void InstanceCreated(object instance)
        {
            Contract.Requires(instance != null);
            Debug.Print(
                "An instance of {0} created: hash code: {1}",
                instance.GetType().FullName,
                instance.GetHashCode());
        }

        /// <summary>
        /// Writes into <see cref="Debug"/> log a line that informs about crossing method's boundary.
        /// This method should be called in a first line of called method.
        /// </summary>
        /// <param name="args">
        /// Array of all called methods arguments. 
        /// The order of arguments matters.
        /// This method print their <see cref="object.ToString()"/> values with corresponding method parameter names. 
        /// </param>
        /// <remarks>
        /// If size of <paramref name="args"/> array and amount of called method's parameters are different,
        /// then no parameter information is printed. The <code>"..."</code> line is printed instead.
        /// </remarks>
        [Conditional("DEBUG")]
        public static void MethodCall(params object[] args)
        {
            Debug.Print(StackTraceBuilder.MethodCall(args));
        }
    }
}
