using FrazzApps.Xamarin.NetworkInspector.Abstractions;
using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.NetworkInspector.Android;
using Android.Net;
using Android.Content;

[assembly: Dependency(typeof(NetworkInspector))]
namespace FrazzApps.Xamarin.NetworkInspector.Android
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

                var connectivityManager = (ConnectivityManager)global::Android.App.Application.Context.GetSystemService(Context.ConnectivityService);
                var activeConnection = connectivityManager.ActiveNetworkInfo;
                return ((activeConnection != null) && activeConnection.IsConnected);
            }
        }

        public bool IsWifi
        {
            get
            {
                var connectivityManager = (ConnectivityManager)global::Android.App.Application.Context.GetSystemService(Context.ConnectivityService);
                var activeConnection = connectivityManager.ActiveNetworkInfo;
                var mobileState = connectivityManager.GetNetworkInfo(ConnectivityType.Wifi).GetState();
                return (mobileState == NetworkInfo.State.Connected);
            }
        }


        public bool IsMobileCarrier
        {
            get
            {
                var connectivityManager = (ConnectivityManager)global::Android.App.Application.Context.GetSystemService(Context.ConnectivityService);
                var activeConnection = connectivityManager.ActiveNetworkInfo;
                var mobileState = connectivityManager.GetNetworkInfo(ConnectivityType.Mobile).GetState();
                return (mobileState == NetworkInfo.State.Connected);
            }
        }

    }
}
