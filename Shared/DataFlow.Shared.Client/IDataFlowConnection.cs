using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AISTek.DataFlow.Shared.Client
{
    public interface IDataFlowConnection : IDisposable
    {
        IJobManagerClient JobManager { get; }

        IRepositoryClient Repository { get; }
    }
}
