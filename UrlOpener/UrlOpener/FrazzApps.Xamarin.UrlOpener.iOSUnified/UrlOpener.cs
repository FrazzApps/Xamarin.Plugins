using FrazzApps.Xamarin.UrlOpener.Abstractions;
using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.UrlOpener.iOSUnified;
using Foundation;
using UIKit;

[assembly: Dependency(typeof(UrlOpener))]
namespace FrazzApps.Xamarin.UrlOpener.iOSUnified
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
            UIApplication.SharedApplication.OpenUrl(new NSUrl(url));
        }
    }
}
