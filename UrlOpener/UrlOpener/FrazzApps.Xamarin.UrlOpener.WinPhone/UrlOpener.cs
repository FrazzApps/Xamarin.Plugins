using FrazzApps.Xamarin.UrlOpener.Abstractions;
using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.UrlOpener.WinPhone;

[assembly: Dependency(typeof(UrlOpener))]
namespace FrazzApps.Xamarin.UrlOpener.WinPhone
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
