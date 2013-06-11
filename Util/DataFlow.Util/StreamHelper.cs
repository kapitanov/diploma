using System;
using System.Diagnostics.Contracts;
using System.IO;
using AISTek.DataFlow.PerfomanceToolkit;

namespace AISTek.DataFlow.Util
{
    /// <summary>
    /// Helper class for <see cref="Stream"/> class.
    /// </summary>
    public static class StreamHelper
    {
        /// <summary>
        /// Reads all <see cref="Stream"/> content into new <see cref="byte"/> buffer.
        /// <see cref="Stream"/> implementation may be not seekable and have unknown length.
        /// </summary>
        /// <param name="stream">
        /// An instance of <see cref="Stream"/> to read.
        /// </param>
        /// <param name="chunkSize">
        /// Transfer chunk size, in bytes.
        /// </param>
        /// <returns>
        /// An <see cref="System.Array"/> of <see cref="Stream"/> that contains all <paramref name="stream"/>'s content.
        /// </returns>
        public static byte[] ReadToBuffer(this Stream stream, int chunkSize = 4096)
        {
            Contract.Requires(stream != null);
            Contract.Ensures(Contract.Result<byte[]>() != null);

            using (Perfomance.Trace("StreamHelper::ReadToBuffer()").BindToTrace())
            {
                using (var memoryStream = ReadToMemoryStream(stream, chunkSize))
                {
                    using (Perfomance.Trace("StreamHelper::ReadToBuffer(): MemoryStream.ToArray()").BindToTrace())
                    {
                        return memoryStream.ToArray();
                    }
                }
            }
        }

        /// <summary>
        /// Reads all <see cref="Stream"/> content into new <see cref="byte"/> buffer.
        /// <see cref="Stream"/> implementation may be not seekable and have unknown length.
        /// </summary>
        /// <param name="stream">
        /// An instance of <see cref="Stream"/> to read.
        /// </param>
        /// <param name="chunkSize">
        /// Transfer chunk size, in bytes.
        /// </param>
        /// <returns>
        /// An <see cref="System.Array"/> of <see cref="Stream"/> that contains all <paramref name="stream"/>'s content.
        /// </returns>
        public static MemoryStream ReadToMemoryStream(this Stream stream, int chunkSize = 4096)
        {
            Contract.Requires(stream != null);
            Contract.Ensures(Contract.Result<MemoryStream>() != null);

            using (Perfomance.Trace("StreamHelper::ReadToMemoryStream()").BindToTrace())
            {
                if (stream is MemoryStream)
                    return stream as MemoryStream;

                using (stream)
                {
                    var memoryStream = new MemoryStream();
                    var buffer = new byte[chunkSize];
                    int count;
                    while ((count = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        memoryStream.Write(buffer, 0, count);
                    }
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    return memoryStream;
                }
            }
        }

        /// <summary>
        /// Copies the <paramref name="source"/> stream content into <paramref name="destination"/> stream
        /// in chunks of size <paramref name="chunkSize"/> bytes.
        /// </summary>
        /// <param name="source">
        /// Source stream. This stream must be readable.
        /// </param>
        /// <param name="destination">
        /// Destination stream. This stream must be writable.
        /// </param>
        /// <param name="chunkSize">
        /// Size of transfer buffer in bytes.
        /// </param>
        public static void CopyTo(this Stream source, Stream destination, int chunkSize = 4*1024)
        {
            Contract.Requires<ArgumentNullException>(source != null);
            Contract.Requires<ArgumentNullException>(destination != null);
            Contract.Requires<ArgumentException>(chunkSize > 0);
            Contract.Requires<ArgumentException>(source.CanRead, "The source stream must be readable.");
            Contract.Requires<ArgumentException>(destination.CanWrite, "The destination stream must be writable.");

            using (Perfomance.Trace("StreamHelper::CopyTo(chunkSize: {0})", chunkSize).BindToTrace())
            using (source)
            {
                var buffer = new byte[chunkSize];
                int count;

                do
                {
                    count = source.Read(buffer, 0, chunkSize);
                    destination.Write(buffer, 0, count);
                } while (count > 0);
            }
        }
    }
}
