using System;
using System.Collections.Generic;
using System.Diagnostics;
using AISTek.DataFlow.Shared.Classes;
using AISTek.DataFlow.Util;

namespace AISTek.DataFlow.MasterNode.DataModel
{
    /// <summary>
    /// Internal representation of task.
    /// </summary>
    public sealed class Task
    {
        private Task()
        {
            Id = Guid.Empty;
            Name = string.Empty;
            EntryPoint = new EntryPoint();
            InputFiles = new List<FileLink>();
            OutputFiles = new List<FileLink>();
            Inputs = new List<Task>();
            Outputs = new List<Task>();
            Parameters = new Dictionary<string, string>();
            State = TaskState.Unattached;
            Error = null;
        }

        /// <summary>
        /// Initializes new instance of <see cref="Task"/>
        /// </summary>
        /// <param name="name">
        /// Task's friendly name.
        /// </param>
        /// <param name="entryPoint">
        /// Task's entry point.
        /// </param>
        public Task(string name, EntryPoint entryPoint)
            : this()
        {
            Name = name;
            EntryPoint = entryPoint;
        }

        /// <summary>
        /// An unique identifier of current task.
        /// Initially it's set to <see cref="Guid.Empty"/>.
        /// It will get its actual value when task will be added into scheduler.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets task's friendly name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets task's entry point.
        /// </summary>
        public EntryPoint EntryPoint { get; private set; }

        /// <summary>
        /// Gets list of links to input files.
        /// </summary>
        public IList<FileLink> InputFiles { get; set; }

        /// <summary>
        /// Gets list of links to output files.
        /// </summary>
        public IList<FileLink> OutputFiles { get;  set; }

        /// <summary>
        /// Gets list of all tasks which current task depends on. 
        /// </summary>
        public IList<Task> Inputs { get; set; }

        /// <summary>
        /// Gets list of all tasks which depends on current task.
        /// </summary>
        public IList<Task> Outputs { get; set; }

        /// <summary>
        /// Gets additional task's parameters.
        /// </summary>
        public IDictionary<string, string> Parameters { get; set; }

        /// <summary>
        /// Gets a job which current job belongs to.
        /// </summary>
        public Job Job { get; set; }

        private TaskState state;

        /// <summary>
        /// Gets and sets task's state.
        /// Initially task state is set to <see cref="TaskState.Unattached"/>
        /// </summary>
        public TaskState State
        {
            get { return state; }
            set
            {
                Debug.Print("Task(\"{0}\").State <- {1}", Name, value);
                state = value;
            }
        }

        /// <summary>
        /// Gets and sets a value that describes a reason of current task to fail.
        /// This value has a meaning only when <see cref="State"/> is <see cref="TaskState.Failed"/>.
        /// Otherwise, it should have "null" value.
        /// </summary>
        public ErrorReport Error { get; set; }

        /// <summary>
        /// Count of unresolved dependencies
        /// </summary>
        public uint DependenciesCount { get; set; }

        /// <summary>
        /// An instance of <see cref="ICancellable"/> that defines the connection to remote worker.
        /// </summary>
        public ICancellable Worker { get; set; }

        /// <summary>
        /// Gets an instance of <see cref="T:System.String"/> which represents current instance of <see cref="Task"/>.
        /// </summary>
        /// <returns>
        /// An instance of <see cref="T:System.String"/> which represents current instance of <see cref="Task"/>.
        /// </returns>
        public override string ToString()
        {
            return string.Format("(\"{0}\", {{{1}}})", Name, Id);
        }
    }
}
