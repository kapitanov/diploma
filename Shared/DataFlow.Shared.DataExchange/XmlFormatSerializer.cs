using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace AISTek.DataFlow.Shared.DataExchange
{
    internal class XmlFormatSerializer : IDataContractSerializer
    {
        public XmlFormatSerializer(Type type, DataContractOptions _)
        {
            serializer = new XmlSerializer(type);
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

        private readonly XmlSerializer serializer;
    }
}
