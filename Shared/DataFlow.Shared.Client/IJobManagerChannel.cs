using System.ServiceModel;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.Shared.Client
{
    /// <summary>
    /// Defines the behavior of outbound request and request/reply channels used by client applications
    /// for service <see cref="IJobManager"/>
    /// </summary>
    public interface IJobManagerClient : IJobManager, IClientChannel
    { }
}


