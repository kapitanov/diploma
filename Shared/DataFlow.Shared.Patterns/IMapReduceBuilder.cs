using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AISTek.DataFlow.Shared.Patterns
{
    public interface IMapReduceBuilder<TInput, TIntermediate, TOutput>
    {
        IMapReduceBuilder<TInput, TIntermediate, TOutput> Map<TMapTask>()
            where TMapTask : IMapTask<TInput, TIntermediate>;

        IMapReduceBuilder<TInput, TIntermediate, TOutput> Reduce<TReduceTask>()
            where TReduceTask : IReduceTask<TIntermediate, TOutput>;

        IMapReduce<TInput, TIntermediate, TOutput> Create();
    }
}
