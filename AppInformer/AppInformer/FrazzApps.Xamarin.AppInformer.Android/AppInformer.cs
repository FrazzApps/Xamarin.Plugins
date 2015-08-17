using FrazzApps.Xamarin.AppInformer.Abstractions;
using System;
using Xamarin.Forms;
using Android.Content;
using Xamarin.Forms.Platform.Android;
using FrazzApps.Xamarin.AppInformer.Android;
using Java.Net;

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
        public static void Init(FormsApplicationActivity activity) { AppInformer.Activity = activity; }

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


        public string DeviceIp
        {
            get
            {
                Java.Util.IEnumeration networkInterfaces = NetworkInterface.NetworkInterfaces;
                while (networkInterfaces.HasMoreElements)
                {
                    Java.Net.NetworkInterface netInterface = (Java.Net.NetworkInterface)networkInterfaces.NextElement();
                    Console.WriteLine(netInterface.ToString());
                }
                return "1.1.1.1";
            }
        }
        public string DeviceType { get { return string.Format("{0} {1}", global::Android.OS.Build.Manufacturer, global::Android.OS.Build.Model); } }

        public string DeviceModal
        {
            get
            {
                // read and return current device model
                return string.IsNullOrEmpty(global::Android.OS.Build.Model) ?
					"Android" :
                global::Android.OS.Build.Model.StartsWith(global::Android.OS.Build.Manufacturer) ?
					string.Format("Android {0}", global::Android.OS.Build.Model) :
					string.Format("Android {0} {1}", global::Android.OS.Build.Manufacturer, global::Android.OS.Build.Model);

                
            }
        }
    }
}
