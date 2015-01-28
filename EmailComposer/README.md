# Email Plugin for Xamarin Forms
Cross platform plugins to send emails


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

            	EmailComposer.Init();

Launching a rate app process:

                IEmailComposer emailComposer = DependencyService.Get<IEmailComposer>();
                emailComposer.SendEmail("<emailaddress>", "<subject>", "<body>");


###iOS

###Android

###Windows Phone

##License
Licensed under MIT see License file under main repository

