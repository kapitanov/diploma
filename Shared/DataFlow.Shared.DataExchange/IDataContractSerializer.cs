using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AISTek.DataFlow.Shared.DataExchange
{
    internal interface IDataContractSerializer
    {
        Stream Serialize(object value);
        object Deserialize(Stream stream);
    }
}
