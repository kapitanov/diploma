using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AISTek.DataFlow.Shared.Classes;
using System.ServiceModel;

namespace AISTek.DataFlow.Shared.Client
{
    public interface IRepositoryClient : IRepository, IClientChannel
    { }
}
