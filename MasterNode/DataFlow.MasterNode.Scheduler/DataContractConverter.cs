using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.MasterNode.Scheduler
{
    internal static class DataContractConverter
    {
        #region Convertions into contract types

        /// <summary>
        /// Converts an instance of <see cref="DataModel.EntryPoint"/> 
        /// into an instance of <see cref="EntryPoint"/>
        /// </summary>
        /// <param name="entryPoint">
        /// An instance of <see cref="DataModel.EntryPoint"/>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="EntryPoint"/> equalent to <paramref name="entryPoint"/> 
        /// </returns>
        public static EntryPoint ToContract(this DataModel.EntryPoint entryPoint)
        {
            Contract.Requires(entryPoint != null);
            Contract.Requires(entryPoint.DependentAssemblyIds != null);
            Contract.Requires(!string.IsNullOrEmpty(entryPoint.QualifiedClassName));
            Contract.Ensures(Contract.Result<EntryPoint>() != null);

            return new EntryPoint
                       {
                           AssemblyId = entryPoint.AssemblyId,
                           DependentAssemblyIds = entryPoint.DependentAssemblyIds,
                           QualifiedClassName = entryPoint.QualifiedClassName
                       };
        }

        /// <summary>
        /// Converts an instance of <see cref="DataModel.FileLink"/> 
        /// into an instance of <see cref="FileLink"/>
        /// </summary>
        /// <param name="fileLink">
        /// An instance of <see cref="DataModel.FileLink"/>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="FileLink"/> equalent to <paramref name="fileLink"/> 
        /// </returns>
        public static FileLink ToContract(this DataModel.FileLink fileLink)
        {
            Contract.Requires(fileLink != null);
            Contract.Requires(fileLink.Metadata != null);
            Contract.Ensures(Contract.Result<FileLink>() != null);

            return new FileLink
                       {
                           Id = fileLink.Id,
                           Metadata = new Dictionary<string, string>(fileLink.Metadata)
                       };
        }

        /// <summary>
        /// Converts an instance of <see cref="DataModel.Task"/> 
        /// into an instance of <see cref="Task"/>
        /// </summary>
        /// <param name="task">
        /// An instance of <see cref="DataModel.Task"/>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Task"/> equalent to <paramref name="task"/> 
        /// </returns>
        public static Task ToContract(this DataModel.Task task)
        {
            Contract.Requires(task != null);
            Contract.Requires(task.InputFiles != null);
            Contract.Requires(task.OutputFiles != null);
            Contract.Requires(task.EntryPoint != null);
            Contract.Requires(task.EntryPoint.DependentAssemblyIds != null);
            Contract.Requires(!string.IsNullOrEmpty(task.EntryPoint.QualifiedClassName));
            Contract.Requires(task.Inputs != null);
            Contract.Requires(task.Outputs != null);
            Contract.Requires(task.Parameters != null);
            Contract.Ensures(Contract.Result<Task>() != null);

            var r = new Task
                       {
                           Id = task.Id,
                           Name = task.Name,
                           EntryPoint = task.EntryPoint.ToContract(),
                           InputFiles = task.InputFiles.Select(file => file.ToContract()).ToList(),
                           OutputFiles = task.OutputFiles.Select(file => file.ToContract()).ToList(),
                           Inputs = task.Inputs.Select(t => t.ToFlatContract()).ToList(),
                           Outputs = task.Outputs.Select(t => t.ToFlatContract()).ToList(),
                           Parameters = new Dictionary<string, string>(task.Parameters)
                       };

            return r;
        }

        /// <summary>
        /// Converts an instance of <see cref="DataModel.Task"/> 
        /// into an instance of <see cref="Task"/>
        /// </summary>
        /// <param name="task">
        /// An instance of <see cref="DataModel.Task"/>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Task"/> equalent to <paramref name="task"/> 
        /// </returns>
        private static Task ToFlatContract(this DataModel.Task task)
        {
            Contract.Requires(task != null);
            Contract.Requires(task.InputFiles != null);
            Contract.Requires(task.EntryPoint != null);
            Contract.Requires(task.EntryPoint.DependentAssemblyIds != null);
            Contract.Requires(!string.IsNullOrEmpty(task.EntryPoint.QualifiedClassName));
            Contract.Requires(task.Outputs != null);
            Contract.Requires(task.OutputFiles != null);
            Contract.Requires(task.Parameters != null);
            Contract.Ensures(Contract.Result<Task>() != null);

            return new Task
            {
                Id = task.Id,
                Name = task.Name,
                EntryPoint = task.EntryPoint.ToContract(),
                InputFiles = task.InputFiles.Select(file => file.ToContract()).ToList(),
                OutputFiles = task.OutputFiles.Select(file => file.ToContract()).ToList(),
                Inputs = Enumerable.Empty<Task>().ToList(),
                Outputs = Enumerable.Empty<Task>().ToList(),
                Parameters = new Dictionary<string, string>(task.Parameters)
            };
        }

        /// <summary>
        /// Converts an instance of <see cref="DataModel.Job"/> 
        /// into an instance of <see cref="Job"/>
        /// </summary>
        /// <param name="job">
        /// An instance of <see cref="DataModel.Job"/>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Job"/> equals to <paramref name="job"/> 
        /// </returns>
        public static Job ToContract(this DataModel.Job job)
        {
            Contract.Requires(job != null);
            Contract.Requires(job.Tasks != null);

            return new Job
                       {
                           Id = job.Id,
                           Name = job.Name,
                           Tasks = job.Tasks.Select(task => task.ToContract()).ToList(),
                           State = (JobState)((int)job.State)
                       };
        }

        #endregion

        #region Convertions from contract types

        /// <summary>
        /// Converts an instance of <see cref="EntryPoint"/> 
        /// into an instance of <see cref="DataModel.EntryPoint"/>
        /// </summary>
        /// <param name="entryPoint">
        /// An instance of <see cref="EntryPoint"/>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="DataModel.EntryPoint"/> equals to <paramref name="entryPoint"/> 
        /// </returns>
        public static DataModel.EntryPoint FromContract(this EntryPoint entryPoint)
        {
            Contract.Requires(entryPoint != null);
            Contract.Requires(entryPoint.DependentAssemblyIds != null);
            Contract.Requires(!string.IsNullOrEmpty(entryPoint.QualifiedClassName));
            Contract.Ensures(Contract.Result<DataModel.EntryPoint>() != null);

            return new DataModel.EntryPoint
            {
                AssemblyId = entryPoint.AssemblyId,
                DependentAssemblyIds = entryPoint.DependentAssemblyIds,
                QualifiedClassName = entryPoint.QualifiedClassName
            };
        }

        /// <summary>
        /// Converts an instance of <see cref="FileLink"/> 
        /// into an instance of <see cref="DataModel.FileLink"/>
        /// </summary>
        /// <param name="fileLink">
        /// An instance of <see cref="FileLink"/>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="DataModel.FileLink"/> equals to <paramref name="fileLink"/> 
        /// </returns>
        public static DataModel.FileLink FromContract(this FileLink fileLink)
        {
            Contract.Requires(fileLink != null);
            Contract.Requires(fileLink.Metadata != null);
            Contract.Ensures(Contract.Result<DataModel.FileLink>() != null);

            return new DataModel.FileLink(fileLink.Id, fileLink.Name, fileLink.Metadata);
        }

        /// <summary>
        /// Converts an instance of <see cref="Task"/> 
        /// into an instance of <see cref="DataModel.Task"/>
        /// </summary>
        /// <param name="task">
        /// An instance of <see cref="Task"/>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="DataModel.Task"/> equals to <paramref name="task"/> 
        /// </returns>
        public static DataModel.Task FromContract(this Task task, IEnumerable<DataModel.Task> tasks)
        {
            Contract.Requires(task != null);
            Contract.Requires(task.InputFiles != null);
            Contract.Requires(task.EntryPoint != null);
            Contract.Requires(task.EntryPoint.DependentAssemblyIds != null);
            Contract.Requires(!string.IsNullOrEmpty(task.EntryPoint.QualifiedClassName));
            Contract.Requires(task.Inputs != null);
            Contract.Requires(task.Outputs != null);
            Contract.Requires(task.OutputFiles != null);
            Contract.Requires(task.Parameters != null);
            Contract.Ensures(Contract.Result<DataModel.Task>() != null);

            return new DataModel.Task(task.Name, task.EntryPoint.FromContract())
                       {
                           Id = task.Id,
                           InputFiles = task.InputFiles.Select(file => file.FromContract()).ToList(),
                           OutputFiles = task.OutputFiles.Select(file => file.FromContract()).ToList(),
                           Inputs = task.Inputs.Select(t => tasks.First(x => x.Id == t.Id)).ToList(),
                           Outputs = task.Outputs.Select(t => t.FromContract(tasks)).ToList(),
                           Parameters = new Dictionary<string, string>(task.Parameters)
                       };
        }

        /// <summary>
        /// Converts an instance of <see cref="Job"/> 
        /// into an instance of <see cref="DataModel.Job"/>
        /// </summary>
        /// <param name="job">
        /// An instance of <see cref="Job"/>.
        /// </param>
        /// <returns>
        /// An instance of <see cref="DataModel.Job"/> equalent to <paramref name="job"/> 
        /// </returns>
        public static DataModel.Job FromContract(this Job job)
        {
            Contract.Requires(job != null);
            Contract.Requires(job.Tasks != null);

            return new DataModel.Job(job.Name, job.Tasks.Select(task => task.FromContract(Enumerable.Empty<DataModel.Task>())))
            {
                Id = job.Id,
                State = (DataModel.JobState)((int)job.State)
            };
        }

        #endregion
    }
}


