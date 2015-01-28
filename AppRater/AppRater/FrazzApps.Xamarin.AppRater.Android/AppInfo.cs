using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.AppRater.Android;
using FrazzApps.Xamarin.AppRater.Abstractions;
using Android.Content;
using Xamarin.Forms.Platform.Android;

[assembly: Dependency(typeof(FrazzApps.Xamarin.AppRater.Android.AppInfo))]
namespace FrazzApps.Xamarin.AppRater.Android
{
    /// <summary>
    /// Rater Implementation
    /// </summary>
    internal class AppInfo : IAppInfo
    {
        private FormsApplicationActivity activity;

        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        internal static void Init() { }
        public AppInfo() { }

        public string AppName
        {
            get
            {
                Context context = activity.ApplicationContext;
                return context.PackageManager.GetPackageInfo(context.PackageName, 0).ApplicationInfo.Name;
            }
        }

        public string AppId
        {
            get
            {
                Context context = activity.ApplicationContext;
                return context.PackageName;
            }
        }
    }
}
