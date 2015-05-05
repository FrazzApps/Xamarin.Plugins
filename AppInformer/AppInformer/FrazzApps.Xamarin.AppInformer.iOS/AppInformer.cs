using FrazzApps.Xamarin.AppInformer.Abstractions;
using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.AppInformer.iOS;
using MonoTouch.Foundation;

[assembly: Dependency(typeof(AppInformer))]
namespace FrazzApps.Xamarin.AppInformer.iOS
{
    /// <summary>
    /// AppInformer Implementation
    /// </summary>
    public class AppInformer : IAppInformer
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init() { }

        /// <summary>
        /// Get the name of the App
        /// </summary>
        public string AppName
        {
            get
            {
                return NSBundle.MainBundle.InfoDictionary["CFBundleDisplayName"].ToString();
            }
        }

        /// <summary>
        /// Get the version of the App
        /// </summary>
        public string AppVersion
        {
            get
            {
                return NSBundle.MainBundle.InfoDictionary["CFBundleShortVersionString"].ToString();
            }
        }

        /// <summary>
        /// Get the Id of the App - currently not working
        /// </summary>
        public string AppId
        {
            get
            {
                return "{AppId52C95796-11A1-4247-BC3B-1F455C3298DE}";
            }
        }

        /// <summary>
        /// Get the Installer Id of the App - currently not working
        /// </summary>
        public string AppInstallerId
        {
            get
            {
                return "{AppInstallerId52C95796-11A1-4247-BC3B-1F455C3298DE}";
            }
        }
    }
}
