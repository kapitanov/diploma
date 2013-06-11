using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;

namespace AISTek.DataFlow.Shared.Patterns
{
    internal class AssemblyReferences
    {
        public AssemblyReferences(Assembly rootAssembly)
        {
            if (rootAssembly == null)
                throw new ArgumentNullException("rootAssembly");

            this.rootAssembly = rootAssembly;
            loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToDictionary(a => a.GetName().FullName);
        }

        public IEnumerable<Japi.File> Build()
        {
            return Build(rootAssembly);
        }

        private IEnumerable<Japi.File> Build(Assembly assembly)
        {
            yield return new Japi.UploadAssembly(assembly);

            foreach (var referencedAssemblyName in assembly.GetReferencedAssemblies())
            {
                if (loadedAssemblies.ContainsKey(referencedAssemblyName.FullName))
                {
                    var referencedAssembly = loadedAssemblies[referencedAssemblyName.FullName];
                    if (!referencedAssembly.GlobalAssemblyCache)
                    {
                        foreach (var innerReferencedAssembly in Build(referencedAssembly))
                        {
                            yield return innerReferencedAssembly;
                        }
                    }
                }
                else
                {
                    Debug.Print("Unable to build reference to \"{0}\"", referencedAssemblyName);
                }
            }
        }

        private readonly Dictionary<string, Assembly> loadedAssemblies;
        private readonly Assembly rootAssembly;
    }
}
