# Icon Generator Plugin for Xamarin Forms
Cross platform plugins to display [Font-Awesome](http://fortawesome.github.io/Font-Awesome/ "Font-Awesome") icons 


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

    new Icon();

For valid icons please refer to: [http://fortawesome.github.io/Font-Awesome/cheatsheet/](http://fortawesome.github.io/Font-Awesome/cheatsheet/  )  

###Configuration
####Windows Phone 8
*fontawesome-webfont.tff* is added to a Assets/Fonts/ folder, the ***Build Action*** must be changed to *Content* and ***Copy to Output Directory*** should be changed to 
*Copy if newer*


##License
Licensed under MIT see License file under main repository
