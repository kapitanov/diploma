using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AISTek.DataFlow.Shared.Patterns
{
    public interface IMapTask<TInput, TIntermediate> : IGenericTask<TInput, TIntermediate>
    { }
}
