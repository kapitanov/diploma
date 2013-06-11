using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AISTek.DataFlow.ComputeNode.Classes
{
    public class TypeNameParser
    {
        // TestTasks.Startup, TestTasks, Version=0.1.1050.467, Culture=neutral, PublicKeyToken=null
        public TypeName Parse(string typeNameString)
        {
            var name = (from str in typeNameString.Split(',')
                        select str.Trim()).First();

            return new TypeName(name);
        }
    }
}
