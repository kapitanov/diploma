using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AISTek.DataFlow.Shared.Patterns
{
    public interface IMapReduceBuilderSetReduceTask<TInput, TIntermediate, TOutput>
    {
        IMapReduceBuilderCreate<TInput, TIntermediate, TOutput> Reduce<TReduceTask>()
            where TReduceTask : IReduceTask<TIntermediate, TOutput>, new();
    }
}
