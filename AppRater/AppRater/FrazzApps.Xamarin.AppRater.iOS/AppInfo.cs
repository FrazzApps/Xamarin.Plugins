using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.AppRater.iOS;
using FrazzApps.Xamarin.AppRater.Abstractions;
using MonoTouch.Foundation;

[assembly: Dependency(typeof(FrazzApps.Xamarin.AppRater.iOS.AppInfo))]
namespace FrazzApps.Xamarin.AppRater.iOS
{
    /// <summary>
    /// Rater Implementation
    /// </summary>
    internal class AppInfo : IAppInfo
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        internal static void Init() { }
        public AppInfo() { }

        public string AppName
        {
            get
            {
                return NSBundle.MainBundle.InfoDictionary["CFBundleDisplayName"].ToString();
            }
        }

        public string AppId
        {
            get
            {
                return "{AppId52C95796-11A1-4247-BC3B-1F455C3298DE}";
            }
        }

    }
}
