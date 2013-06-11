using System.ServiceModel;
using AISTek.DataFlow.Shared.Classes;
using AISTek.DataFlow.Shared.Classes.RepositoryService;

namespace AISTek.DataFlow.Shared.Client
{
    /// <summary>
    /// Defines the behavior of outbound request and request/reply channels used by client applications
    /// for service <see cref="IRepositoryService"/>
    /// </summary>
    internal interface IRepositoryServiceChannel : IRepositoryService, IClientChannel
    { }
}


