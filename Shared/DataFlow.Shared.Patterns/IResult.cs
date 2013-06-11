using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AISTek.DataFlow.Shared.Patterns
{
    public interface IResult<T>
    {
        T Value { get; }

        TimeSpan Timing { get; }
    }
}
