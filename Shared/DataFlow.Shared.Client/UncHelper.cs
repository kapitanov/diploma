using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using AISTek.DataFlow.Util;
using AISTek.DataFlow.PerfomanceToolkit;

namespace AISTek.DataFlow.Shared.Client
{
    internal static class UncHelper
    {
        public static void WriteFile(this Uri uri, Stream dataStream)
        {
            if (uri == null)
                throw new ArgumentNullException("uri");
            if (!uri.IsUnc)
                throw new ArgumentException("Specified URI is not a valid UNC URI.");
            if (dataStream == null)
                throw new ArgumentNullException("dataStream");

            using (Perfomance.Trace("UncHelper::WriteFile(\"{0}\")", uri).BindToTrace())
            using (var fileStream = File.Open(uri.UncPath(), FileMode.Create))
            {
                dataStream.CopyTo(fileStream);
            }
        }

        public static Stream ReadFile(this Uri uri, bool sharedAccess)
        {
            if (uri == null)
                throw new ArgumentNullException("uri");
            if (!uri.IsUnc)
                throw new ArgumentException("Specified URI is not a valid UNC URI.");

            using (Perfomance.Trace("UncHelper::ReadFile(\"{0}\")", uri).BindToTrace())
            using (var fileStream = File.Open(uri.UncPath(), FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return fileStream.ReadToMemoryStream();
            }
        }

        public static void DeleteFile(this Uri uri)
        {
            if (uri == null)
                throw new ArgumentNullException("uri");
            if (!uri.IsUnc)
                throw new ArgumentException("Specified URI is not a valid UNC URI.");

            var path = uri.UncPath();
            if (File.Exists(path))
                File.Delete(path);
        }

        private static string UncPath(this Uri uri)
        {
            if (uri == null)
                throw new ArgumentNullException("uri");
            if (!uri.IsUnc)
                throw new ArgumentException("Specified URI is not a valid UNC URI.");

            return string.Format(@"\\{0}{1}", uri.Host, uri.PathAndQuery.Replace('/', '\\'));
        }
    }
}
