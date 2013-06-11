using System.ServiceModel;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.Shared.Client
{
    /// <summary>
    /// Defines the behavior of outbound request and request/reply channels used by client applications
    /// for service <see cref="ITaskProvider"/>
    /// </summary>
    public interface ITaskProviderChannel : ITaskProvider, IClientChannel
    { }
}


