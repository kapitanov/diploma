using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AISTek.DataFlow.Shared.Classes;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.IO;

namespace AISTek.DataFlow.Shared.Client
{
    internal class RepositoryClient : IRepositoryClient
    {
        public RepositoryClient(IRepositoryServiceChannel channel)
        {
            Channel = channel;
        }

        public IRepositoryServiceChannel Channel { get; private set; }

        #region Implementation of IRepository

        /// <summary>
        /// Creates new file.
        /// </summary>
        /// <param name="fileName">
        /// Non-unique name of new file.
        /// </param>
        /// <returns>
        /// GUID of the new file.
        /// </returns>
        public Guid CreateNew(string fileName)
        {
            return Channel.CreateNew(fileName).Id;
        }

        /// <summary>
        /// Deletes the file from repository.
        /// </summary>
        /// <param name="fileUid">
        /// File's GUID.
        /// </param>
        public void Delete(Guid id)
        {
            var fileInfo = Channel.QueryFileInfo(id);
            fileInfo.Uri.DeleteFile();
            Channel.Delete(id);
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
        /// A list of <see cref="FileName"/> that contains all available items in the requested range.
        /// </returns>
        public IEnumerable<FileName> Browse(int beginFrom, int itemsCount)
        {
            return from fileInfo in Channel.Browse(beginFrom, itemsCount)
                   select new FileName(fileInfo.Id, fileInfo.Name);
        }

        /// <summary>
        /// Uploads file to repository.
        /// </summary>
        /// <param name="fileId">
        /// File's GUID.
        /// </param>
        /// <param name="dataStream">
        /// Data stream.
        /// </param>
        public void Upload(Guid fileId, Stream dataStream)
        {
            var fileInfo = Channel.QueryFileInfo(fileId);
            fileInfo.Uri.WriteFile(dataStream);
        }

        /// <summary>
        /// Uploads new file to repository.
        /// </summary>
        /// <param name="name">
        /// Name of the new file.
        /// </param>
        /// <param name="dataStream">
        /// Data stream.
        /// </param>
        /// <returns>
        /// New file's GUID.
        /// </returns>
        public Guid UploadNew(string name, Stream dataStream)
        {
            var fileInfo = Channel.CreateNew(name);
            fileInfo.Uri.WriteFile(dataStream);
            return fileInfo.Id;
        }

        /// <summary>
        /// Downloads file from repository.
        /// </summary>
        /// <param name="id">
        /// File's GUID.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Stream"/> that contains file's data.
        /// </returns>
        public Stream Download(Guid id, bool sharedAccess)
        {
            var fileInfo = Channel.QueryFileInfo(id);
            var stream = fileInfo.Uri.ReadFile(sharedAccess);
            return stream;
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
            return Channel.GetProperty<T>();
        }

        public void DisplayInitializationUI()
        {
            Channel.DisplayInitializationUI();
        }

        public void Abort()
        {
            Channel.Abort();
        }

        public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
        {
            return Channel.BeginClose(timeout, callback, state);
        }

        public IAsyncResult BeginClose(AsyncCallback callback, object state)
        {
            return Channel.BeginClose(callback, state);

        }

        public IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
        {
            return Channel.BeginOpen(timeout, callback, state);
        }

        public IAsyncResult BeginOpen(AsyncCallback callback, object state)
        {
            return Channel.BeginOpen(callback, state);
        }

        public void Close(TimeSpan timeout)
        {
            Channel.Close(timeout);
        }

        public void Close()
        {
            Channel.Close();
        }

        public event EventHandler Closed
        {
            add { Channel.Closed += value; }
            remove { Channel.Closed -= value; }
        }

        public event EventHandler Closing
        {
            add { Channel.Closing += value; }
            remove { Channel.Closing -= value; }
        }

        public void EndClose(IAsyncResult result)
        {
            Channel.EndClose(result);
        }

        public void EndOpen(IAsyncResult result)
        {
            Channel.EndOpen(result);
        }

        public event EventHandler Faulted
        {
            add { Channel.Faulted += value; }
            remove { Channel.Faulted -= value; }
        }

        public void Open(TimeSpan timeout)
        {
            Channel.Open(timeout);
        }

        public void Open()
        {
            Channel.Open();
        }

        public event EventHandler Opened
        {
            add { Channel.Opened += value; }
            remove { Channel.Opened -= value; }
        }

        public event EventHandler Opening
        {
            add { Channel.Opening += value; }
            remove { Channel.Opening -= value; }
        }

        public CommunicationState State
        {
            get { return Channel.State; }
        }

        public void Dispose()
        {
            Channel.Dispose();
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
            get { return Channel.Extensions; }
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
            get { return Channel.AllowOutputBatching; }
            set { Channel.AllowOutputBatching = value; }
        }

        /// <summary>
        /// Gets the input session for the channel.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.ServiceModel.Channels.IInputSession"/> for the channel.
        /// </returns>
        public IInputSession InputSession
        {
            get { return Channel.InputSession; }
        }

        /// <summary>
        /// Gets the local endpoint for the channel.
        /// </summary>
        /// <returns>
        /// The local <see cref="T:System.ServiceModel.EndpointAddress"/> for the channel.
        /// </returns>
        public EndpointAddress LocalAddress
        {
            get { return Channel.LocalAddress; }
        }

        /// <summary>
        /// Gets or sets the time period within which an operation must complete or an exception is thrown.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.TimeSpan"/> that specifies the time period within which an operation must complete.
        /// </returns>
        public TimeSpan OperationTimeout
        {
            get { return Channel.OperationTimeout; }
            set { Channel.OperationTimeout = value; }
        }

        /// <summary>
        /// Gets the output session associated with the channel, if any.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.ServiceModel.Channels.IOutputSession"/> implementation if an input session exists; otherwise, null.
        /// </returns>
        public IOutputSession OutputSession
        {
            get { return Channel.OutputSession; }
        }

        /// <summary>
        /// Gets the remote address associated with the channel.
        /// </summary>
        /// <returns>
        /// The remote <see cref="T:System.ServiceModel.EndpointAddress"/> for the channel.
        /// </returns>
        public EndpointAddress RemoteAddress
        {
            get { return Channel.RemoteAddress; }
        }

        /// <summary>
        /// Returns an identifier for the current session, if any.
        /// </summary>
        /// <returns>
        /// The current session identifier, if any.
        /// </returns>
        public string SessionId
        {
            get { return Channel.SessionId; }
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
            return Channel.BeginDisplayInitializationUI(callback, state);
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
            Channel.EndDisplayInitializationUI(result);
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
            get { return Channel.AllowInitializationUI; }
            set { Channel.AllowInitializationUI = value; }
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
            get { return Channel.DidInteractiveInitialization; }
        }

        /// <summary>
        /// Gets the URI that contains the transport address to which messages are sent on the client channel.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Uri"/> that contains the transport address to which messages are sent on the channel.
        /// </returns>
        public Uri Via
        {
            get { return Channel.Via; }
        }

        /// <summary>
        /// Reserved. 
        /// </summary>
        public event EventHandler<UnknownMessageReceivedEventArgs> UnknownMessageReceived
        {
            add { Channel.UnknownMessageReceived += value; }
            remove { Channel.UnknownMessageReceived -= value; }
        }

        #endregion
    }
}
