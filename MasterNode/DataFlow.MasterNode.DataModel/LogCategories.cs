using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace AISTek.DataFlow.MasterNode.DataModel
{
    public static class LogCategories
    {
        #region Subsystems

        public static string Core = "Core";

        public static string TaskServices = "TaskServices";

        public static string Repository = "Repository";
        
        #endregion

        #region Components

        public static string TaskQueue = "TaskQueue";

        public static string TaskProviderService = "TaskProviderService";

        public static string JobScheduler = "JobScheduler";

        public static string JobManagerService = "JobManagerService";

        public static string JobBuildingPipeline = "JobBuildingPipeline";

        public static string JobGraphCompletionStrategy = "JobGraphCompletionStrategy";

        public static string RepositoryDataAccessLayer = "Repository DAL";

        #endregion


        public static LogEntry Information(string message, params string[] categories)
        {
            return CreateLogEntry(message, TraceEventType.Information, categories);
        }

        public static LogEntry Warning(string message, params string[] categories)
        {
            return CreateLogEntry(message, TraceEventType.Warning, categories);
        }

        public static LogEntry Error(string message, params string[] categories)
        {
            return CreateLogEntry(message, TraceEventType.Error, categories);
        }

        public static LogEntry Critical(string message, params string[] categories)
        {
            return CreateLogEntry(message, TraceEventType.Critical, categories);
        }

        private static LogEntry CreateLogEntry(string message, TraceEventType severity, params string[] categories)
        {
            return new LogEntry { Message = message, Severity = severity, Categories = categories, Title = message };
        }
    }
}
