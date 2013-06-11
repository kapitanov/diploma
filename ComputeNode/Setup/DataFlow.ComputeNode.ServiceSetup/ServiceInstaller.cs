using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AISTek.DataFlow.ComputeNode.ServiceSetup
{
    /// <summary>
    /// Installs and provides functionality for handling windows services
    /// </summary>
    public class ServiceInstaller
    {
        private const int StandardRightsRequired = 0xF0000;
        private const int ServiceWin32OwnProcess = 0x00000010;

        [StructLayout(LayoutKind.Sequential)]
        private class ServiceStatus
        {
            public int dwServiceType = 0;
            public ServiceState dwCurrentState = 0;
            public int dwControlsAccepted = 0;
            public int dwWin32ExitCode = 0;
            public int dwServiceSpecificExitCode = 0;
            public int dwCheckPoint = 0;
            public int dwWaitHint = 0;
        }

        [DllImport("advapi32.dll", EntryPoint = "OpenSCManagerA")]
        private static extern IntPtr OpenScManager(string lpMachineName, string
        lpDatabaseName, ServiceManagerRights dwDesiredAccess);
        [DllImport("advapi32.dll", EntryPoint = "OpenServiceA",
        CharSet = CharSet.Ansi)]
        private static extern IntPtr OpenService(IntPtr hSCManager, string
        lpServiceName, ServiceRights dwDesiredAccess);
        [DllImport("advapi32.dll", EntryPoint = "CreateServiceA")]
        private static extern IntPtr CreateService(IntPtr hSCManager, string
        lpServiceName, string lpDisplayName, ServiceRights dwDesiredAccess, int
        dwServiceType, ServiceBootFlag dwStartType, ServiceError dwErrorControl,
        string lpBinaryPathName, string lpLoadOrderGroup, IntPtr lpdwTagId, string
        lpDependencies, string lp, string lpPassword);
        [DllImport("advapi32.dll")]
        private static extern int CloseServiceHandle(IntPtr hSCObject);
        [DllImport("advapi32.dll")]
        private static extern int QueryServiceStatus(IntPtr hService,
        ServiceStatus lpServiceStatus);
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern int DeleteService(IntPtr hService);
        [DllImport("advapi32.dll")]
        private static extern int ControlService(IntPtr hService, ServiceControl
        dwControl, ServiceStatus lpServiceStatus);
        [DllImport("advapi32.dll", EntryPoint = "StartServiceA")]
        private static extern int StartService(IntPtr hService, int
        dwNumServiceArgs, int lpServiceArgVectors);

        /// <summary>
        /// 
        /// </summary>
        public ServiceInstaller()
        {
        }

        /// <summary>
        /// Takes a service name and tries to stop and then uninstall the windows serviceError
        /// </summary>
        /// <param name="serviceName">The windows service name to uninstall</param>
        public static void Uninstall(string serviceName)
        {
            var scman = OpenScManager(ServiceManagerRights.Connect);
            try
            {
                var service = OpenService(scman, serviceName,
                                          ServiceRights.StandardRightsRequired | ServiceRights.Stop |
                                          ServiceRights.QueryStatus);
                if (service == IntPtr.Zero)
                {
                    throw new ApplicationException("Service not installed.");
                }
                try
                {
                    StopService(service);
                    var ret = DeleteService(service);
                    if (ret == 0)
                    {
                        var error = Marshal.GetLastWin32Error();
                        throw new ApplicationException("Could not delete service " + error);
                    }
                }
                finally
                {
                    CloseServiceHandle(service);
                }
            }
            finally
            {
                CloseServiceHandle(scman);
            }
        }

        /// <summary>
        /// Accepts a service name and returns true if the service with that service name exists
        /// </summary>
        /// <param name="serviceName">The service name that we will check for existence</param>
        /// <returns>True if that service exists false otherwise</returns>
        public static bool ServiceIsInstalled(string serviceName)
        {
            var scman = OpenScManager(ServiceManagerRights.Connect);
            try
            {
                var service = OpenService(scman, serviceName,
                                          ServiceRights.QueryStatus);
                if (service == IntPtr.Zero) return false;
                CloseServiceHandle(service);
                return true;
            }
            finally
            {
                CloseServiceHandle(scman);
            }
        }

        /// <summary>
        /// Takes a service name, a service display name and the path to the service executable and installs / starts the windows service.
        /// </summary>
        /// <param name="serviceName">The service name that this service will have</param>
        /// <param name="displayName">The display name that this service will have</param>
        /// <param name="fileName">The path to the executable of the service</param>
        public static void InstallAndStart(string serviceName, string displayName, string fileName)
        {
            var scman = OpenScManager(ServiceManagerRights.Connect | ServiceManagerRights.CreateService);
            try
            {
                var service = OpenService(scman, serviceName,
                                          ServiceRights.QueryStatus | ServiceRights.Start);
                if (service == IntPtr.Zero)
                {
                    service = CreateService(scman, serviceName, displayName,
                    ServiceRights.QueryStatus | ServiceRights.Start, ServiceWin32OwnProcess,
                    ServiceBootFlag.AutoStart, ServiceError.Normal, fileName, null, IntPtr.Zero,
                    null, null, null);
                }
                if (service == IntPtr.Zero)
                {
                    throw new ApplicationException("Failed to install service.");
                }
                try
                {
                    StartService(service);
                }
                finally
                {
                    CloseServiceHandle(service);
                }
            }
            finally
            {
                CloseServiceHandle(scman);
            }
        }

        /// <summary>
        /// Takes a service name and starts it
        /// </summary>
        /// <param name="name">The service name</param>
        public static void StartService(string name)
        {
            var scman = OpenScManager(ServiceManagerRights.Connect);
            try
            {
                var hService = OpenService(scman, name, ServiceRights.QueryStatus |
                                                        ServiceRights.Start);
                if (hService == IntPtr.Zero)
                {
                    throw new ApplicationException("Could not open service.");
                }
                try
                {
                    StartService(hService);
                }
                finally
                {
                    CloseServiceHandle(hService);
                }
            }
            finally
            {
                CloseServiceHandle(scman);
            }
        }

        /// <summary>
        /// Stops the provided windows service
        /// </summary>
        /// <param name="name">The service name that will be stopped</param>
        public static void StopService(string name)
        {
            var scman = OpenScManager(ServiceManagerRights.Connect);
            try
            {
                var hService = OpenService(scman, name, ServiceRights.QueryStatus |
                                                        ServiceRights.Stop);
                if (hService == IntPtr.Zero)
                {
                    throw new ApplicationException("Could not open service.");
                }
                try
                {
                    StopService(hService);
                }
                finally
                {
                    CloseServiceHandle(hService);
                }
            }
            finally
            {
                CloseServiceHandle(scman);
            }
        }

        /// <summary>
        /// Stars the provided windows service
        /// </summary>
        /// <param name="hService">The handle to the windows service</param>
        private static void StartService(IntPtr hService)
        {
            var status = new ServiceStatus();
            StartService(hService, 0, 0);
            WaitForServiceStatus(hService, ServiceState.Starting, ServiceState.Run);
        }

        /// <summary>
        /// Stops the provided windows service
        /// </summary>
        /// <param name="hService">The handle to the windows service</param>
        private static void StopService(IntPtr hService)
        {
            ServiceStatus status = new ServiceStatus();
            ControlService(hService, ServiceControl.Stop, status);
            WaitForServiceStatus(hService, ServiceState.Stopping, ServiceState.Stop);
        }

        /// <summary>
        /// Takes a service name and returns the <code>ServiceState</code> of the corresponding service
        /// </summary>
        /// <param name="ServiceName">The service name that we will check for his <code>ServiceState</code></param>
        /// <returns>The ServiceState of the service we wanted to check</returns>
        public static ServiceState GetServiceStatus(string ServiceName)
        {
            var scman = OpenScManager(ServiceManagerRights.Connect);
            try
            {
                var hService = OpenService(scman, ServiceName,
                                           ServiceRights.QueryStatus);
                if (hService == IntPtr.Zero)
                {
                    return ServiceState.NotFound;
                }
                try
                {
                    return GetServiceStatus(hService);
                }
                finally
                {
                    CloseServiceHandle(scman);
                }
            }
            finally
            {
                CloseServiceHandle(scman);
            }
        }

        /// <summary>
        /// Gets the service state by using the handle of the provided windows service
        /// </summary>
        /// <param name="hService">The handle to the service</param>
        /// <returns>The <code>ServiceState</code> of the service</returns>
        private static ServiceState GetServiceStatus(IntPtr hService)
        {
            ServiceStatus ssStatus = new ServiceStatus();
            if (QueryServiceStatus(hService, ssStatus) == 0)
            {
                throw new ApplicationException("Failed to query service status.");
            }
            return ssStatus.dwCurrentState;
        }

        /// <summary>
        /// Returns true when the service status has been changes from wait status to desired status
        /// ,this method waits around 10 seconds for this operation.
        /// </summary>
        /// <param name="hService">The handle to the service</param>
        /// <param name="waitStatus">The current state of the service</param>
        /// <param name="desiredStatus">The desired state of the service</param>
        /// <returns>bool if the service has successfully changed states within the allowed timeline</returns>
        private static bool WaitForServiceStatus(IntPtr hService, ServiceState waitStatus, ServiceState desiredStatus)
        {
            var ssStatus = new ServiceStatus();

            QueryServiceStatus(hService, ssStatus);
            if (ssStatus.dwCurrentState == desiredStatus)
                return true;
            var dwStartTickCount = Environment.TickCount;
            var dwOldCheckPoint = ssStatus.dwCheckPoint;

            while (ssStatus.dwCurrentState == waitStatus)
            {
                // Do not wait longer than the wait hint. A good interval is
                // one tenth the wait hint, but no less than 1 second and no
                // more than 10 seconds.

                var dwWaitTime = ssStatus.dwWaitHint / 10;

                if (dwWaitTime < 1000) dwWaitTime = 1000;
                else if (dwWaitTime > 10000) dwWaitTime = 10000;

                System.Threading.Thread.Sleep(dwWaitTime);

                // Check the status again.

                if (QueryServiceStatus(hService, ssStatus) == 0) break;

                if (ssStatus.dwCheckPoint > dwOldCheckPoint)
                {
                    // The service is making progress.
                    dwStartTickCount = Environment.TickCount;
                    dwOldCheckPoint = ssStatus.dwCheckPoint;
                }
                else
                {
                    if (Environment.TickCount - dwStartTickCount > ssStatus.dwWaitHint)
                    {
                        // No progress made within the wait hint
                        break;
                    }
                }
            }
            return (ssStatus.dwCurrentState == desiredStatus);
        }

        /// <summary>
        /// Opens the service manager
        /// </summary>
        /// <param name="rights">The service manager rights</param>
        /// <returns>the handle to the service manager</returns>
        private static IntPtr OpenScManager(ServiceManagerRights rights)
        {
            var scman = OpenScManager(null, null, rights);
            if (scman == IntPtr.Zero)
            {
                throw new ApplicationException("Could not connect to service control manager.");
            }
            return scman;
        }
    }

}
