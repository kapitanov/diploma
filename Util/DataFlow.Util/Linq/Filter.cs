using System;
using System.Data.Objects;
using System.Diagnostics.Contracts;
using System.Linq;

namespace AISTek.DataFlow.Util.Linq
{
    public static class Filter
    {
        public static IFilter<T> Create<T>(IQueryable<T> query, IDisposable context)
        {
            Contract.Requires(query != null);
            return new Filter<T>(query, context);
        }

        public static IFilter<T> Create<T>(ObjectQuery<T> query)
        {
            Contract.Requires(query != null);
            return new Filter<T>(query, query.Context);
        }
    }
}
