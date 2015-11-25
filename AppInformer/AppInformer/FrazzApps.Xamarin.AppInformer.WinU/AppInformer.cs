using FrazzApps.Xamarin.AppInformer.Abstractions;
using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.AppInformer.WinU;
using Windows.ApplicationModel.Store;
using System.Collections.Generic;
using System.Reflection;
using Windows.Networking.Connectivity;
using Windows.ApplicationModel;
using Windows.Security.ExchangeActiveSyncProvisioning;

[assembly: Dependency(typeof(AppInformer))]
namespace FrazzApps.Xamarin.AppInformer.WinU
{
    /// <summary>
    /// AppInformer Implementation
    /// </summary>
    public class AppInformer : IAppInformer
    {

        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init() {}

        /// <summary>
        /// Get the name of the App - the assembly title information 
        /// </summary>
        public string AppName
        {
            get
            {
                return Package.Current.DisplayName;
            }
        }

        /// <summary>
        /// Get the version of the App
        /// </summary>
        public string AppVersion
        {
            get
            {
                var version = Package.Current.Id.Version;
                string appVersion = String.Format("{0}.{1}.{2}.{3}",
                    version.Major, version.Minor, version.Build, version.Revision);
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
        public string DeviceType
        {
            get
            {
                // get the device manufacturer and model name
                EasClientDeviceInformation eas = new EasClientDeviceInformation();
                string deviceManufacturer = eas.SystemManufacturer;
                string deviceModel = eas.SystemProductName;
                string deviceHardwareVersion = eas.SystemHardwareVersion;
                string deviceFirmwareVersion = eas.SystemFirmwareVersion;
                string deviceOSVersion = eas.OperatingSystem;
                return deviceManufacturer + " " + deviceModel + " " + deviceHardwareVersion + " " + deviceFirmwareVersion + " " + deviceOSVersion;
            }
        }
    }
}
