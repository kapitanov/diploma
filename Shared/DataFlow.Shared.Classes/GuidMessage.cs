using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace AISTek.DataFlow.Shared.Classes
{
    [MessageContract(WrapperNamespace = Namespaces.Repository.Namespace)]
    [Serializable]
    public sealed class GuidMessage
    {
        [MessageBodyMember]
        public Guid Id { get; set; }

        public static implicit operator GuidMessage(Guid guid)
        {
            return new GuidMessage {Id = guid};
        }

        public static implicit operator Guid(GuidMessage message)
        {
            return message.Id;
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
