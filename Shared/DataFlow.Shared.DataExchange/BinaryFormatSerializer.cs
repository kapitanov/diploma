using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace AISTek.DataFlow.Shared.DataExchange
{
    internal class BinaryFormatSerializer : IDataContractSerializer
    {
        public BinaryFormatSerializer(DataContractOptions _)
        {
            serializer = new BinaryFormatter();
        }

        public Stream Serialize(object value)
        {
            var stream = new MemoryStream();
            serializer.Serialize(stream, value);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        public object Deserialize(Stream stream)
        {
            using (stream)
            {
                return serializer.Deserialize(stream);
            }
        }

        private readonly BinaryFormatter serializer;
    }
}
