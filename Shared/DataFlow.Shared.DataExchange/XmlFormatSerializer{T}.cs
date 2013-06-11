using System.IO;
using System.Xml.Serialization;

namespace AISTek.DataFlow.Shared.DataExchange
{
    internal class XmlFormatSerializer<T> : IDataContractSerializer<T>
    {
        public XmlFormatSerializer(DataContractOptions _)
        {
            serializer = new XmlSerializer(typeof(T));
        }

        public Stream Serialize(T value)
        {
            var stream = new MemoryStream();
            serializer.Serialize(stream, value);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        public T Deserialize(Stream stream)
        {
            using (stream)
            {
                return (T)serializer.Deserialize(stream);
            }
        }

        private readonly XmlSerializer serializer;
    }
}
