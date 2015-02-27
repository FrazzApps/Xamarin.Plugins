# Data Storage Plugin for Xamarin Forms
Cross platform plugins to use the local data store for data storage


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

            	DataStorer.Init();

Launching a rate app process:

                IDataStorer dataStorer = DependencyService.Get<IDataStorer>();
                bool successful = await dataStorer.SaveText("<filename>", "<text to sage>");
                string text = await dataStorer.LoadText("<filename>");



###iOS

###Android

###Windows Phone

##License
Licensed under MIT see License file under main repository

