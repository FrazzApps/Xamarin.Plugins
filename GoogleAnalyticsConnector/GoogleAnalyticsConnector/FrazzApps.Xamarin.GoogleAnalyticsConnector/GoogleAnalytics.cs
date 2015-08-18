using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FrazzApps.Xamarin.GoogleAnalyticsConnector
{
    public class GoogleAnalytics
    {
        private string TrackingID { get; set; }
        private string AppName { get; set; }
        private string AppVersion { get; set; }
        private string AppId { get; set; }
        private string AppInstallerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trackingID">Tracking ID / Property ID.</param>
        /// <param name="appName"></param>
        /// <param name="appVersion"></param>
        /// <param name="appId"></param>
        /// <param name="appInstallerId"></param>
        public GoogleAnalytics(
            string trackingID,
            string appName,
            string appVersion,
            string appId,
            string appInstallerId)
        {
            TrackingID = trackingID;

            AppName = appName;
            AppVersion = appVersion;
            AppId = appId;
            AppInstallerId = appInstallerId;
        }

        private async void Track(Uri url)
        {
            StringContent queryString = new StringContent(url.Query);

            using (var client = new HttpClient())
            {

                try
                {
                    using (var response = await client.PostAsync(url, queryString))
                    {
                        System.Diagnostics.Debug.WriteLine("Tracking result = [" + response.StatusCode + "] " + response.ReasonPhrase);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("GoogleAnalyticsHelper Tracking Exception: " + ex.Message + "\n - URL: " + url.OriginalString);
                }
            }
        }
        /// <summary>
        /// Tracks a "PageView" type analytic
        /// </summary>
        /// <param name="userId">cid - Anonymous Client ID.</param>
        /// <param name="pageName"></param>
        public void TrackPage(string userId, string pageName)
        {
            //  System.Diagnostics.Debug.WriteLine("Tracking Page " + pageName);

            string parametersBaseString = string.Format(
               "v={0}&tid={1}&cid={2}",
               1,
               TrackingID,
               userId);

            string parametersPageView = string.Format(
            "&t=pageview&dp=%2F{0}",
            pageName);

            Uri url = new Uri("http://www.google-analytics.com/collect?" + parametersBaseString + parametersPageView);

            Track(url);
        }
        /// <summary>
        /// Tracks an "Exception" type analytic
        /// </summary>
        /// <param name="userId">cid - Anonymous Client ID.</param>
        /// <param name="ex"></param>
        /// <param name="isFatal"></param>
        public void TrackException(string userId, Exception ex, bool isFatal)
        {
            // System.Diagnostics.Debug.WriteLine("Tracking Exception " + ex.Message);

            string parametersBaseString = string.Format(
               "v={0}&tid={1}&cid={2}",
               1,
               TrackingID,
               userId);

            string parametersPageView = string.Format(
            "&t=exception&exd={0}&exf={1}",
            ex.Message,
            (isFatal) ? 1 : 2);

            Uri url = new Uri("http://www.google-analytics.com/collect?" + parametersBaseString + parametersPageView);

            Track(url);
        }

        /// <summary>
        /// Tracks a "Screenview" type analytic
        /// Reporting AppName, Version, ID, Installer and the screen name
        /// </summary>
        /// <param name="userId">cid - Anonymous Client ID.</param>
        /// <param name="screenName"></param>
        /// /// <param name="screenRes">Example value: 800x600</param>
        public void TrackScreen(string userId, string screenName, string screenRes = null)
        {
            // System.Diagnostics.Debug.WriteLine("Tracking Screen " + screenName);


            string parametersBaseString = string.Format(
               "v={0}&tid={1}&cid={2}",
               1,
               TrackingID,
               userId);

            string parametersPageView = string.Format(
            "&t=screenview&an={0}&av={1}&aid={2}&aiid={3}&cd={4}",
            AppName, // App name.
            AppVersion, // App version.
            AppId,// App Id.
            AppInstallerId,// App Installer Id.
            screenName);

            if (screenRes != null)
                parametersPageView += string.Format("&sr={0}", screenRes);

            Uri url = new Uri("http://www.google-analytics.com/collect?" + parametersBaseString + parametersPageView);

            Track(url);
        }

        /// <summary>
        /// Tracks an "event" type analytic
        /// </summary>
        /// <param name="userId">cid - Anonymous Client ID.</param>
        /// <param name="category">ec - Event Category.</param>
        /// <param name="action">ea - Event Action.</param>
        /// <param name="label">el - Event Label. Not Required</param>
        /// <param name="value">ev - Event Value. Not Required</param>
        public void TrackEvent(string userId, string category, string action, string label = null, int value = int.MinValue)
        {
            // System.Diagnostics.Debug.WriteLine("Tracking Event " + category + "|" + action);

            string parametersBaseString = string.Format(
               "v={0}&tid={1}&cid={2}",
               1,
               TrackingID,
               userId);

            string parametersPageView = string.Format(
            "&t=event&ec={0}&ea={1}",
            category, // Event Category. Required.
            action);

            string labelParam = null;
            if (label != null)
                labelParam = string.Format("&el={0}", label);

            string valueParam = null;
            if (value != int.MinValue)
                valueParam = string.Format("&ev={0}", value);

            //&t=event        // Event hit type
            //&ec=video       // Event Category. Required.
            //&ea=play        // Event Action. Required.
            //&el=holiday     // Event label.
            //&ev=300         // Event value.

            Uri url = new Uri("http://www.google-analytics.com/collect?" + parametersBaseString + parametersPageView + (labelParam ?? "") + (valueParam ?? ""));

            Track(url);
        }


    }
}
