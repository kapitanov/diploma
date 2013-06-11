using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AISTek.DataFlow.Shared.Japi
{
    public class UploadAssembly : UploadFileStream
    {
        public UploadAssembly(Assembly assembly)
            : base(assembly.GetName().Name + ".dll", assembly.GetFiles().First())
        { }
    }
}
