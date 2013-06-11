using System;
using System.ServiceModel;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.MasterNode.Core.Services
{
    [ServiceContract(Namespace = Namespaces.Scheduler.Namespace, SessionMode = SessionMode.NotAllowed)]
    public interface IServerStatus
    {
        [OperationContract]
        Version GetServerVersion();

        [OperationContract]
        Event[] GetAllDebugEvents();
    }
}
