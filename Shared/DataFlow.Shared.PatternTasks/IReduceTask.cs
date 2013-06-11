using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AISTek.DataFlow.Shared.Patterns
{
    public interface IReduceTask<TIntermediate, TOutput> : IGenericTask<IGather<TIntermediate>, TOutput>
    { }
}
