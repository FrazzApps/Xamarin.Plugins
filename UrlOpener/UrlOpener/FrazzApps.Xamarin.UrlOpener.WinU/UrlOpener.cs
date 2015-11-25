using FrazzApps.Xamarin.UrlOpener.Abstractions;
using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.UrlOpener.WinU;

[assembly: Dependency(typeof(UrlOpener))]
namespace FrazzApps.Xamarin.UrlOpener.WinU
{
    /// <summary>
    /// UrlOpener Implementation
    /// </summary>
    public class UrlOpener : IUrlOpener
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init() { }


        public void Open(string url)
        {
            Windows.System.Launcher.LaunchUriAsync(new Uri(url));
        }
    }
}
