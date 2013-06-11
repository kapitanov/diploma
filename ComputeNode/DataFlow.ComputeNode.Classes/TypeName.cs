using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AISTek.DataFlow.ComputeNode.Classes
{
    /// <summary>
    /// Contains type name data.
    /// </summary>
    public class TypeName
    {
        /// <summary>
        /// Initializes new instance of <see cref="TypeName"/> class.
        /// </summary>
        /// <param name="name">
        /// Type name.
        /// </param> 
        public TypeName(
            string name)
            : this(name, null, null, null, null)
        { }

         /// <summary>
         /// Initializes new instance of <see cref="TypeName"/> class.
         /// </summary>
         /// <param name="name">
         /// Type name.
         /// </param>
         /// <param name="assemblyName">
         /// Assembly name.
         /// </param>
        public TypeName(
            string name, 
            string assemblyName)
            : this(name, assemblyName, null, null, null)
        { }

        /// <summary>
        /// Initializes new instance of <see cref="TypeName"/> class.
        /// </summary>
        /// <param name="name">
        /// Type name.
        /// </param>
        /// <param name="assemblyName">
        /// Assembly name.
        /// </param>
        /// <param name="assemblyVersion">
        /// Assembly version.
        /// </param>
        /// <param name="assemblyCulture">
        /// Assembly culture.
        /// </param>
        /// <param name="publicKeyToken">
        /// Assembly public key token.
        /// </param>
        public TypeName(
            string name, 
            string assemblyName, 
            Version assemblyVersion, 
            CultureInfo assemblyCulture, 
            byte[] publicKeyToken)
        {
            Contract.Requires(!string.IsNullOrEmpty(name));

            Name = name;
            AssemblyName = assemblyName;
            AssemblyVersion = assemblyVersion;
            AssemblyCulture = assemblyCulture;
            PublicKeyToken = publicKeyToken;
        }

        /// <summary>
        /// Gets type name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets assembly name if defined.
        /// </summary>
        public string AssemblyName { get; private set; }

        /// <summary>
        /// Gets a value indicating whether assembly name is defined.
        /// </summary>
        public bool HasAssemblyName { get { return AssemblyName != null; } }

        /// <summary>
        /// Gets assembly version if defined.
        /// </summary>
        
        public Version AssemblyVersion { get; private set; }

        /// <summary>
        /// Gets a value indicating whether assembly version is defined.
        /// </summary>
        public bool HasAssemblyVersion { get { return AssemblyVersion != null; } }

        /// <summary>
        /// Gets assembly culture if defined.
        /// </summary>
        public CultureInfo AssemblyCulture { get; private set; }

        /// <summary>
        /// Gets a value indicating whether assembly culture is defined.
        /// </summary>
        public bool HasAssemblyCulture { get { return AssemblyCulture != null; } }

        /// <summary>
        /// Gets assembly public key token if defined.
        /// </summary>
        
        public byte[] PublicKeyToken { get; private set; }

        /// <summary>
        /// Gets a value indicating whether assembly public key token is defined.
        /// </summary>
        public bool HasPublicKeyToken { get { return PublicKeyToken != null; } }
    }
}
