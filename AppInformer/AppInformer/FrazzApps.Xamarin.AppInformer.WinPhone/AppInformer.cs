using FrazzApps.Xamarin.AppInformer.Abstractions;
using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.AppInformer.WinPhone;
using Windows.ApplicationModel.Store;

[assembly: Dependency(typeof(AppInformer))]
namespace FrazzApps.Xamarin.AppInformer.WinPhone
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
        /// Get the name of the App - the assembly title information 
        /// </summary>
        public string AppName
        {
            get
            {
                string appName = "";
                var executingAssembly = System.Reflection.Assembly.GetExecutingAssembly();
                var customAttributes = executingAssembly.GetCustomAttributes(typeof(System.Reflection.AssemblyTitleAttribute), false);
                if (customAttributes != null)
                {
                    var assemblyName = customAttributes[0] as System.Reflection.AssemblyTitleAttribute;
                    appName = assemblyName.Title;
                }
                //string appName = Package.Current.Id.Name;
                return appName;
            }
        }

        /// <summary>
        /// Get the version of the App
        /// </summary>
        public string AppVersion
        {
            get
            {
                string appVersion = "";
                var executingAssembly = System.Reflection.Assembly.GetExecutingAssembly();
                var customAttributes = executingAssembly.GetCustomAttributes(typeof(System.Reflection.AssemblyFileVersionAttribute), false);
                if (customAttributes != null)
                {
                    var assemblyName = customAttributes[0] as System.Reflection.AssemblyFileVersionAttribute;
                    appVersion = assemblyName.Version;
                }
                //var version = Package.Current.Id.Version;
                //string appVersion = String.Format("{0}.{1}.{2}.{3}",
                //    version.Major, version.Minor, version.Build, version.Revision);
                return appVersion;
            }
        }

        /// <summary>
        /// Get the Id of the App created by the Windows Store
        /// </summary>
        public string AppId
        {
            get
            {
                return CurrentApp.AppId.ToString();
            }
        }

        /// <summary>
        /// Get the Id of the App - Same as AppId
        /// </summary>
        public string AppInstallerId
        {
            get
            {
                return CurrentApp.AppId.ToString();
            }
        }
    }
}
