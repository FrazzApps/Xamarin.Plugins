using FrazzApps.Xamarin.NetworkInspector.Abstractions;
using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.NetworkInspector.WinU;
using System.Net.NetworkInformation;
using Windows.Networking.Connectivity;

[assembly: Dependency(typeof(NetworkInspector))]
namespace FrazzApps.Xamarin.NetworkInspector.WinU
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
                    var profile = NetworkInformation.GetInternetConnectionProfile();
                    switch (profile.NetworkAdapter.IanaInterfaceType)
                    {
                        case 71: //wifi
                            return true;

                        case 243://Cellular;
                        case 244://Cellular;
                        default://Other;
                            break;
                    }
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
                    var profile = NetworkInformation.GetInternetConnectionProfile();
                    switch (profile.NetworkAdapter.IanaInterfaceType)
                    {
                        case 243://Cellular;
                        case 244://Cellular;
                            return true;

                        case 71: //wifi
                        default://Other;
                            break;
                    }
                }
                return false;
            }
        }
    }
}
