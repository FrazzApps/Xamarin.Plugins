# Application Informer Plugin for Xamarin Forms
Cross platform plugins to allow users to easily get information about your app [...a licky boom-boom down...].


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

            	AppInformer.Init();

Methods available:

        string AppName {get;}
        string AppVersion { get; }
        string AppId { get; }
        string AppInstallerId { get; }



###iOS

###Android

###Windows Phone

##License
Licensed under MIT see License file under main repository

