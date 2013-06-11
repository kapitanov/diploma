using System.IO;

namespace AISTek.DataFlow.Shared.DataExchange
{
    internal interface IDataContractSerializer<T>
    {
        Stream Serialize(T value);
        T Deserialize(Stream stream);
    }
}
