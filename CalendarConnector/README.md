# Calendar Plugin for Xamarin Forms
Cross platform plugins to create Calendar events


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

Note you must initialize the plugin on each platform.


###iOS
Initialization Code:

            	CalendarConnector.Init();

###Android
The first thing you need to do is add the appropriate permissions to the Android manifest.  The permissions you need to add are android.permisson.READ_CALENDAR and android.permission.WRITE_CALENDAR, depending on whether you are reading and/or writing calendar data.

Initialization Code:

            	CalendarConnector.Init(this);

###Windows Phone
Initialization Code:

            	CalendarConnector.Init();

##License
Licensed under MIT see License file under main repository

