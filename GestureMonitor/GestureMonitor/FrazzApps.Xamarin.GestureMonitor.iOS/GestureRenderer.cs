using FrazzApps.Xamarin.GestureMonitor.iOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(View), typeof(GestureRenderer))]


namespace FrazzApps.Xamarin.GestureMonitor.iOS
{
    public class GestureRenderer : global::Xamarin.Forms.Platform.iOS.ViewRenderer
    {
        private GestureMonitor GestureMonitor;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);


        }

        public void SetMonitor(GestureMonitor monitor)
        {
            GestureMonitor = monitor;
        }
    }
}
