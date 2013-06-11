using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using AISTek.DataFlow.Util;
using Newtonsoft.Json;

namespace AISTek.DataFlow.Shared.DataExchange
{
    internal class JsonFormatSerializer : IDataContractSerializer
    {
        public Stream Serialize(object value)
        {
            var json = JsonConvert.SerializeObject(value);
            return new MemoryStream(Encoding.Unicode.GetBytes(json));
        }

        public object Deserialize(Stream stream)
        {
            var json = Encoding.Unicode.GetString(stream.ReadToBuffer());
            return JsonConvert.DeserializeObject(json);
        }
    }
}
