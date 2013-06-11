using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AISTek.DataFlow.Shared.Patterns
{
    public interface IMapReduce<TInput, TIntermediate, TOutput>
    {
        IResult<TOutput> Execute(IEnumerable<TInput> input);
    }
}
