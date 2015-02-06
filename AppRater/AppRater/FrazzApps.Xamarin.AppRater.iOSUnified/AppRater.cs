
using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.AppRater.iOSUnified;
using FrazzApps.Xamarin.AppRater.Abstractions;
using Foundation;
using UIKit;

[assembly: Dependency(typeof(FrazzApps.Xamarin.AppRater.iOSUnified.AppRater))]
namespace FrazzApps.Xamarin.AppRater.iOSUnified
{
    /// <summary>
    /// Rater Implementation
    /// </summary>
    public class AppRater : IAppRater
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init()
        {
            EmailComposer.Init();
        }
        public AppRater() { }


        public void RateApp(string id)
        {
            String url;
            float iOSVersion = float.Parse(UIDevice.CurrentDevice.SystemVersion);

            if (iOSVersion >= 7.0f && iOSVersion < 7.1f)
            {
                url = "itms-apps://itunes.apple.com/app/id" + id;
            }
            else
            {
                url = "itms-apps://itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews?type=Purple+Software&id=" + id;
                UIApplication.SharedApplication.OpenUrl(new NSUrl(url));
            }
        }
    }
}
