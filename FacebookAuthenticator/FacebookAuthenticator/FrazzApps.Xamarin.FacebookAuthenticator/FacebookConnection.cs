 using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FrazzApps.Xamarin.FacebookAuthenticator
{

    public class FacebookConnection
    {
        public event EventHandler<FacebookAuthenticationEventArgs> SignInCompleted;
        public event EventHandler<FacebookIdentificationEventArgs> GetAccountInfoCompleted;

        private string Key { get; set; }
        private string Secret { get; set; }
        private string Scope { get; set; }

        internal string AccessCode { get; set; }
        internal string AccessToken { get; set; }
        internal DateTime AccessTokenExpiry { get; set; }

        private const string AuthoriseURL = "https://www.facebook.com/dialog/oauth";
        private const string RedirectURL = "https://www.facebook.com/connect/login_success.html";
        private const string AccessTokenURL = "https://graph.facebook.com/oauth/access_token";
        //TODO -> private const string RequestTokenURL = "https://graph.facebook.com/oauth/request_token";

		private const string BaseAccessUrl = "https://graph.facebook.com";


        public FacebookConnection(string key, string secret, string scope)
        {
            this.Key = key;
            this.Secret = secret;
            this.Scope = scope;
        }
        
        internal Uri LoginUri()
        {
            //https://www.facebook.com/dialog/oauth?client_id={app-id}&redirect_uri={redirect-uri}

            string responseType = "code";
            string display = "popup";
            return new Uri(
                String.Format(@"{0}?client_id={1}&redirect_uri={3}&response_type={4}&scope={5}",
                AuthoriseURL,
                Key,//app-id
                display,
                RedirectURL,
                responseType,
                Scope));
        }

        internal Uri AuthenticateUri()
        {
            //https://graph.facebook.com/oauth/access_token?client_id={app-id}&redirect_uri={redirect-uri}&client_secret={app-secret}&code={code-parameter}

            //string redirectURL = WebUtility.UrlEncode("http://localhost/oauth");
            return new Uri(
                String.Format(@"{0}?client_id={1}&redirect_uri={2}&client_secret={3}&code={4}",
                AccessTokenURL,
                Key,//app-id
                RedirectURL,
                Secret,
                AccessCode));
        }

        internal void SignIn(WebView browser)
		{
		//	browser.IsVisible = true;
            browser.Source = LoginUri();

            browser.PropertyChanged += browser_PropertyChanged;
        }

        internal async void Authenticate()
        {
            System.Diagnostics.Debug.WriteLine("Authenticating");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(AuthenticateUri());
            request.Method = "GET";
            WebResponse response = await GetResponseAsync(request);

            StreamReader sr = new StreamReader(response.GetResponseStream());
            string result = sr.ReadToEnd();
            Dictionary<string, string> queryValues = ParseQueryString(result);

            this.AccessToken = queryValues["access_token"];
            this.AccessTokenExpiry = DateTime.Now.AddSeconds(Double.Parse(queryValues["expires"]));

            System.Diagnostics.Debug.WriteLine("Authentication Complete");
            this.SignInCompleted(this, new FacebookAuthenticationEventArgs(this.AccessCode, this.AccessToken, this.AccessTokenExpiry));
        }



        private void browser_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Source"))
            {
                WebView browser = sender as WebView;
                browser.IsVisible = true;
                if (browser != null)
                {
                    UrlWebViewSource source = browser.Source as UrlWebViewSource;
                    if (source != null)
                    {
                        // https://www.facebook.com/connect/login_success.html?code=AQCU-FgxysBWTCSnCuqH3b4i1FOHdDwWx6huLDTABcsgPcbj4wufFAoaUmcG55FOSsYUkOCSftQnHbm9lyc7p7eI5lY07QfK4D4banQng3Aup3dFcRK1jUK1-no-d-zS6IC0JQojZAD-guPrqkgt5P1B1ch7Go96XpK3P59PhgqHOvrbE7UzkQLejbcgkzTDBOUtphOFVuHdT_ZcqCuo11Bi945DT210gYp07uZQGKFtflD6TmrFMgu6h-dkmaWELkV3efgFVAGZCqVzsn5S5yEWugE2TCgZTafkbXQ2In8u_6eYPkAzPl9xS5p-R6KxVSA#_=_
                        if (source.Url.StartsWith(RedirectURL))
                        {
							browser.IsVisible = false;
                            System.Diagnostics.Debug.WriteLine("Login Success");
                            Uri result = new Uri(source.Url);
                            Dictionary<string, string> queryValues = ParseQueryString(result);
                            this.AccessCode = queryValues["code"];
                            Authenticate();
                        }
                    }
                }
            }
        }



		public async Task<bool> GetAccountInfo(FacebookAuthenticationEventArgs authenticationEventArgs)
		{
			Uri url = new Uri(BaseAccessUrl + "/me?access_token=" + authenticationEventArgs.AccessToken);

			string result = await CallApi(url);


            //try
            //{
            //    JObject jobj = JObject.Parse(result);


            //    user.Username = jobj["name"].ToString();
            //    user.Email = jobj["email"].ToString();
            //}
            //catch (Exception ex)
            //{
            //    DiagnosticsHelper.Instance.WriteLine("FacebookConnection GetAccountInfo error:" + ex.Message);
            //}


			this.GetAccountInfoCompleted(this, new FacebookIdentificationEventArgs(authenticationEventArgs, result));
			return true;
		}




        #region Helper Functions


        private async Task<String> CallApi(Uri url)
        {
            string result = "";

            HttpWebRequest request = null;
            WebResponse response = null;

            request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";

            try
            {
                response = await GetResponseAsync(request);

                StreamReader sr = new StreamReader(response.GetResponseStream());
                result = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Facebook CallApi Exception: " + ex.Message + "\n - URL: " + url.OriginalString);
            }
            finally
            {
                if (request != null)
                {
                    request.Abort();
                    request = null;
                }

                if (response != null)
                {
                    response.Dispose();
                    response = null;
                }

            }

            return result;
        }


        private Task<WebResponse> GetResponseAsync(HttpWebRequest request)
        {
            if (request == null) throw new ArgumentNullException("request");
            return Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, null);
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

        #endregion


    }
}
