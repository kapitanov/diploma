using System;
using System.IO;

namespace AISTek.DataFlow.ComputeNode.ServiceSetup
{
    public class Service
    {
        public static int Main(string[] args)
        {
            if (args.Length > 0)
            {
                switch (args[0].ToLowerInvariant())
                {
                    case Commands.Install:
                        return Install(Directory.GetCurrentDirectory());

                    case Commands.Uninstall:
                        return Unistall();
                }

                Console.WriteLine("Command \"{0}\" is not recognized.", args[1]);
                return 2;
            }

            Console.WriteLine("No command is specified. Available commands are:\n, - install\n - uninstall");
            return 3;
        }

        internal static int Install(string path)
        {
            try
            {
                ServiceInstaller.InstallAndStart(
                     ServiceName,
                     DisplayServiceName,
                     Path.Combine(path, ServicePath));
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("Service install failed.\n{0}", e);
                return 1;
            }
        }

        internal static int Unistall()
        {
            try
            {
                ServiceInstaller.Uninstall(ServiceName);
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("Service uninstall failed.\n{0}", e);
                return 1;
            }
        }

        private const string ServiceName = "DFCompNode";
        private const string DisplayServiceName = "AISTek DataFlow Compute node";
        private const string ServicePath = @"DataFlow.computeNode.Service.exe";
    }
}


