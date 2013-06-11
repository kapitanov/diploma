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
    /// An implementation of <see cref="IExecutionEngine"/> that sandboxes another implementation of 
    /// <see cref="IExecutionEngine"/> and releases of used resources.
    /// </summary>
    public class SandboxEngine : MarshalByRefObject, IExecutionEngine
    {
        #region Constructor

        /// <summary>
        /// Initializes new instance of <see cref="SandboxEngine"/>. 
        /// </summary>
        /// <param name="engineType">
        /// An injection parameter - a type of <see cref="IExecutionEngine"/> that is being sandboxed.
        /// </param>
        [InjectionConstructor]
        public SandboxEngine(
            [Dependency("EngineType")]
            Type engineType)
        {
            Contract.Requires<ArgumentNullException>(engineType != null);
            Contract.Requires(
                engineType.GetInterfaces().Count(t => t == typeof(IExecutionEngine)) > 0,
                "Engine type passed into sandbox engine must implement IExecutionEngine.");

            this.engineType = engineType;
        }

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
                    using (Perfomance.Trace("SandboxEngine::ExecuteTask()").BindToTrace())
                    {
                        CreateSandbox();
                        RunTask();
                        FreeSandbox();
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
        private IExecutionEngine sandbox;
        private const string SandboxDomainName = "Sandbox";
        private readonly Type engineType;
        private AppDomain sandboxDomain;

        #endregion

        #region Private methods

        private void CreateSandbox()
        {
            Contract.Requires(engineType != null);
            using (Perfomance.Trace("SandboxEngine::CreateSandbox()").BindToConsole())
            {
                sandboxDomain = AppDomain.CreateDomain(SandboxDomainName);
                
                //sandboxDomain.AssemblyLoad += (_, e) =>
                //{
                //    Debug.Print("SANDBOX: Loaded rawAssembly {0}", e.LoadedAssembly.FullName);
                //};
                //sandboxDomain.AssemblyResolve += (_, e) =>
                //{
                //    Debug.Print("SANDBOX: Resolving rawAssembly {0}", e.Name);

                //};
                
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    sandboxDomain.Load(assembly.GetName());
                }

                var sandBoxAssembly = sandboxDomain.Load(engineType.Assembly.FullName);

                sandbox = sandboxDomain.CreateInstanceAndUnwrap(sandBoxAssembly.FullName, engineType.FullName)
                          as IExecutionEngine;
                sandbox.Repository = Repository;
                sandbox.SystemResources = SystemResources;
            }
        }

        private void RunTask()
        {
            Contract.Requires(sandbox != null);
            Contract.Requires(currentTask != null);

            try
            {
                using (Perfomance.Trace("SandboxEngine::RunTask()").BindToTrace())
                {
                    sandbox.ExecuteTask(currentTask);
                }
            }
            catch (TaskFailedException)
            {
                throw;
            }
            catch (TaskRejectedException)
            {
                throw;
            }
            catch (Exception inner)
            {
                throw new TaskFailedException(
                    string.Format("Task {0} failed. See InnerException property for details.", currentTask),
                    inner);
            }
        }

        private void FreeSandbox()
        {
            Contract.Requires(sandbox != null);
            Contract.Requires(currentTask != null);
            using (Perfomance.Trace("SandboxEngine::FreeSandbox()").BindToTrace())
            {

                var disposable = sandbox as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }

                AppDomain.Unload(sandboxDomain);
                currentTask = null;
            }
        }

        #endregion
    }
}