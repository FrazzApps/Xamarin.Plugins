using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FrazzApps.Xamarin.LinkedInAuthenticator
{
    public class LinkedInLoginPage : ContentPage
    {
        WebView _Browser = null;
        StackLayout _BaseLayout = null;

        private LinkedInConnection _Connection = null;
        //public event EventHandler<LinkedInAuthenticationEventArgs> SignInCompleted;


        public LinkedInLoginPage()
        {
            this.Title = "LinkedInLoginPage";

			Label lbl = new Label ()
			{ Text = "key, secret, scope and redirectURL required..." };

			_BaseLayout = new StackLayout();

			_BaseLayout.Children.Add(lbl);


            Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);  // Accomodate iPhone status bar.
			Content = _BaseLayout;
        }


        public LinkedInLoginPage(LinkedInConnection connection)//string key, string secret, string scope, string redirectURL)
        {
            this.Title = "LinkedInLoginPage";

            this._Connection = connection;
            this._Browser = new WebView();

           // LinkedInConnection connection = new LinkedInConnection(key, secret, scope, redirectURL);
            this._Connection.SignIn(this._Browser);
            Label lbl = new Label() { Text = "Connecting..." };
            lbl.SetBinding(Label.IsVisibleProperty, "IsVisible", BindingMode.OneWay, new BooleanInverterConverter());
            lbl.BindingContext = this._Browser;

            this._BaseLayout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            this._BaseLayout.Children.Add(lbl);
            this._BaseLayout.Children.Add(this._Browser);

            this._Browser.IsVisible = false;


            Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);  // Accomodate iPhone status bar.
            Content = this._BaseLayout;

        }

        //void Connection_SignInCompleted(object sender, EventArgs e)
        //{
        //    LinkedInConnection connection = sender as LinkedInConnection;
        //    if (this != null)
        //    {
        //        if (this.SignInCompleted != null)
        //        {
        //            this.SignInCompleted(this, new LinkedInAuthenticationEventArgs(connection.AccessCode, connection.AccessToken, connection.AccessTokenExpiry));
        //        }

        //        //if (Navigation.ModalStack.Contains(this))
        //        //    Navigation.PopModalAsync();
        //        //else if (Navigation.NavigationStack.Contains(this))
        //        //    Navigation.PopAsync();
        //    }
        //}

        //void browser_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    System.Diagnostics.Debug.WriteLine(e.PropertyName);
        //    if (e.PropertyName.Equals("Source"))
        //    {
        //        WebView browser = sender as WebView;
        //        UrlWebViewSource source = browser.Source as UrlWebViewSource;
        //        System.Diagnostics.Debug.WriteLine(source.Url);
        //    }
        //}




        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (this._Browser != null)
            {
                if (this._Connection != null)
                {
                    this._Connection.SignIn(_Browser);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("LinkedInLoginPage On Appearing _Connection == null");
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("LinkedInLoginPage On Appearing _Browser == null");
            }
        }





        public class BooleanInverterConverter : IValueConverter
        {
            public object Convert(object value, Type targetType,
                object parameter, CultureInfo culture)
            {
                bool b = (bool)value;
                return !b;
            }

            public object ConvertBack(object value, Type targetType,
                object parameter, CultureInfo culture)
            {
                bool b = (bool)value;
                return !b;
            }
        }
    }
}
