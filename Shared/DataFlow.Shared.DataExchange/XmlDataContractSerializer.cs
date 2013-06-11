using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace AISTek.DataFlow.Shared.DataExchange
{
    internal class XmlDataContractSerializer : IDataContractSerializer
    {
        public XmlDataContractSerializer(Type type, DataContractOptions _)
        {
            serializer = new DataContractSerializer(type);
        }

        public Stream Serialize(object value)
        {
            var stream = new MemoryStream();
            serializer.WriteObject(stream, value);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        public object Deserialize(Stream stream)
        {
            using (stream)
            {
                return serializer.ReadObject(stream);
            }
        }

        private readonly DataContractSerializer serializer;
    }
}
