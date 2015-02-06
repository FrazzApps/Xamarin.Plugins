using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.AppRater.WinPhone;
using FrazzApps.Xamarin.AppRater.Abstractions;
using Microsoft.Phone.Tasks;

[assembly: Dependency(typeof(FrazzApps.Xamarin.AppRater.WinPhone.AppRater))]
namespace FrazzApps.Xamarin.AppRater.WinPhone
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
            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
            marketplaceReviewTask.Show();
        }



    }
}
