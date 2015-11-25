using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FrazzApps.Xamarin.AnalyticsConnector
{
    public class GoogleAnalytics
    {
        private string TrackingID { get; set; }
        private string AppName { get; set; }
        private string AppVersion { get; set; }
        private string AppId { get; set; }
        private string AppInstallerId { get; set; }

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
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //request.Method = "POST";
            //request.ContentType = "application/x-www-form-urlencoded";
            
            StringContent queryString = new StringContent(url.Query);

            HttpClient client = new HttpClient();


           // WebResponse response = null;
            HttpResponseMessage response = null;
            try
            {
                response = await client.PostAsync(url, queryString);
                //response = await HttpWebHelper.GetResponseAsync(request);

                //System.Diagnostics.Debug.WriteLine("Tracking result = [" + ((HttpWebResponse)response).StatusCode + "] " + ((HttpWebResponse)response).StatusDescription);
                System.Diagnostics.Debug.WriteLine("Tracking result = [" + response.StatusCode + "] " + response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("GoogleAnalyticsHelper Tracking Exception: " + ex.Message + "\n - URL: " + url.OriginalString);
            }

                //if (request != null)
                //{
                //    request.Abort();
                //    request = null;
                //}
                if (client != null)
                {
                    client.Dispose();
                    client = null;
                }

                if (response != null)
                {
                    response.Dispose();
                    response = null;
                }
        }

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
             (isFatal)?1:2);

            Uri url = new Uri("http://www.google-analytics.com/collect?" + parametersBaseString + parametersPageView);

            Track(url);
        }

        public void TrackScreen(string userId, string screenName)
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

            Uri url = new Uri("http://www.google-analytics.com/collect?" + parametersBaseString + parametersPageView);

            Track(url);
        }
        
        public void TrackEvent(string userId, string category, string action)
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

//&t=event        // Event hit type
//&ec=video       // Event Category. Required.
//&ea=play        // Event Action. Required.
//&el=holiday     // Event label.
//&ev=300         // Event value.

            Uri url = new Uri("http://www.google-analytics.com/collect?" + parametersBaseString + parametersPageView);

            Track(url);
        }


    }
}
