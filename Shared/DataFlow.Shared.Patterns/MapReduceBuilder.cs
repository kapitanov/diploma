using System;
using AISTek.DataFlow.Shared.Client;
using AISTek.DataFlow.Shared.DataExchange;

namespace AISTek.DataFlow.Shared.Patterns
{
    internal sealed class MapReduceBuilder<TInput, TIntermediate, TOutput>
        : IMapReduceBuilderSetMapTask<TInput, TIntermediate, TOutput>,
            IMapReduceBuilderSetReduceTask<TInput, TIntermediate, TOutput>,
            IMapReduceBuilderCreate<TInput, TIntermediate, TOutput>
    {
        public MapReduceBuilder(
            IDataFlowConnection connection,
            DataContractOptions dtoOptions)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            if (dtoOptions == null)
                throw new ArgumentNullException("dtoOptions");

            this.connection = connection;
            this.dtoOptions = dtoOptions;
        }

        public IMapReduceBuilderSetReduceTask<TInput, TIntermediate, TOutput> Map<TMapTask>()
            where TMapTask : IMapTask<TInput, TIntermediate>, new()
        {
            mapTaskType = typeof(TMapTask);
            return this;
        }

        public IMapReduceBuilderCreate<TInput, TIntermediate, TOutput> Reduce<TReduceTask>()
            where TReduceTask : IReduceTask<TIntermediate, TOutput>, new()
        {
            reduceTaskType = typeof(TReduceTask);
            return this;
        }

        public IMapReduce<TInput, TIntermediate, TOutput> Create()
        {
            if (mapTaskType == null)
                throw new InvalidOperationException("\"MAP\" task is not set");
            if (reduceTaskType == null)
                throw new InvalidOperationException("\"REDUCE\" task is not set");

            return new MapReducePattern<TInput, TIntermediate, TOutput>(connection, dtoOptions, mapTaskType, reduceTaskType);
        }

        private Type mapTaskType;
        private Type reduceTaskType;

        private readonly IDataFlowConnection connection;
        private readonly DataContractOptions dtoOptions;
    }
}