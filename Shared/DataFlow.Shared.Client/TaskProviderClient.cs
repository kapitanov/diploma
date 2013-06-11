using System;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.Shared.Client
{
    /// <summary>
    /// Client proxy class for <see cref="ITaskProvider"/>.
    /// </summary>
    [DebuggerStepThrough]
    [Serializable]
    internal sealed class TaskProviderClient : ClientBase<ITaskProvider>, ITaskProviderChannel
    {
        #region Constructors

        internal TaskProviderClient()
            : base()
        { }

        internal TaskProviderClient(string endpointConfigurationName)
            : base(endpointConfigurationName)
        { }

        internal TaskProviderClient(string endpointConfigurationName, string remoteAddress)
            : base(endpointConfigurationName, remoteAddress)
        { }

        internal TaskProviderClient(string endpointConfigurationName, EndpointAddress remoteAddress)
            : base(endpointConfigurationName, remoteAddress)
        { }

        internal TaskProviderClient(Binding binding, EndpointAddress remoteAddress)
            : base(binding, remoteAddress)
        { } 

        #endregion

        #region Implementation of ITaskProvider

        /// <summary>
        /// Gets a <see cref="TimeSpan"/> that defines worker notifications interval.
        /// </summary>
        /// <returns>
        /// An instance of <see cref="TimeSpan"/> that contains worker notification period.
        /// </returns>
        public TimeSpan QueryNotifyInterval()
        {
            return Channel.QueryNotifyInterval();
        }

        /// <summary>
        /// Request a task for execution.
        /// </summary>
        /// <returns>
        /// Blocks current thread until a task will be available for execution.
        /// </returns>
        public Task GetTask()
        {
            return Channel.GetTask();
        }

        /// <summary>
        /// Informs scheduler that task with id = <paramref name="taskId"/> was rejected for execution.  
        /// </summary>
        /// <param name="taskId">
        /// Task's unique identifier.
        /// </param>
        public void RejectTask(Guid taskId)
        {
            Channel.RejectTask(taskId);
        }
        
        /// <summary>
        /// Informs scheduler that task with id = <paramref name="taskId"/> has thrown an exception <paramref name="errorReport"/>.  
        /// </summary>
        /// <param name="taskId">
        /// Task's unique identifier.
        /// </param>
        /// <param name="errorReport">
        /// Error report.
        /// </param>
        public void FailTask(Guid taskId, ErrorReport errorReport)
        {
            Channel.FailTask(taskId, errorReport);
        }

        /// <summary>
        /// Informs scheduler that task with id = <paramref name="taskId"/> has been successfully executed.
        /// </summary>
        /// <param name="taskId">
        /// Task's unique identifier.
        /// </param>
        public void CompleteTask(Guid taskId)
        {
            Channel.CompleteTask(taskId);
        }

        /// <summary>
        /// Informs scheduler that task with id = <paramref name="taskId"/> is being executed.
        /// </summary>
        /// <param name="taskId">
        /// Task's unique identifier.
        /// </param>
        public void NotifyWorker(Guid taskId)
        {
            Channel.NotifyWorker(taskId);
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
        public T GetProperty<T>() where T : class
        {
            return InnerChannel.GetProperty<T>();
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
            get { return InnerChannel.Extensions; }
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
            get { return InnerChannel.AllowOutputBatching; }
            set { InnerChannel.AllowOutputBatching = value; }
        }

        /// <summary>
        /// Gets the input session for the channel.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.ServiceModel.Channels.IInputSession"/> for the channel.
        /// </returns>
        public IInputSession InputSession
        {
            get { return InnerChannel.InputSession; }
        }

        /// <summary>
        /// Gets the local endpoint for the channel.
        /// </summary>
        /// <returns>
        /// The local <see cref="T:System.ServiceModel.EndpointAddress"/> for the channel.
        /// </returns>
        public EndpointAddress LocalAddress
        {
            get { return InnerChannel.LocalAddress; }
        }

        /// <summary>
        /// Gets or sets the time period within which an operation must complete or an exception is thrown.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.TimeSpan"/> that specifies the time period within which an operation must complete.
        /// </returns>
        public TimeSpan OperationTimeout
        {
            get { return InnerChannel.OperationTimeout; }
            set { InnerChannel.OperationTimeout = value; }
        }

        /// <summary>
        /// Gets the output session associated with the channel, if any.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.ServiceModel.Channels.IOutputSession"/> implementation if an input session exists; otherwise, null.
        /// </returns>
        public IOutputSession OutputSession
        {
            get { return InnerChannel.OutputSession; }
        }

        /// <summary>
        /// Gets the remote address associated with the channel.
        /// </summary>
        /// <returns>
        /// The remote <see cref="T:System.ServiceModel.EndpointAddress"/> for the channel.
        /// </returns>
        public EndpointAddress RemoteAddress
        {
            get { return InnerChannel.RemoteAddress; }
        }

        /// <summary>
        /// Returns an identifier for the current session, if any.
        /// </summary>
        /// <returns>
        /// The current session identifier, if any.
        /// </returns>
        public string SessionId
        {
            get { return InnerChannel.SessionId; }
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


