using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.AppRater.WinPhone;
using FrazzApps.Xamarin.AppRater.Abstractions;
using Microsoft.Phone.Tasks;
using Windows.ApplicationModel.Store;

[assembly: Dependency(typeof(FrazzApps.Xamarin.AppRater.WinPhone.AppInfo))]
namespace FrazzApps.Xamarin.AppRater.WinPhone
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

        public string AppId
        {
            get
            {
                return CurrentApp.AppId.ToString();
            }
        }


    }
}
