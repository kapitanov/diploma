using System.IO;
using System.Runtime.Serialization;

namespace AISTek.DataFlow.Shared.DataExchange
{
    internal class XmlDataContractSerializer<T> : IDataContractSerializer<T>
    {
        public XmlDataContractSerializer(DataContractOptions _)
        {
            serializer = new DataContractSerializer(typeof(T));
        }

        public Stream Serialize(T value)
        {
            var stream = new MemoryStream();
            serializer.WriteObject(stream, value);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        public T Deserialize(Stream stream)
        {
            using(stream)
            {
                return (T)serializer.ReadObject(stream);
            }
        }

        private readonly DataContractSerializer serializer;
    }
}
