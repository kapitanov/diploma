using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("DataFlow.MasterNode.Core")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("AISTek")]
[assembly: AssemblyProduct("AISTek DataFlow")]
[assembly: AssemblyCopyright("Copyright © AISTek 2009")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("3260bee8-88ec-4a53-bca8-f8b1c30d27da")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("0.1.1117.645")]
[assembly: AssemblyFileVersion("0.1.1117.645")]
[assembly: InternalsVisibleTo("DataFlow.MasterNode.Core.Tests")]

[assembly: EventLogPermission(SecurityAction.RequestMinimum, PermissionAccess = EventLogPermissionAccess.Administer)]
