using System;

namespace FrazzApps.Xamarin.NetworkInspector.Abstractions
{
    /// <summary>
    /// NetworkInspector Interface
    /// </summary>
    public interface INetworkInspector
    {

        bool IsConnected { get; }

        bool IsWifi { get; }

        bool IsMobileCarrier { get; }
    }

}

namespace FrazzApps.Xamarin.NetworkInspector
{
public enum NetworkStatus
{
    NotReachable,
    ReachableViaCarrierDataNetwork,
    ReachableViaWiFiNetwork
}
}