using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AISTek.DataFlow.Shared.Classes;
using AISTek.DataFlow.Util;
using System.Diagnostics;
using System.Reflection;


namespace AISTek.DataFlow.ComputeNode.Engine
{
    internal class AssemblyLoader 
    {
        public AssemblyLoader(IRepository repository, Task task)
        {
            if (repository == null)
                throw new ArgumentNullException("repository");
            if (task == null)
                throw new ArgumentNullException("task");

            this.repository = repository;
            this.task = task;

            LoadCodebase();
        }

        public AssemblyLoader BindTo(AppDomain domain)
        {
            boundDomain = domain;

            domain.AssemblyLoad += (_, e) =>
            {
                Debug.Print("AssemblyLoader: loaded assembly {0}", e.LoadedAssembly.FullName);
            };

            domain.AssemblyResolve += (_, e) => LoadAssembly(e.Name);

            return this;
        }

        private void LoadCodebase()
        {
            assemblies = (from id in task.EntryPoint.DependentAssemblyIds
                          let rawAssembly = repository.Download(id).ReadToBuffer()
                          select Assembly.Load(rawAssembly)).ToList();
        }

        private Assembly LoadAssembly(string assemblyName)
        {
            Console.WriteLine("AssemblyLoader::LoadAssembly({0})", assemblyName);

            var foundAssembly = assemblies.FirstOrDefault(assembly => assembly.FullName.StartsWith(assemblyName));
            if (foundAssembly != null)
            {
                Debug.Print("AssemblyLoader: assembly {0} is resolved as {1}", assemblyName, foundAssembly.FullName);
                return foundAssembly;
            }

            Debug.Print("AssemblyLoader: assembly {0} is not resolved", assemblyName);
            return null;
        }

        private readonly IRepository repository;
        private readonly Task task;
        private AppDomain boundDomain;
        private IList<Assembly> assemblies;
    }
}