using System.Runtime.Serialization;

namespace AISTek.DataFlow.Shared.FileLinks
{
    [DataContract]
    public sealed class DataLink
    {
        [DataMember(IsRequired = true)]
        public string UtcPath { get; set; }
    }
}
