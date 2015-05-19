using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using System.Globalization;

namespace FrazzApps.Xamarin.FacebookAuthenticator
{
    public class FacebookLoginPage : ContentPage
	{
		WebView _Browser = null;
		StackLayout _BaseLayout = null;

        private FacebookConnection _Connection = null;
       // public event EventHandler<FacebookAuthenticationEventArgs> SignInCompleted;

        public FacebookLoginPage()
        {
            this.Title = "FacebookLoginPage";

			Label lbl = new Label ()
			{ Text = "key, secret, and scope required..." };

			_BaseLayout = new StackLayout();

			_BaseLayout.Children.Add(lbl);


            Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);  // Accomodate iPhone status bar.
			Content = _BaseLayout;
        }

        public FacebookLoginPage(FacebookConnection connection)
        {
            this.Title = "FacebookLoginPage";

         //   Connection = new FacebookConnection(key, secret, scope);
         //   connection.SignInCompleted += Connection_SignInCompleted;
            
            this._Connection = connection;

            this._Browser = new WebView();
			//_Browser.Source = new UrlWebViewSource() { Url = _Connection.LoginUri().AbsoluteUri };
            this._Connection.SignIn(this._Browser);

			Label lbl = new Label ()
			{ Text = "Connecting..." };
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
        //    FacebookConnection connection = sender as FacebookConnection;
        //    if (this != null)
        //    {
        //        if (this.SignInCompleted != null)
        //        {
        //            this.SignInCompleted(this, new FacebookAuthenticationEventArgs(connection.AccessCode, connection.AccessToken, connection.AccessTokenExpiry));
        //        }
        //    }
        //}

        //void browser_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    System.Diagnostics.Debug.WriteLine("FacebookLoginPage On Appearing _Browser PropertyChanged = " + e.PropertyName);
        //    if (e.PropertyName.Equals("Source"))
        //    {
        //        WebView browser = sender as WebView;
        //        UrlWebViewSource source = browser.Source as UrlWebViewSource;
        //        System.Diagnostics.Debug.WriteLine(source.Url);
        //    }
        //}


        protected override void OnAppearing ()
        {
            base.OnAppearing ();

            if (this._Browser != null)
            {
        //        _Browser.IsVisible = true;// false;

        //        try{
        //        if (_Browser.Parent == null)
        //            _BaseLayout.Children.Add (_Browser);
        //        }catch(Exception ex) {
        //            System.Diagnostics.Debug.WriteLine ("FacebookLoginPage On Appearing Exception = " + ex.Message);
        //        }

                if (this._Connection != null)
                {
                    this._Connection.SignIn(_Browser);
                } else {
                    System.Diagnostics.Debug.WriteLine ("FacebookLoginPage On Appearing _Connection == null");
                }
            } else
           {
                System.Diagnostics.Debug.WriteLine ("FacebookLoginPage On Appearing _Browser == null");
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
