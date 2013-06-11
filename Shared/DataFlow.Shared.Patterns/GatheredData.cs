using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AISTek.DataFlow.Shared.Patterns
{
    internal static class GatheredData
    {
        public static object Gather(Type type, IEnumerable<object> data)
        {
            var typedGenericGatheredData = genericGatheredData.MakeGenericType(new[] { type });
            return typedGenericGatheredData.GetConstructors()[0]
                .Invoke(new[] { data });
        }
        
        private class GatheredDataContainer<T> : List<T>, IGather<T>
        {
            public GatheredDataContainer(IEnumerable<object> data)
                : base(data.Cast<T>())
            { }
        }

        private static Type genericGatheredData = typeof(GatheredDataContainer<>);
    }
}
