using FrazzApps.Xamarin.NetworkInspector.Abstractions;
using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.NetworkInspector.WinPhone;
using Microsoft.Phone.Net.NetworkInformation;
using Windows.Networking.Connectivity;

[assembly: Dependency(typeof(NetworkInspector))]
namespace FrazzApps.Xamarin.NetworkInspector.WinPhone
{
    /// <summary>
    /// NetworkInspector Implementation
    /// </summary>
    public class NetworkInspector : INetworkInspector
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init() { }


        public bool IsConnected
        {
            get
            {
                bool isConnected = NetworkInterface.GetIsNetworkAvailable();
                if (!isConnected)
                {
                    return false;
                }
                else
                {
                    ConnectionProfile InternetConnectionProfile = NetworkInformation.GetInternetConnectionProfile();
                    NetworkConnectivityLevel connection = InternetConnectionProfile.GetNetworkConnectivityLevel();
                    if (connection == NetworkConnectivityLevel.None || connection == NetworkConnectivityLevel.LocalAccess)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public bool IsWifi
        {
            get
            {
                if (IsConnected)
                {
                    if (NetworkInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                        return true;
                }
                return false;
            }
        }

        public bool IsMobileCarrier
        {
            get
            {
                if (IsConnected)
                {
                    if ((NetworkInterface.NetworkInterfaceType == NetworkInterfaceType.MobileBroadbandCdma) ||  (NetworkInterface.NetworkInterfaceType == NetworkInterfaceType.MobileBroadbandGsm))
                        return true;
                }
                return false;
            }
        }
    }
}
