using System.Text;

namespace AISTek.DataFlow.Shared.DataExchange
{
    public sealed class DataContractOptions
    {
        public DataContractOptions(DataContractSerialization serialization, Encoding encoding = null)
        {
            Serialization = serialization;
            Encoding = encoding ?? Encoding.Unicode;
        }
        
        public static DataContractOptions Defaut = new DataContractOptions(DataContractSerialization.XmlDataContract, Encoding.UTF8);

        public DataContractSerialization Serialization { get; set; }

        public Encoding Encoding { get; set; }
    }
}
