using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.Shared.Client
{
    /// <summary>
    /// Client proxy class for <see cref="IJobManager"/>.
    /// </summary>
    [DebuggerStepThrough]
    [Serializable]
    internal sealed class JobManagerClient : DuplexClientBase<IJobManager>, IJobManagerClient
    {
        #region Constructors
        
        internal JobManagerClient(InstanceContext callbackInstance)
            : base(callbackInstance)
        { }

        internal JobManagerClient(InstanceContext callbackInstance, string endpointConfigurationName)
            : base(callbackInstance, endpointConfigurationName)
        { }

        internal JobManagerClient(InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress)
            : base(callbackInstance, endpointConfigurationName, remoteAddress)
        { }

        internal JobManagerClient(InstanceContext callbackInstance, string endpointConfigurationName, EndpointAddress remoteAddress)
            : base(callbackInstance, endpointConfigurationName, remoteAddress)
        { }

        internal JobManagerClient(InstanceContext callbackInstance, Binding binding, EndpointAddress remoteAddress)
            : base(callbackInstance, binding, remoteAddress)
        { } 

        #endregion

        #region Implementation of IJobManager

        /// <summary>
        /// Creates a job with specified name and sets it as current.
        /// </summary>
        /// <param name="name">
        /// Job's name.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Job"/>.
        /// </returns>
        public Job CreateJob(string name)
        {
            return Channel.CreateJob(name);
        }

        /// <summary>
        /// Opens an existing job and sets it as current.
        /// </summary>
        /// <param name="id">
        /// Job's UID.
        /// </param>
        public void OpenJob(Guid id)
        {
            Channel.OpenJob(id);
        }

        /// <summary>
        /// Creates a task in current job.
        /// </summary>
        /// <param name="task">
        /// An instance of <see cref="Task"/> which defines the task to create.
        /// </param>
        /// <returns>
        /// An unique identifier for created task.
        /// </returns>
        public Guid CreateTask(Task task)
        {
            return Channel.CreateTask(task);
        }

        /// <summary>
        /// Updates the created task with UID = <paramref name="id"/> in the current job. 
        /// </summary>
        /// <param name="id">
        /// Task's UID.
        /// </param>
        /// <param name="task">
        /// Task's updated definition.
        /// </param>
        public void UpdateTask(Guid id, Task task)
        {
            Channel.UpdateTask(id, task);
        }

        /// <summary>
        /// Removes the task from current job.
        /// </summary>
        /// <param name="id">
        /// Task's UID.
        /// </param>
        public void RemoveTask(Guid id)
        {
            Channel.RemoveTask(id);
        }

        /// <summary>
        /// Starts execution of the current job.
        /// </summary>
        public void StartJob()
        {
            Channel.StartJob();
        }

        /// <summary>
        /// Cancels execution of the current job.
        /// </summary>
        public void CancelJob()
        {
            Channel.CancelJob();
        }

        /// <summary>
        /// Reinitializes current job after cancelling, allowing to start it again.
        /// </summary>
        public void RestartJob()
        {
            Channel.RestartJob();
        }

        /// <summary>
        /// Deletes current job from scheduler. Job must not be in state "Processing".
        /// </summary>
        public void DeleteJob()
        {
            Channel.DeleteJob();
        }

        /// <summary>
        /// Finds all jobs in scheduler which are not being processed and have specified name.
        /// </summary>
        /// <param name="name">
        /// Job's name to match.
        /// </param>
        /// <returns>
        /// List of all matching jobs.
        /// </returns>
        public IEnumerable<Job> FindJobs(string name)
        {
            return Channel.FindJobs(name);
        }

        /// <summary>
        /// Get list of all jobs in scheduler.
        /// </summary>
        /// <returns>
        /// List of all jobs in scheduler.
        /// </returns>
        public IEnumerable<Job> GetAllJobs()
        {
            return Channel.GetAllJobs();
        }

        /// <summary>
        /// Gets an instance of <see cref="Job"/> which describes current job.
        /// </summary>
        public Job CurrentJob { get { return Channel.CurrentJob; } }

        /// <summary>
        /// Gets a state of the the job.
        /// </summary>
        /// <param name="jobId">
        /// Unique identifier of a job.
        /// </param>
        /// <returns>
        /// A value of <see cref="JobState"/> that contains the state of the job.
        /// If the job with speicifed GUID doesn't exists, then a value of <see cref="JobState.NotExists"/>
        /// </returns>
        public JobState QueryJobState(Guid jobId)
        {
            return Channel.QueryJobState(jobId);
        }

        /// <summary>
        /// Gets error report for failed job.
        /// </summary>
        /// <param name="jobId">
        /// Unique identifier of a job.
        /// </param>
        /// <returns>
        /// An instance of <see cref="ErrorSummary"/> class.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// This exception is thrown if the specified job is not in "failed" state.
        /// </exception>
        public ErrorSummary GetErrorReport(Guid jobId)
        {
            return Channel.GetErrorReport(jobId);
        }

        #endregion

        #region Implementation of IChannel

        /// <summary>
        /// Returns a typed object requested, if present, from the appropriate layer in the channel stack.
        /// </summary>
        /// <returns>
        /// The typed object <paramref name="T"/> requested if it is present or null if it is not.
        /// </returns>
        /// <typeparam name="T">The typed object for which the method is querying.</typeparam>
        public T GetProperty<T>() 
            where T : class
        {
            return InnerDuplexChannel.GetProperty<T>();
        }

        #endregion

        #region Implementation of IExtensibleObject<IContextChannel>

        /// <summary>
        /// Gets a collection of extension objects for this extensible object.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.ServiceModel.IExtensionCollection`1"/> of extension objects.
        /// </returns>
        public IExtensionCollection<IContextChannel> Extensions
        {
            get { return InnerDuplexChannel.Extensions; }
        }

        #endregion

        #region Implementation of IContextChannel

        /// <summary>
        /// Gets or sets a value that instructs Windows Communication Foundation (WCF) to store a set of messages before giving the messages to the transport.
        /// </summary>
        /// <returns>
        /// true if the batching of outgoing messages is allowed; otherwise, false.
        /// </returns>
        public bool AllowOutputBatching
        {
            get { return InnerDuplexChannel.AllowOutputBatching; }
            set { InnerDuplexChannel.AllowOutputBatching = value; }
        }

        /// <summary>
        /// Gets the input session for the channel.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.ServiceModel.Channels.IInputSession"/> for the channel.
        /// </returns>
        public IInputSession InputSession
        {
            get { return InnerDuplexChannel.InputSession; }
        }

        /// <summary>
        /// Gets the local endpoint for the channel.
        /// </summary>
        /// <returns>
        /// The local <see cref="T:System.ServiceModel.EndpointAddress"/> for the channel.
        /// </returns>
        public EndpointAddress LocalAddress
        {
            get { return InnerDuplexChannel.LocalAddress; }
        }

        /// <summary>
        /// Gets or sets the time period within which an operation must complete or an exception is thrown.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.TimeSpan"/> that specifies the time period within which an operation must complete.
        /// </returns>
        public TimeSpan OperationTimeout
        {
            get { return InnerDuplexChannel.OperationTimeout; }
            set { InnerDuplexChannel.OperationTimeout = value; }
        }

        /// <summary>
        /// Gets the output session associated with the channel, if any.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.ServiceModel.Channels.IOutputSession"/> implementation if an input session exists; otherwise, null.
        /// </returns>
        public IOutputSession OutputSession
        {
            get { return InnerDuplexChannel.OutputSession; }
        }

        /// <summary>
        /// Gets the remote address associated with the channel.
        /// </summary>
        /// <returns>
        /// The remote <see cref="T:System.ServiceModel.EndpointAddress"/> for the channel.
        /// </returns>
        public EndpointAddress RemoteAddress
        {
            get { return InnerDuplexChannel.RemoteAddress; }
        }

        /// <summary>
        /// Returns an identifier for the current session, if any.
        /// </summary>
        /// <returns>
        /// The current session identifier, if any.
        /// </returns>
        public string SessionId
        {
            get { return InnerDuplexChannel.SessionId; }
        }

        #endregion

        #region Implementation of IClientChannel

        /// <summary>
        /// An asynchronous call to begin using a user interface to obtain credential information.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> to use to call back when processing has completed.
        /// </returns>
        /// <param name="callback">
        /// The method that is called when this method completes.
        /// </param>
        /// <param name="state">
        /// Information about the state of the channel.
        /// </param>
        public IAsyncResult BeginDisplayInitializationUI(AsyncCallback callback, object state)
        {
            return InnerChannel.BeginDisplayInitializationUI(callback, state);
        }

        /// <summary>
        /// Called when the call to <see cref="M:System.ServiceModel.IClientChannel.BeginDisplayInitializationUI(System.AsyncCallback,System.Object)"/> 
        /// has finished.
        /// </summary>
        /// <param name="result">
        /// The <see cref="T:System.IAsyncResult"/>.
        /// </param>
        public void EndDisplayInitializationUI(IAsyncResult result)
        {
            InnerChannel.EndDisplayInitializationUI(result);
        }

        /// <summary>
        /// Gets or sets a value indicating whether <see cref="M:System.ServiceModel.IClientChannel.DisplayInitializationUI"/> 
        /// attempts to call the <see cref="T:System.ServiceModel.Dispatcher.IInteractiveChannelInitializer"/> objects in the 
        /// <see cref="P:System.ServiceModel.Dispatcher.ClientRuntime.InteractiveChannelInitializers"/> property or throws 
        /// if that collection is not empty. 
        /// </summary>
        /// <returns>
        /// true if Windows Communication Foundation (WCF) is permitted to invoke interactive channel initializers; otherwise, false. 
        /// </returns>
        public bool AllowInitializationUI
        {
            get { return InnerChannel.AllowInitializationUI; }
            set { InnerChannel.AllowInitializationUI = value; }
        }

        /// <summary>
        /// Gets a value indicating whether a call was done to a user interface to obtain credential information. 
        /// </summary>
        /// <returns>
        /// true if the <see cref="M:System.ServiceModel.IClientChannel.DisplayInitializationUI"/> method was called 
        /// (or the <see cref="M:System.ServiceModel.IClientChannel.BeginDisplayInitializationUI(System.AsyncCallback,System.Object)"/> 
        /// and <see cref="M:System.ServiceModel.IClientChannel.EndDisplayInitializationUI(System.IAsyncResult)"/> methods; otherwise, false.
        /// </returns>
        public bool DidInteractiveInitialization
        {
            get { return InnerChannel.DidInteractiveInitialization; }
        }

        /// <summary>
        /// Gets the URI that contains the transport address to which messages are sent on the client channel.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Uri"/> that contains the transport address to which messages are sent on the channel.
        /// </returns>
        public Uri Via
        {
            get { return InnerChannel.Via; }
        }

        /// <summary>
        /// Reserved. 
        /// </summary>
        public event EventHandler<UnknownMessageReceivedEventArgs> UnknownMessageReceived;

        #endregion
    }
}


