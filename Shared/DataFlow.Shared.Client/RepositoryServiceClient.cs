using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using AISTek.DataFlow.Shared.Classes;
using AISTek.DataFlow.Shared.Classes.RepositoryService;

namespace AISTek.DataFlow.Shared.Client
{
    /// <summary>
    /// Client proxy class for <see cref="IRepositoryService"/>.
    /// </summary>
    [DebuggerStepThrough]
    [Serializable]
    internal sealed class RepositoryServiceClient : ClientBase<IRepositoryService>, IRepositoryServiceChannel
    {
        #region Constructors

        internal RepositoryServiceClient()
        { }

        internal RepositoryServiceClient(string endpointConfigurationName)
            : base(endpointConfigurationName)
        { }

        internal RepositoryServiceClient(string endpointConfigurationName, string remoteAddress)
            : base(endpointConfigurationName, remoteAddress)
        { }

        internal RepositoryServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress)
            : base(endpointConfigurationName, remoteAddress)
        { }

        internal RepositoryServiceClient(Binding binding, EndpointAddress remoteAddress)
            : base(binding, remoteAddress)
        { }

        #endregion

        #region Implementation of IRepositoryService

        /// <summary>
        /// Creates new file.
        /// </summary>
        /// <param name="fileName">
        /// Non-unique name of new file.
        /// </param>
        /// <returns>
        /// An instance of <see cref="RemoteFileInfo"/> that contains GUID of the new file.
        /// </returns>
        public RemoteFileInfo CreateNew(string fileName)
        {
            return Channel.CreateNew(fileName);
        }

        /// <summary>
        /// Deletes the file from repository.
        /// </summary>
        /// <param name="fileUid">
        /// File's GUID.
        /// </param>
        public void Delete(Guid fileUid)
        {
            Channel.Delete(fileUid);
        }

        /// <summary>
        /// Queries file's information.
        /// </summary>
        /// <param name="id">
        /// File's GUID.
        /// </param>
        /// <returns>
        /// An instance of <see cref="RemoteFileInfo"/> that contains file's info.
        /// </returns>
        public RemoteFileInfo QueryFileInfo(Guid id)
        {
            return Channel.QueryFileInfo(id);
        }

        /// <summary>
        /// Browses all the files in repository.
        /// </summary>
        /// <param name="beginFrom">
        /// A zero-based index of element to start from.
        /// </param>
        /// <param name="itemsCount">
        /// An amount of items to show. Must be greater than zero.
        /// </param>
        /// <returns>
        /// A list of <see cref="RemoteFileInfo"/> that contains all available items in the requested range.
        /// </returns>
        public IEnumerable<RemoteFileInfo> Browse(int beginFrom, int itemsCount)
        {
            return Channel.Browse(beginFrom, itemsCount);
        }

        #endregion

        #region Implementation of IChannel

        /// <summary>
        /// Returns a typed object requested, if present, from the appropriate layer in the channel stack.
        /// </summary>
        /// <returns>
        /// The typed object <typeparamref name="T"/> requested if it is present or null if it is not.
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
        public event EventHandler<UnknownMessageReceivedEventArgs> UnknownMessageReceived
        {
            add { InnerChannel.UnknownMessageReceived += value; }
            remove { InnerChannel.UnknownMessageReceived -= value; }
        }

        #endregion
    }
}


