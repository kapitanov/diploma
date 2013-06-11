using System;
namespace AISTek.DataFlow.Shared.DataExchange
{
    internal class DataContractSerializerFactory
    {
        public IDataContractSerializer<T> CreateSerializer<T>(DataContractOptions options)
        {
            switch (options.Serialization)
            {
                case DataContractSerialization.JsonFormat:
                    return new JsonFormatSerializer<T>();

                case DataContractSerialization.XmlFormat:
                    return new XmlFormatSerializer<T>(options);

                case DataContractSerialization.BinaryFormat:
                    return new BinaryFormatSerializer<T>(options);

                case DataContractSerialization.JsonDataContract:
                    return new JsonDataContractSerializer<T>(options);

                default:
                    return new XmlDataContractSerializer<T>(options);
            }
        }

        public IDataContractSerializer CreateSerializer(Type type, DataContractOptions options)
        {
            switch (options.Serialization)
            {
                case DataContractSerialization.JsonFormat:
                    return new JsonFormatSerializer();

                case DataContractSerialization.XmlFormat:
                    return new XmlFormatSerializer(type, options);

                case DataContractSerialization.BinaryFormat:
                    return new BinaryFormatSerializer(options);

                case DataContractSerialization.JsonDataContract:
                    return new JsonDataContractSerializer(type, options);

                default:
                    return new XmlDataContractSerializer(type, options);
            }
        }
    }
}
