using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AISTek.DataFlow.MasterNode.Tests.DataExchangeTest
{
    [Serializable]
    [DataContract]
    public sealed class LeafComplexData : ComplexData
    {
        public LeafComplexData()
        {
            Data = new List<string>();
        }

        public LeafComplexData(IEnumerable<string> data)
        {
            Data = new List<string>(data);
        }

        [DataMember]
        public List<string> Data { get; set; }

        public static bool operator ==(LeafComplexData x, LeafComplexData y)
        {
            if (x.Data.Count != y.Data.Count)
                return false;

            for (var i = 0; i < x.Data.Count; i++)
            {
                if (x.Data[i] != y.Data[i])
                    return false;
            }

            return true;
        }

        public static bool operator !=(LeafComplexData x, LeafComplexData y)
        {
            return !(x == y);
        }
    }
}
