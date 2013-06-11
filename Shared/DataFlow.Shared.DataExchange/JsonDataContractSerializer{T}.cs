using System.IO;
using System.Runtime.Serialization.Json;

namespace AISTek.DataFlow.Shared.DataExchange
{
    internal class JsonDataContractSerializer<T> : IDataContractSerializer<T>
    {
        public JsonDataContractSerializer(DataContractOptions _)
        {
            serializer = new DataContractJsonSerializer(typeof(T));
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

        private readonly DataContractJsonSerializer serializer;
    }
}
