using FrazzApps.Xamarin.GestureMonitor.Abstractions;
using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.GestureMonitor.WindowsPhone;
using System.Windows;
using Microsoft.Phone.Controls;

[assembly: Dependency(typeof(GestureMonitorImplementation))]
namespace FrazzApps.Xamarin.GestureMonitor.WindowsPhone
{
    /// <summary>
    /// GestureMonitor Implementation
    /// </summary>
    public class GestureMonitorImplementation : IGestureMonitor
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init() { }

        public void MonitorElement(Element element)
        {
            //System.Windows.Controls.Image img = new System.Windows.Controls.Image();
            //img.
            //var gl = GestureService.GetGestureListener(img);
            //gl.DoubleTap += new EventHandler<GestureEventArgs>(GestureListenerDoubleTap);

            //GestureListener gl = new GestureListener();
            //gl.

            // Windows.UI.Input.GestureRecognizer grTap = new  Windows.UI.Input.GestureRecognizer();
            //grTap.GestureSettings = Windows.UI.Input.GestureSettings.DoubleTap | Windows.UI.Input.GestureSettings.Hold | Windows.UI.Input.GestureSettings.CrossSlide;
            System.Windows.Controls.TextBlock el = new System.Windows.Controls.TextBlock();
            el.ManipulationCompleted
            //Label elbn = new Label();
            //elbn.
            //UIElement elem = el as UIElement;
            //GestureService gs = new GestureService();
            //e
            
        }
    }
}
