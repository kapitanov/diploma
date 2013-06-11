using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AISTek.DataFlow.Shared.DataExchange
{
    internal class BinaryFormatSerializer<T> : IDataContractSerializer<T>
    {
        public BinaryFormatSerializer(DataContractOptions _)
        {
            serializer = new BinaryFormatter();
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

        private readonly BinaryFormatter serializer;
    }
}
