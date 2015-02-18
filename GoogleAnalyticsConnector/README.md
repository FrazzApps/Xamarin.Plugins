# Google Analytics Plugin for Xamarin Forms
Cross platform plugins to connect to Google Analytics 


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

    GoogleAnalytics ga = new GoogleAnalytics(
							    <trackingID>,
							    <appName>,
							    <appVersion>,
							    <appId>,
							    <appInstallerId>);
    
    ga.TrackPage(<userId>, <pageName);
    ga.TrackException(<userId>, <exception>, <isFatal>);
    ga.TrackScreen(<userId>, <screenName>);
    ga.TrackEvent(<userId>, <category>, <action>);


##License
Licensed under MIT see License file under main repository
