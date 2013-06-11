using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AISTek.DataFlow.Shared.Patterns
{
    public interface IMapReduceBuilderSetMapTask<TInput, TIntermediate, TOutput>
    {
        IMapReduceBuilderSetReduceTask<TInput, TIntermediate, TOutput> Map<TMapTask>()
            where TMapTask : IMapTask<TInput, TIntermediate>, new();
    }
}
