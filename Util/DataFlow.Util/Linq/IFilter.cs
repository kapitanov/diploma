using System;
using System.Linq;

namespace AISTek.DataFlow.Util.Linq
{
    /// <summary>
    /// Provides functionality to evaluate queries against a specific data source wherein the type of the data is known
    /// and properly dispose the resources used by data source after evaluation ofthe query.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the data in the data source.
    /// </typeparam>
    public interface IFilter<T> : IQueryable<T>, IDisposable
    { }
}


