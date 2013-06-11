using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AISTek.DataFlow.Shared.Patterns
{
    internal static class FileHelper
    {
        public static Japi.File DataType<T>(this Japi.File file)
        {
            file.Add(Constants.DataTypeKey, typeof(T).AssemblyQualifiedName);
            return file;
        }

        public static Type GetDataType(this Classes.FileLink file)
        {
            var typeName = file.Metadata[Constants.DataTypeKey];

            return Type.GetType(typeName, true);
        }
    }
}
