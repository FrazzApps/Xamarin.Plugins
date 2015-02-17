using FrazzApps.Xamarin.UrlOpener.Abstractions;
using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.UrlOpener.iOS;

[assembly: Dependency(typeof(UrlOpener))]
namespace FrazzApps.Xamarin.UrlOpener.iOS
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
            MonoTouch.UIKit.UIApplication.SharedApplication.OpenUrl(new MonoTouch.Foundation.NSUrl(url));
        }
    }
}
