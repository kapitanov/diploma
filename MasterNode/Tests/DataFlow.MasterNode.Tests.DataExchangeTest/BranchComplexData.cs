using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AISTek.DataFlow.MasterNode.Tests.DataExchangeTest
{
    [Serializable]
    [DataContract]
    public sealed class BranchComplexData : ComplexData
    {
        public BranchComplexData()
        { }

        public BranchComplexData(ComplexData left, ComplexData right)
        {
            Left = left;
            Right = right;
        }

        [DataMember]
        public ComplexData Left { get; set; }

        [DataMember]
        public ComplexData Right{ get; set; }

        public static bool operator ==(BranchComplexData x, BranchComplexData y)
        {
            return x.Left == y.Left && x.Right == y.Right;
        }

        public static bool operator !=(BranchComplexData x, BranchComplexData y)
        {
            return !(x == y);
        }
    }
}
