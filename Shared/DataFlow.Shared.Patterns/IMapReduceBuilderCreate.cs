using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AISTek.DataFlow.Shared.Patterns
{
    public interface IMapReduceBuilderCreate<TInput, TIntermediate, TOutput>
    {
        IMapReduce<TInput, TIntermediate, TOutput> Create();
    }
}
