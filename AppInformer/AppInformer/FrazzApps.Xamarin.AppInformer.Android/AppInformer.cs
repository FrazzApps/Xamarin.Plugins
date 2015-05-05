using FrazzApps.Xamarin.AppInformer.Abstractions;
using System;
using Xamarin.Forms;
using Android.Content;
using Xamarin.Forms.Platform.Android;
using FrazzApps.Xamarin.AppInformer.Android;

[assembly: Dependency(typeof(AppInformer))]
namespace FrazzApps.Xamarin.AppInformer.Android
{
    /// <summary>
    /// AppInformer Implementation
    /// </summary>
    public class AppInformer : IAppInformer
    {

        public static FormsApplicationActivity Activity;

        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        internal static void Init(FormsApplicationActivity activity) { AppInformer.Activity = activity; }

        /// <summary>
        /// Get the name of the App
        /// </summary>
        public string AppName
        {
            get
            {
                Context context = AppInformer.Activity.ApplicationContext;
                return context.PackageManager.GetPackageInfo(context.PackageName, 0).ApplicationInfo.Name;
            }
        }

        /// <summary>
        /// Get the version of the App
        /// </summary>
        public string AppVersion
        {
            get
            {
                Context context = AppInformer.Activity.ApplicationContext;
                return context.PackageManager.GetPackageInfo(context.PackageName, 0).VersionName;
            }
        }

        /// <summary>
        /// Get the Id of the App - currently not working
        /// </summary>
        public string AppId
        {
            get
            {
                Context context = AppInformer.Activity.ApplicationContext;
                return context.PackageName;
            }
        }

        /// <summary>
        /// Get the Installer Id of the App - currently not working
        /// </summary>
        public string AppInstallerId
        {
            get
            {
                Context context = AppInformer.Activity.ApplicationContext;
                return context.PackageManager.GetInstallerPackageName(context.PackageName);
            }
        }
    }
}
