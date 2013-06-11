using System;

namespace AISTek.DataFlow.ComputeNode.Classes
{
    /// <summary>
    /// Represents task execution overall result.
    /// </summary>
    public sealed class ExecutionResult
    {
        /// <summary>
        /// Creates new instance of <see cref="ExecutionResult"/> class 
        /// that defines the overall result of successful task execution.
        /// </summary>
        /// <param name="taskId">
        /// Unique identifier of executed task.
        /// </param>
        /// <returns>
        /// An instance of <see cref="ExecutionResult"/> class.
        /// </returns>
        public static ExecutionResult Success(Guid taskId)
        {
            return new ExecutionResult(taskId, ExecutionResultState.Success);
        }

        /// <summary>
        /// Creates new instance of <see cref="ExecutionResult"/> class 
        /// that defines the overall result of task rejection.
        /// </summary>
        /// <param name="taskId">
        /// Unique identifier of executed task.
        /// </param>
        /// <returns>
        /// An instance of <see cref="ExecutionResult"/> class.
        /// </returns>
        public static ExecutionResult Rejected(Guid taskId)
        {
            return new ExecutionResult(taskId, ExecutionResultState.Rejected);
        }

        /// <summary>
        /// Creates new instance of <see cref="ExecutionResult"/> class 
        /// that defines the overall result of failure task execution.
        /// </summary>
        /// <param name="taskId">
        /// Unique identifier of executed task.
        /// </param>
        /// <returns>
        /// An instance of <see cref="ExecutionResult"/> class.
        /// </returns>
        public static ExecutionResult Failed(Guid taskId)
        {
            return new ExecutionResult(taskId, ExecutionResultState.Failed);
        }

        private ExecutionResult(Guid taskId, ExecutionResultState state)
        {
            TaskId = taskId;
            State = state;
        }

        /// <summary>
        /// Gets a task's unique identifier.
        /// </summary>
        public Guid TaskId { get;private set; }

        /// <summary>
        /// Gets a value indicating kind of task execution result.
        /// </summary>
        public ExecutionResultState State { get; private set; }
    }
}
