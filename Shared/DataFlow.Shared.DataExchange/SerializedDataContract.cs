using System;
using System.IO;

namespace AISTek.DataFlow.Shared.DataExchange
{
    public sealed class SerializedDataContract : IDisposable
    {
        internal SerializedDataContract(Stream stream)
        {
            Stream = stream;
        }

        internal Stream Stream { get; private set; }

        public void Dispose()
        {
            if(Stream != null)
                Stream.Dispose();
        }
    }
}
