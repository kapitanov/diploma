using System.IO;
using System.Text;
using AISTek.DataFlow.Util;
using Newtonsoft.Json;

namespace AISTek.DataFlow.Shared.DataExchange
{
    internal class JsonFormatSerializer<T> : IDataContractSerializer<T>
    {
        public Stream Serialize(T value)
        {
            var json = JsonConvert.SerializeObject(value);
            return new MemoryStream(Encoding.Unicode.GetBytes(json));
        }

        public T Deserialize(Stream stream)
        {
            var json = Encoding.Unicode.GetString(stream.ReadToBuffer());
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
