using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AISTek.DataFlow.Util;
using AISTek.DataFlow.Shared.Client;
using System.Reflection;
using AISTek.DataFlow.Shared.DataExchange;

namespace AISTek.DataFlow.Shared.Patterns
{
    internal class MapReducePattern<TInput, TIntermediate, TOutput> : IMapReduce<TInput, TIntermediate, TOutput>
    {
        public MapReducePattern(
            IDataFlowConnection connection,
            DataContractOptions dtoOptions,
            Type mapTaskType,
            Type reduceTaskType)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            if (dtoOptions == null)
                throw new ArgumentNullException("dtoOptions");
            if (mapTaskType == null)
                throw new ArgumentNullException("mapTaskType");
            if (reduceTaskType == null)
                throw new ArgumentNullException("reduceTaskType");

            this.connection = connection;
            this.dtoOptions = dtoOptions;
            this.mapTaskType = mapTaskType;
            this.reduceTaskType = reduceTaskType;
        }

        public IResult<TOutput> Execute(IEnumerable<TInput> inputs)
        {
            var job = CreateJob(inputs);

            return new MapReduceResult<TOutput>(job, dtoOptions).Startup(connection);
        }

        private Japi.Job CreateJob(IEnumerable<TInput> inputs)
        {
            // Create job
            var job = new Japi.Job { Name = Constants.JobName + DateTime.Now.ToUniversalTime() };

            // Create runtime task's entrypoint
            // TODO: implement correct assembly referencing
            var assembly = new Japi.UploadAssembly(typeof(RuntimeTask).Assembly);
            var entryPoint = new Japi.EntryPoint
            {
                Assembly = assembly,
                References = //new AssemblyReferences(mapTaskType.Assembly).Build().ToList(),
                { 
                    new Japi.UploadAssembly(mapTaskType.Assembly), 
                    //new Japi.UploadAssembly(typeof(AISTek.DataFlow.Shared.DataExchange.DataContractExtensions).Assembly),
                    new Japi.UploadAssembly(typeof(IGenericTask<,>).Assembly) 
                },
                ClassName = typeof(RuntimeTask).FullName
            };

            // Create "map" tasks
            var mapTasks = new List<Japi.Task>();
            foreach (var pair in inputs.Select((input, n) => new { Index = n, Input = input }))
            {
                mapTasks.Add(new Japi.Task
                {
                    EntryPoint = entryPoint,
                    InputFiles = { new UploadObject<TInput>(Constants.FileName_Input + pair.Index, pair.Input, dtoOptions).DataType<TInput>() },
                    Name = Constants.TaskName_Map + pair.Index,
                    Parameters = 
                    {
                        { Constants.TaskTypeNameKey, mapTaskType.AssemblyQualifiedName },
                        { Constants.DtoOptionsKey, DtoHelper.Store(dtoOptions) },
                    },
                    OutputFiles = { new Japi.CreateFile(Constants.FileName_Intermediate + pair.Index).DataType<TIntermediate>() }
                });
            }

            // Create "reduce" tasks
            var resultTask = new Japi.Task
            {
                EntryPoint = entryPoint,
                InputFiles = (from t in mapTasks
                              from f in t.OutputFiles
                              select f).ToList(),
                Name = Constants.TaskName_Reduce,
                Parameters = 
                {
                    { Constants.TaskTypeNameKey, reduceTaskType.AssemblyQualifiedName },
                    { Constants.DtoOptionsKey, DtoHelper.Store(dtoOptions) },
                },
                OutputFiles = new List<Japi.File> { new Japi.CreateFile(Constants.FileName_Result).DataType<TOutput>() },
                Dependencies = mapTasks
            };

            job.Tasks.AddRange(mapTasks);
            job.Tasks.Add(resultTask);

            return job;
        }

        private readonly Type mapTaskType;
        private readonly Type reduceTaskType;

        private readonly IDataFlowConnection connection;
        private readonly DataContractOptions dtoOptions;
    }
}
