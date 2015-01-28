# Application Rating Plugin for Xamarin Forms
Cross platform plugins to allow users to easily rate your app.


##Setup
- Currently in Alpha
- Available on NuGet: [TBD]
- Install into your PCL project and Client projects

###Supports
- Xamarin.iOS
- Xamarin.iOS (x64 Unified)
- Xamarin.Android
- Windows Phone 8 (Silverlight)

##API Usage

Note you must initialize the plugin on each platform:

            	AppRater.Init();

Launching a rate app process:

                IAppRater appRater = DependencyService.Get<IAppRater>();
                appRater.RateApp(appInfo.AppId);

Launching a Rate/Feedback user request process:

                RateFeedbackDialog rateDialog = new RateFeedbackDialog("<emailaddress>");
                rateDialog.RateFeedbackCompleted += rateDialog_RateFeedbackCompleted;
                rateDialog.Show(this);
	


		        void rateDialog_RateFeedbackCompleted(object sender, RateFeedbackEventArgs e)
		        {
		            System.Diagnostics.Debug.WriteLine("RateFeedbackDialog Result = " + e.Result);
		        }


###iOS

###Android

###Windows Phone

##License
Licensed under MIT see License file under main repository

