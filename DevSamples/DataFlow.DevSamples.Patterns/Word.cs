using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AISTek.DataFlow.DevSamples.Patterns
{
    [DataContract]
    [Serializable]
    public class Word
    {
        public Word(string value, double count)
        {
            Value = value;
            Count = count;
        }

        public Word()
            : this(string.Empty, 0)
        { }

        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public double Count { get; set; }
    }
}
