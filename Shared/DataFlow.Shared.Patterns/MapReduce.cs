using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AISTek.DataFlow.Shared.Classes;
using AISTek.DataFlow.Shared.Client;
using AISTek.DataFlow.Shared.DataExchange;

namespace AISTek.DataFlow.Shared.Patterns
{
    public static class MapReduce
    {
        public static IMapReduceBuilderSetMapTask<TInput, TIntermediate, TOutput> Create<TInput, TIntermediate, TOutput>(
           IDataFlowConnection connection,
            DataContractOptions dtoOptions = null)
        {
            return new MapReduceBuilder<TInput, TIntermediate, TOutput>(connection, dtoOptions ?? DataContractOptions.Defaut);
        }
    }
}
