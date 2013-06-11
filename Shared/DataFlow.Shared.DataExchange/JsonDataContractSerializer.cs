using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;

namespace AISTek.DataFlow.Shared.DataExchange
{
    internal class JsonDataContractSerializer : IDataContractSerializer
    {
        public JsonDataContractSerializer(Type type, DataContractOptions _)
        {
            serializer = new DataContractJsonSerializer(type);
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

        private readonly DataContractJsonSerializer serializer;
    }
}
