using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AISTek.DataFlow.Shared.DataExchange;
using AISTek.DataFlow.Shared.Japi;

namespace AISTek.DataFlow.Shared.Patterns
{
    internal static class DtoHelper
    {
        public static string Store(DataContractOptions dtoOptions)
        {
            return string.Format("{0}:{1}", dtoOptions.Serialization, dtoOptions.Encoding.EncodingName);
        }

        public static DataContractOptions Read(string s)
        {
            var parts = s.Split(separators);
            if (parts.Length < 2)
                throw new ArgumentException("This is not a valid string representation of DataContractOptions");

            return new DataContractOptions(
                (DataContractSerialization)Enum.Parse(typeof(DataContractSerialization), parts[0]),
                Encoding.GetEncoding(parts[1]));
        }

        private static char[] separators = new[] { ':' };
    }
}
