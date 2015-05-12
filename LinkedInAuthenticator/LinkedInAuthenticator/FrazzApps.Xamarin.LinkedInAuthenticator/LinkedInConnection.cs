using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FrazzApps.Xamarin.LinkedInAuthenticator
{
    internal class LinkedInConnection
    {
        public event EventHandler SignInCompleted;

        private string Key { get; set; }
        private string Secret { get; set; }
        private string Scope { get; set; }
                
        private string CSRFCode = Guid.NewGuid().ToString();

        internal string AccessCode { get; set; }
        internal string AccessToken { get; set; }
        internal DateTime AccessTokenExpiry { get; set; }

        private const string AuthoriseURL = "https://www.linkedin.com/uas/oauth2/authorization";
        private string RedirectURL = "http://localhost/oauth"; //TODO Set the Success page
        private const string AccessTokenURL = "https://www.linkedin.com/uas/oauth2/accessToken";
        //TODO -> private const string RequestTokenURL = "https://graph.linkedin.com/oauth/request_token";


        public LinkedInConnection(string key, string secret, string scope, string redirectURL)
        {
            this.Key = key;
            this.Secret = secret;
            this.Scope = scope;
            this.RedirectURL = redirectURL;
        }



        internal Uri LoginUri()
        {
            //https://www.linkedin.com/uas/oauth2/authorization?response_type=code&client_id=YOUR_API_KEY&scope=SCOPE&state=STATE&redirect_uri=YOUR_REDIRECT_URI

            string responseType = "code";
            string display = "popup";
            return new Uri(
                String.Format(@"{0}?client_id={1}&redirect_uri={3}&response_type={4}&scope={5}&state={6}",
                AuthoriseURL,
                Key,//YOUR_API_KEY
                display,
                RedirectURL,
                responseType,
                Scope,
                CSRFCode));
        }

        internal Uri AuthenticateUri()
        {
            //https://www.linkedin.com/uas/oauth2/accessToken?grant_type=authorization_code&code=AUTHORIZATION_CODE&redirect_uri=YOUR_REDIRECT_URI&client_id=YOUR_API_KEY&client_secret=YOUR_SECRET_KEY
            //string redirectURL = WebUtility.UrlEncode("http://localhost/oauth");
            return new Uri(
                String.Format(@"{0}?grant_type=authorization_code&client_id={1}&redirect_uri={2}&client_secret={3}&code={4}",
                AccessTokenURL,
                Key,//app-id
                RedirectURL,
                Secret,
                AccessCode));
        }

        internal void SignIn(WebView browser)
        {
            browser.Source = LoginUri();

            browser.PropertyChanged += browser_PropertyChanged;
        }

        internal async void Authenticate()
        {
            System.Diagnostics.Debug.WriteLine("Authenticating");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(AuthenticateUri());
            request.Method = "GET";

			try{
            WebResponse response = await GetResponseAsync(request);

            StreamReader sr = new StreamReader(response.GetResponseStream());
            string result = sr.ReadToEnd();

            JObject jobj = JObject.Parse(result);

            AccessToken = jobj["access_token"].ToString();
            AccessTokenExpiry = DateTime.Now.AddSeconds(Double.Parse(jobj["expires_in"].ToString()));

			}catch(Exception ex) {
				System.Diagnostics.Debug.WriteLine("LinkedInConnection Authentication error:" + ex.Message);

			}
            
			System.Diagnostics.Debug.WriteLine("Authentication Complete");
            this.SignInCompleted(this, new EventArgs());
        }


        private  Task<WebResponse> GetResponseAsync(HttpWebRequest request)
        {
            if (request == null) throw new ArgumentNullException("request");
            return Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, null);
        }



        private void browser_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Source"))
            {
                WebView browser = sender as WebView;
                if (browser != null)
                {
                     UrlWebViewSource source = browser.Source as UrlWebViewSource;
                    if (source != null)
                    {
                        // https://www.linkedin.com/connect/login_success.html?code=AQCU-FgxysBWTCSnCuqH3b4i1FOHdDwWx6huLDTABcsgPcbj4wufFAoaUmcG55FOSsYUkOCSftQnHbm9lyc7p7eI5lY07QfK4D4banQng3Aup3dFcRK1jUK1-no-d-zS6IC0JQojZAD-guPrqkgt5P1B1ch7Go96XpK3P59PhgqHOvrbE7UzkQLejbcgkzTDBOUtphOFVuHdT_ZcqCuo11Bi945DT210gYp07uZQGKFtflD6TmrFMgu6h-dkmaWELkV3efgFVAGZCqVzsn5S5yEWugE2TCgZTafkbXQ2In8u_6eYPkAzPl9xS5p-R6KxVSA#_=_
                        if (source.Url.StartsWith(RedirectURL))
                        {
                            System.Diagnostics.Debug.WriteLine("Login Success");
                            Uri result = new Uri(source.Url);
                            Dictionary<string, string> queryValues = ParseQueryString(result);
                            AccessCode = queryValues["code"];
                            Authenticate();
                        }
                    }
                }
            }
        }


        private Dictionary<string, string> ParseQueryString(Uri uri)
        {
            var query = uri.Query.Substring(uri.Query.IndexOf('?') + 1); // +1 for skipping '?'
            return ParseQueryString(query);
        }

        private Dictionary<string, string> ParseQueryString(string queryString)
        {
            var pairs = queryString.Split('&');
            return pairs
                .Select(o => o.Split('='))
                .Where(items => items.Count() == 2)
                .ToDictionary(pair => Uri.UnescapeDataString(pair[0]),
                    pair => Uri.UnescapeDataString(pair[1]));
        }

    }
}
