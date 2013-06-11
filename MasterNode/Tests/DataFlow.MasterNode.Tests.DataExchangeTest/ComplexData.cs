using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace AISTek.DataFlow.MasterNode.Tests.DataExchangeTest
{
    [Serializable]
    [DataContract]
    [KnownType(typeof(LeafComplexData))]
    [KnownType(typeof(BranchComplexData))]
    [XmlInclude(typeof(LeafComplexData))]
    [XmlInclude(typeof(BranchComplexData))]
    public abstract class ComplexData : IEquatable<ComplexData>
    {
        public static bool operator ==(ComplexData x, ComplexData y)
        {
            if (x is LeafComplexData && y is LeafComplexData)
            {
                return (x as LeafComplexData) == (y as LeafComplexData);
            }

            if (x is BranchComplexData && y is BranchComplexData)
            {
                return (x as BranchComplexData) == (y as BranchComplexData);
            }

            return false;
        }

        public static bool operator !=(ComplexData x, ComplexData y)
        {
            return !(x == y);
        }

        public bool Equals(ComplexData other)
        {
            return this == other;
        }
    }
}
