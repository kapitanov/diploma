using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using AISTek.DataFlow.ComputeNode.Classes;
using AISTek.DataFlow.PerfomanceToolkit;
using AISTek.DataFlow.Shared.Classes;
using AISTek.DataFlow.Util;
using Microsoft.Practices.Unity;
using System.Diagnostics;

namespace AISTek.DataFlow.ComputeNode.Engine
{
    /// <summary>
    /// General task execution engine without sandboxing.
    /// </summary>
    public class GeneralEngine : MarshalByRefObject, IExecutionEngine
    {
        #region Constructor

        /// <summary>
        /// Initializes new instance of <see cref="GeneralEngine"/>.
        /// </summary>
        /// <param name="systemResources">
        /// An injection parameter - an instance of <see cref="ISystemResources"/>.
        /// </param>
        /// <param name="repository">
        /// An injection parameter - an instance of <see cref="IRepositoryService"/>.
        /// </param>
        [InjectionConstructor]
        public GeneralEngine(
            [Dependency]
            ISystemResources systemResources,
            [Dependency]
            IRepository repository)
        {
            SystemResources = systemResources;
            Repository = repository;
        }

        /// <summary>
        /// Initializes new instance of <see cref="GeneralEngine"/>.
        /// </summary>
        public GeneralEngine()
        { }

        #endregion

        #region Implementation of IExecutionEngine

        /// <summary>
        /// Gets and sets a value of <see cref="AISTek.DataFlow.Shared.Classes.ISystemResources"/> that provides information of available system resources.
        /// </summary>
        public ISystemResources SystemResources { get; set; }

        /// <summary>
        /// Gets and sets an instance of <see cref="AISTek.DataFlow.Shared.Classes.IRepositoryService"/> that allows access to file repository.
        /// </summary>
        public IRepository Repository { get; set; }

        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <param name="task">
        /// An instance of <see cref="AISTek.DataFlow.Shared.Classes.Task"/> that must be executed.
        /// </param>
        /// <exception cref="TaskRejectedException">
        /// Occurs if there's not enough system resources to execute specified task efficiently.
        /// </exception> 
        /// <exception cref="TaskFailedException">
        /// Occurs when execution engine fails to execute the specified task.
        /// </exception> 
        public void ExecuteTask(Task task)
        {
            // Make sure that any instance of engine will execute only one task at one time.
            lock (synchRoot)
            {
                currentTask = task;

                try
                {
                    using (Perfomance.Trace("GeneralEngine::ExecuteTask()").BindToTrace())
                    {
                        LoadCodebase();
                        CreateInstance();
                        RunTask();
                        Free();
                    }
                }
                catch (UnableToLoadTaskException e)
                {
                    throw new TaskExecutionFailureException(
                        string.Format("Unable to execute the task {0}: unable to load task's code.", currentTask),
                        e);
                }
                catch (TaskFailedException)
                {
                    throw;
                }
                catch (TaskRejectedException)
                {
                    throw;
                }
            }
        }

        #endregion

        #region Private fields

        private readonly object synchRoot = new object();

        private Task currentTask;
        private ITask taskInstance;
        private Assembly currentLoadingAssembly;

        #endregion

        #region Private methods

        private void LoadCodebase()
        {
            using (Perfomance.Trace("GeneralEngine::LoadCodebase()").BindToConsole())
            {
                assemblyLoader = new AssemblyLoader(Repository, currentTask).BindTo(AppDomain.CurrentDomain);

                foreach (var rawAssembly in from id in currentTask.EntryPoint.DependentAssemblyIds
                                         select Repository.Download(id).ReadToBuffer())
                {
                    AppDomain.CurrentDomain.Load(rawAssembly);
                }


                var buffer = Repository.Download(currentTask.EntryPoint.AssemblyId)
                    .ReadToBuffer();
                currentLoadingAssembly = Assembly.Load(buffer);
            }
        }

        private void CreateInstance()
        {
            Contract.Requires(currentTask != null);
            Contract.Requires(currentTask.EntryPoint != null);
            Contract.Requires(!string.IsNullOrEmpty(currentTask.EntryPoint.QualifiedClassName));

            using (Perfomance.Trace("GeneralEngine::CreateInstance()").BindToConsole())
            {
                var type = Type.GetType(currentTask.EntryPoint.QualifiedClassName);
                if (type == null)
                {
                    var typeName = new TypeNameParser().Parse(currentTask.EntryPoint.QualifiedClassName);
                    type = currentLoadingAssembly.GetType(typeName.Name, false);
                    if (type == null)
                        throw new UnableToLoadTaskException(string.Format(
                            "Unable to load task: entry point class {0} doesn't exists.",
                            currentTask.EntryPoint.QualifiedClassName));
                }
                try
                {
                    var instance = Activator.CreateInstance(type);
                    taskInstance = instance as ITask;
                    if (taskInstance == null)
                        throw new UnableToLoadTaskException(string.Format(
                            "Unable to load task: entry point class {0} doesn't implement interface {1}.",
                            currentTask.EntryPoint.QualifiedClassName,
                            typeof(ITask).FullName));
                }
                catch (MissingMethodException inner)
                {
                    throw new UnableToLoadTaskException(string.Format(
                        "Unable to load task: entry point class {0} doesn't have a parameterless contructor.",
                        currentTask.EntryPoint.QualifiedClassName),
                                                        inner);
                }
                catch (TargetInvocationException inner)
                {
                    throw new UnableToLoadTaskException(string.Format(
                        "Unable to load task: entry point class {0} constructor has thrown an exception.",
                        currentTask.EntryPoint.QualifiedClassName),
                                                        inner);
                }
            }
        }

        private void RunTask()
        {
            Contract.Requires(currentTask != null);
            Contract.Requires(taskInstance != null);

            using (Perfomance.Trace("GeneralEngine::RunTask()").BindToTrace())
            {
                // Configure the task instance
                taskInstance.Repository = Repository;
                taskInstance.Task = currentTask;

                // Validate the task
                if (!taskInstance.Validate(SystemResources))
                {
                    throw new TaskRejectedException(
                        string.Format("Task {0} refused to be run on current machine.", currentTask));
                }

                // Execute the task
                try
                {
                    taskInstance.Execute();
                }
                catch (Exception inner)
                {
                    throw new TaskFailedException(
                        string.Format("Task {0} failed. See InnerException property for details.", currentTask),
                        inner);
                }
            }
        }

        private void Free()
        {
            using (Perfomance.Trace("GeneralEngine::Free()").BindToTrace())
            {
                taskInstance.Teardown();
                taskInstance = null;
                currentTask = null;
            }
        }

        #endregion

        private AssemblyLoader assemblyLoader;
    }
}

