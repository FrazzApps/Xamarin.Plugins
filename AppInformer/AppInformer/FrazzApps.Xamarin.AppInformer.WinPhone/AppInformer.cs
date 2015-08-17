using FrazzApps.Xamarin.AppInformer.Abstractions;
using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.AppInformer.WinPhone;
using Windows.ApplicationModel.Store;
using Microsoft.Phone.Info;
using System.Collections.Generic;
using System.Reflection;
using Windows.Networking.Connectivity;

[assembly: Dependency(typeof(AppInformer))]
namespace FrazzApps.Xamarin.AppInformer.WinPhone
{
    /// <summary>
    /// AppInformer Implementation
    /// </summary>
    public class AppInformer : IAppInformer
    {
        private static Assembly AppAssembly = null;

        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init(object parent) { AppAssembly = parent.GetType().Assembly; }

        /// <summary>
        /// Get the name of the App - the assembly title information 
        /// </summary>
        public string AppName
        {
            get
            {
                string appName = "";
                var executingAssembly = (AppAssembly!=null) ? AppAssembly : Assembly.GetExecutingAssembly();
                var customAttributes = executingAssembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (customAttributes != null)
                {
                    var assemblyName = customAttributes[0] as AssemblyTitleAttribute;
                    appName = assemblyName.Title;
                }
                //string appName = Package.Current.Id.Name;
                return appName;
            }
        }

        /// <summary>
        /// Get the version of the App
        /// </summary>
        public string AppVersion
        {
            get
            {
                string appVersion = "";
                var executingAssembly = (AppAssembly != null) ? AppAssembly : Assembly.GetExecutingAssembly();
                var customAttributes = executingAssembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);
                if (customAttributes != null)
                {
                    var assemblyName = customAttributes[0] as AssemblyFileVersionAttribute;
                    appVersion = assemblyName.Version;
                }
                //var version = Package.Current.Id.Version;
                //string appVersion = String.Format("{0}.{1}.{2}.{3}",
                //    version.Major, version.Minor, version.Build, version.Revision);
                return appVersion;
            }
        }

        /// <summary>
        /// Get the Id of the App created by the Windows Store
        /// </summary>
        public string AppId
        {
            get
            {
                return CurrentApp.AppId.ToString();
            }
        }

        /// <summary>
        /// Get the Id of the App - Same as AppId
        /// </summary>
        public string AppInstallerId
        {
            get
            {
                return CurrentApp.AppId.ToString();
            }
        }


        public string DeviceIp
        {
            get
            {
                List<string> ipAddresses = new List<string>();
                var hostnames = NetworkInformation.GetHostNames();
                foreach (var hn in hostnames)
                {
                    //IanaInterfaceType == 71 => Wifi
                    //IanaInterfaceType == 6 => Ethernet (Emulator)
                    if (hn.IPInformation != null &&
                       (hn.IPInformation.NetworkAdapter.IanaInterfaceType == 71
                       || hn.IPInformation.NetworkAdapter.IanaInterfaceType == 6))
                    {
                        string ipAddress = hn.DisplayName;
                        ipAddresses.Add(ipAddress);
                    }
                }

                return ipAddresses[0];
            }
        }
        public string DeviceType { get {
        return DeviceStatus.DeviceManufacturer + " " + DeviceStatus.DeviceName + " " + DeviceStatus.DeviceHardwareVersion + " " + DeviceStatus.DeviceFirmwareVersion + " " + Environment.OSVersion;
        } }
    }
}
