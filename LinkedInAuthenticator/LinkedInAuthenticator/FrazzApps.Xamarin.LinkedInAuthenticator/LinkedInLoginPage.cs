using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FrazzApps.Xamarin.LinkedInAuthenticator
{
    public class LinkedInLoginPage : ContentPage
    {
        public event EventHandler<LinkedInAuthenticationEventArgs> SignInCompleted;

        public LinkedInLoginPage(string key, string secret, string scope, string redirectURL)
        {
            this.Title = "LinkedInLoginPage";

            WebView browser = new WebView();

            LinkedInConnection connection = new LinkedInConnection(key, secret, scope, redirectURL);
            connection.SignIn(browser);

            connection.SignInCompleted += Connection_SignInCompleted;


            Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);  // Accomodate iPhone status bar.
            Content = browser;

        }

        void Connection_SignInCompleted(object sender, EventArgs e)
        {
            LinkedInConnection connection = sender as LinkedInConnection;
            if (this != null)
            {
                if (this.SignInCompleted != null)
                {
                    this.SignInCompleted(this, new LinkedInAuthenticationEventArgs(connection.AccessCode, connection.AccessToken, connection.AccessTokenExpiry));
                }

                //if (Navigation.ModalStack.Contains(this))
                //    Navigation.PopModalAsync();
                //else if (Navigation.NavigationStack.Contains(this))
                //    Navigation.PopAsync();
            }
        }

        void browser_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.PropertyName);
            if (e.PropertyName.Equals("Source"))
            {
                WebView browser = sender as WebView;
                UrlWebViewSource source = browser.Source as UrlWebViewSource;
                System.Diagnostics.Debug.WriteLine(source.Url);
            }
        }
    }
}
