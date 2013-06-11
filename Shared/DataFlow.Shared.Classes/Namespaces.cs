namespace AISTek.DataFlow.Shared.Classes
{
    /// <summary>
    /// WCF services' namespaces, configuration and action names.
    /// </summary>
    public static class Namespaces
    {
        /// <summary>
        /// System's root Uri
        /// </summary>
        public const string RootNamespace = "http://aistek.dataflow";

        /// <summary>
        /// Services' root Uri
        /// </summary>
        public const string ServicesNamespace = RootNamespace + "/services";
        
        /// <summary>
        /// Defines constants for repository subsystem.
        /// </summary>
        public static class Repository
        {
            /// <summary>
            /// Repository services' uri
            /// </summary>
            public const string Namespace = ServicesNamespace + "/repository/";

            public const string Configuration = "DataFlow.Repository.IRepositoryService";

            public static class Actions
            {
                private const string Base = Namespace + "IRepositoryService/";

                public const string Browse = Base + "Browse";
                public const string BrowseReply = Base + "BrowseResponse";
                public const string CreateNew = Base + "CreateNew";
                public const string CreateNewReply = Base + "CreateNewResponse";
                public const string Delete = Base + "Delete";
                public const string QueryFileInfo = Base + "QueryFileInfo";
                public const string QueryFileInfoReply = Base + "QueryFileInfoResponse";   
            }           
            
        }

        public static class Scheduler
        {
            /// <summary>
            /// Scheduler services' Uri
            /// </summary>
            public const string Namespace = ServicesNamespace + "/scheduler/";
            
            public static class TaskProvider
            {
                public const string Configuration = "DataFlow.Scheduler.ITaskProvider";

                public static class Actions
                {
                    private const string Base = Namespace + "ITaskProvider/";

                    public const string QueryNotifyInterval = Base + "QueryNotifyInterval";
                    public const string QueryNotifyIntervalResponse = Base + "QueryNotifyInterval";
                    public const string GetTask = Base + "GetTask";
                    public const string GetTaskResponse = Base + "GetTaskResponse";
                    public const string RejectTask = Base + "RejectTask";
                    public const string RejectTaskResponse = Base + "RejectTaskResponse";
                    public const string FailTask = Base + "FailTask";
                    public const string FailTaskResponse = Base + "FailTaskResponse";
                    public const string CompleteTask = Base + "CompleteTask";
                    public const string CompleteTaskResponse = Base + "CompleteTaskResponse";
                    public const string NotifyWorker = Base + "NotifyWorker";
                    public const string NotifyWorkerResponse = Base + "NotifyWorkerResponse";
                }
            }

            public static class JobManager
            {
                public const string Configuration = "DataFlow.Scheduler.IJobManager";

                private const string Base = Namespace + "IJobManager/";

                public static class Actions
                {
                    public const string CreateJob = Base + "CreateJob";
                    public const string CreateJobResponse = Base + "CreateJobResponse";
                    public const string OpenJob = Base + "OpenJob";
                    public const string CreateTask = Base + "CreateTask";
                    public const string CreateTaskResponse = Base + "CreateTaskResponse";
                    public const string UpdateTask = Base + "UpdateTask";
                    public const string RemoveTask = Base + "RemoveTask";
                    public const string StartJob = Base + "StartJob";
                    public const string CancelJob = Base + "CancelJob";
                    public const string RestartJob = Base + "RestartJob";
                    public const string DeleteJob = Base + "DeleteJob";
                    public const string FindJobs = Base + "FindJobs";
                    public const string FindJobsResponse = Base + "FindJobsResponse";
                    public const string GetAllJobs = Base + "GetAllJobs";
                    public const string GetAllJobsResponse = Base + "GetAllJobsResponse";
                    public const string GetCurrentJob = Base + "get_CurrentJob";
                    public const string GetCurrentJobResponse = Base + "get_CurrentJobResponse";
                    public const string QueryJobState = Base + "QueryJobState";
                    public const string QueryJobStateResponse = Base + "QueryJobStateResponse";
                    public const string GetErrorReport = Base + "GetErrorReport";
                    public const string GetErrorReportResponse = Base + "GetErrorReportResponse";
                }

                public static class Callback
                {
                    public const string JobCompleted = Base + "JobCompleted";
                    public const string JobFailed = Base + "JobFailed";
                }
            }
        }
    }
}