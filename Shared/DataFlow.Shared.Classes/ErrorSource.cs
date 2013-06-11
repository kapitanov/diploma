namespace AISTek.DataFlow.Shared.Classes
{
    /// <summary>
    /// Defines possible error sources.
    /// </summary>
    public enum ErrorSource
    {
        /// <summary>
        /// The error has been caused by task's code.
        /// </summary>
        Task = 0,

        /// <summary>
        /// The error has been caused by task execution engine.
        /// </summary>
        Engine,

        /// <summary>
        /// The error has been caused by external code.
        /// </summary>
        External,
        
        /// <summary>
        /// The error source is unknown.
        /// </summary>
        Unknown
    }
}
