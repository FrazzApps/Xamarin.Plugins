using FrazzApps.Xamarin.GestureMonitor.iOSUnified;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(View), typeof(GestureRenderer))]


namespace FrazzApps.Xamarin.GestureMonitor.iOSUnified
{
    public class GestureRenderer : global::Xamarin.Forms.Platform.iOS.ViewRenderer//, UIViewController
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

        public override void TouchesBegan(Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
        }

        public override void TouchesMoved(Foundation.NSSet touches, UIEvent evt)
        {           
          base.TouchesMoved(touches, evt);
        }

        public override void TouchesEnded(Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);
        }

        public override void TouchesCancelled(Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);
        }
    }
}
