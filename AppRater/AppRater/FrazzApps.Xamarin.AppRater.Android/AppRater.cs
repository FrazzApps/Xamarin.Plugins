using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.AppRater.Android;
using FrazzApps.Xamarin.AppRater.Abstractions;
using Android.Content;
using Android.Widget;

[assembly: Dependency(typeof(FrazzApps.Xamarin.AppRater.Android.AppRater))]
namespace FrazzApps.Xamarin.AppRater.Android
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

        public void RateApp(string id) { RateApp(id, true); }

        public void RateApp(string id, bool googlePlay) { 
            //true if Google Play, false if Amazone Store
            try
            {
                Forms.Context.StartActivity(new Intent(Intent.ActionView, global::Android.Net.Uri.Parse((googlePlay ? "market://details?id=" : "amzn://apps/android?p=") + id)));
            }
            catch (ActivityNotFoundException e1)
            {
                try
                {
                    Forms.Context.StartActivity(new Intent(Intent.ActionView, global::Android.Net.Uri.Parse((googlePlay ? "http://play.google.com/store/apps/details?id=" : "http://www.amazon.com/gp/mas/dl/android?p=") + id)));
                    Console.WriteLine("AppRater Android Exception 1: " + e1.Message);
                }
                catch (ActivityNotFoundException e2)
                {
                    Toast.MakeText(Forms.Context, "You don't have any app that can open this link", ToastLength.Short).Show();
                    Console.WriteLine("AppRater Android Exception 2: " + e2.Message);
                }
            }
        }
    }
}
