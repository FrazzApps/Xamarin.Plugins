using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.AppRater.WinU;
using FrazzApps.Xamarin.AppRater.Abstractions;
using Windows.ApplicationModel.Store;

[assembly: Dependency(typeof(AppRater))]
namespace FrazzApps.Xamarin.AppRater.WinU
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
            //MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
            //marketplaceReviewTask.Show();
            string uriString = "ms - windows - store://review/?ProductId=" + CurrentApp.AppId;
            Windows.System.Launcher.LaunchUriAsync(new Uri(uriString));
        }



    }
}
