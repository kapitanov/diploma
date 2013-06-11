namespace AISTek.DataFlow.Util
{
    /// <summary>
    /// Represents an operation that can be interrupted.
    /// </summary>
    public interface ICancellable
    {
        /// <summary>
        /// Interrups operation.
        /// </summary>
        void Cancel();
    }
}
