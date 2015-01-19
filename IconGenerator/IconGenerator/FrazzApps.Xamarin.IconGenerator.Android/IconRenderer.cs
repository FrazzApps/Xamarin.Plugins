using FrazzApps.Xamarin.IconGenerator;
using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Widget;
using Android.Graphics;
using Xamarin.Forms;
using FrazzApps.Xamarin.IconGenerator.Android;

[assembly: Xamarin.Forms.ExportRenderer(typeof(Icon), typeof(IconRenderer_Android))]
namespace FrazzApps.Xamarin.IconGenerator.Android
{
    class IconRenderer_Android : LabelRenderer
    {
		protected override void OnElementChanged (ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged (e);

			var label = (TextView)Control; // for example
			Typeface font = Typeface.CreateFromAsset (Forms.Context.Assets, "fontawesome-webfont.ttf");
			label.Typeface = font;
		}
    }
}


