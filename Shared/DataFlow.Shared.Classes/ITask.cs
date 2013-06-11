namespace AISTek.DataFlow.Shared.Classes
{
    /// <summary>
    /// Defines methods for task class.
    /// </summary>
    public interface ITask
    {
        /// <summary>
        /// Gets and sets an instance of <see cref="IRepositoryService"/> that allows access to file repository.
        /// </summary>
        IRepository Repository { get; set; }

        /// <summary>
        /// Gets and sets and instance of <see cref="Task"/> that contains definition of current task.
        /// </summary>
        Task Task { get; set; }

        /// <summary>
        /// Checks system resources to comply task's requirements. 
        /// </summary>
        /// <param name="resources">
        /// An instance of <see cref="ISystemResources"/> that provides information about available system resources.
        /// </param>
        /// <returns>
        /// true if current task can be executed efficiently enough on specified resources, false otherwise.
        /// </returns>
        bool Validate(ISystemResources resources);

        /// <summary>
        /// Executes current task.
        /// </summary>
        void Execute();

        /// <summary>
        /// Teardowns the task's used resources.
        /// </summary>
        void Teardown();
    }
}


