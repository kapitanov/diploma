using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AISTek.DataFlow.Sdk.Samples
{
    [DataContract]
    [Serializable]
    public class Words : IEnumerable<Word>
    {
        public Words()
        {
            Items = new List<Word>();
        }

        public Words(IEnumerable<Word> words)
        {
            Items = new List<Word>(words);
        }

        [DataMember]
        public List<Word> Items { get; set; }
        public IEnumerator<Word> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
