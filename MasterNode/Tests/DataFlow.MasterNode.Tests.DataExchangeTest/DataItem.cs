using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AISTek.DataFlow.MasterNode.Tests.DataExchangeTest
{
    [Serializable]
    public sealed class DataItem : List<KeyValuePair<int, string>>
    {
        public DataItem()
            : base()
        { }

        public DataItem(IEnumerable<KeyValuePair<int, string>> items)
            : base(items)
        { }

        public static bool operator ==(DataItem x, DataItem y)
        {
            if (x.Count != y.Count)
                return false;

            for (var i = 0; i < x.Count; i++)
            {
                if ((x[i].Key != y[i].Key) ||
                    (x[i].Value != y[i].Value))
                    return false;
            }

            return true;
        }

        public static bool operator !=(DataItem x, DataItem y)
        {
            return !(x == y);
        }
    }
}
