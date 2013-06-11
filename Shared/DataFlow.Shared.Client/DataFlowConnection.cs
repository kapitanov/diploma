using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.Shared.Client
{
    public class DataFlowConnection : IDataFlowConnection
    {
        private DataFlowConnection (IJobManagerClient jobManager, IRepositoryClient repository)
        {
            if (jobManager == null)
                throw new ArgumentNullException("jobManager");
            if (repository == null)
                throw new ArgumentNullException("repository");

            JobManager = jobManager;
            Repository = repository;
        }

        public IJobManagerClient JobManager { get; private set; }

        public IRepositoryClient Repository { get; private set; }

        public void Dispose()
        {
            if (JobManager != null)
                JobManager.Dispose();

            if (Repository != null)
                Repository.Dispose();
        }

        public static IDataFlowConnection CreateDefaultConnection()
        {
            var repositoryClientFactory = new RepositoryClientFactory();
            var jobManagerClientFactory = new JobManagerClientFactory();

            return new DataFlowConnection(
                jobManagerClientFactory.CreateClient(JobManagerCallback),
                repositoryClientFactory.CreateClient());
        }

        public static IDataFlowConnection CreateConnection(string jobManagerConfigurationName, string repositoryConfigurationName)
        {
            var repositoryClientFactory = new RepositoryClientFactory();
            var jobManagerClientFactory = new JobManagerClientFactory();

            return new DataFlowConnection(
                jobManagerClientFactory.CreateClient(JobManagerCallback, jobManagerConfigurationName),
                repositoryClientFactory.CreateClient(repositoryConfigurationName));
        }

        private static IJobManagerCallback JobManagerCallback = new JobManagerCallbackStub();

        private sealed class JobManagerCallbackStub : IJobManagerCallback
        {
            public void JobCompleted(Job job)
            { }

            public void JobFailed(Job job)
            { }
        }
    }
}
