namespace AISTek.DataFlow.ComputeNode.Service
{
    /// <summary>
    /// Defines methods for top-level management of execution service.
    /// </summary>
    public interface IExecutionService
    {
        /// <summary>
        /// Starts execution service.
        /// </summary>
        void Stop();

        /// <summary>
        /// Stops started service.
        /// </summary>
        void Start();
    }
}