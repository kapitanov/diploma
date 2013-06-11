using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using AISTek.DataFlow.Util;

namespace AISTek.DataFlow.MasterNode.Tests.DataExchangeTest
{
    [Serializable]
    [DataContract]
    public sealed class PlainData : IEquatable<PlainData>
    {
        // Methods
        public PlainData()
            : this(Sequence.Empty<string>())
        { }

        public PlainData(IEnumerable<string> items)
        {
            this.Items = new List<string>(items);
        }

        public static bool operator ==(PlainData x, PlainData y)
        {
            if (x.Items.Count != y.Items.Count)
            {
                Console.WriteLine("Count inconsistent");
                return false;
            }

            for (int i = 0; i < x.Items.Count; i++)
            {
                if (x.Items[i] != y.Items[i])
                {
                    Console.WriteLine("Data[{0}] inconsistent", i);
                    return false;
                }
            }
            return true;
        }

        public static bool operator !=(PlainData x, PlainData y)
        {
            return !(x == y);
        }

        // Properties
        [DataMember]
        public string A { get; set; }

        [DataMember]
        public string B { get; set; }

        [DataMember]
        public List<string> Items { get; set; }

        #region IEquatable<PlainData> Members

        public bool Equals(PlainData other)
        {
            return this == other;
        }

        #endregion
    }


}
