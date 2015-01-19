using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace FrazzApps.Xamarin.FacebookAuthenticator
{
    public class FacebookLoginPage : ContentPage
    {

        public event EventHandler<FacebookAuthenticationEventArgs> SignInCompleted;

        public FacebookLoginPage(string key, string secret, string scope)
        {
            this.Title = "FacebookLoginPage";

            WebView browser = new WebView();

            FacebookConnection connection = new FacebookConnection(key, secret, scope);
            connection.SignIn(browser);

            connection.SignInCompleted += Connection_SignInCompleted;
            
            Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);  // Accomodate iPhone status bar.
            Content = browser;
        }

        void Connection_SignInCompleted(object sender, EventArgs e)
        {
            FacebookConnection connection = sender as FacebookConnection;
            if (this != null)
            {
                if (this.SignInCompleted != null)
                {
                    this.SignInCompleted(this, new FacebookAuthenticationEventArgs(connection.AccessCode, connection.AccessToken, connection.AccessTokenExpiry));
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
