using FrazzApps.Xamarin.GestureMonitor.WinPhone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;

[assembly: ExportRenderer(typeof(View), typeof(GestureRenderer))]


namespace FrazzApps.Xamarin.GestureMonitor.WinPhone
{
    public class GestureRenderer : global::Xamarin.Forms.Platform.WinPhone.ViewRenderer
    {
        private GestureMonitor GestureMonitor;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);


            if (Control == null)
            {
                if(e.NewElement.GetType() == typeof(StackLayout))
                    SetNativeControl(new System.Windows.Controls.StackPanel());
                else if (e.NewElement.GetType() == typeof(Grid))
                    SetNativeControl(new System.Windows.Controls.Grid());
            }

            if (Control != null)
                        {
                            //var stack = Control as System.Windows.Controls.StackPanel;
                            //if (stack!=null)
                            //    stack.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
                            //var grid = Control as System.Windows.Controls.Grid;
                            //if (grid != null)
                            //    grid.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Blue);
                Control.Hold += Control_Hold;
                Control.DoubleTap += Control_DoubleTap;
                Control.Tap += Control_Tap;
                Control.ManipulationCompleted += Control_ManipulationCompleted;
            }
        }

        public void SetMonitor(GestureMonitor monitor)
        {
            GestureMonitor = monitor;
        }

        void Control_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            if (e.IsInertial)
            {
                FrameworkElement control = sender as FrameworkElement;
                GestureRenderer renderer = control.Parent as GestureRenderer;
                Element element = renderer.Element;
                Console.WriteLine("Control_ManipulationCompleted [" + control.Name + "] " + e.IsInertial);
                if (GestureMonitor!=null)
                GestureMonitor.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.Swipe));
            }
        }

        void Control_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            FrameworkElement control = sender as FrameworkElement;
            GestureRenderer renderer = control.Parent as GestureRenderer;
            Element element = renderer.Element;
            Console.WriteLine("Control_Tap [" + control.Name + "] " + e.GetPosition(control).ToString());
            if (GestureMonitor != null)
                GestureMonitor.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.Tap));
        }

        void Control_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            FrameworkElement control = sender as FrameworkElement;
            GestureRenderer renderer = control.Parent as GestureRenderer;
            Element element = renderer.Element;
            Console.WriteLine("Control_DoubleTap [" + control.Name + "] " + e.GetPosition(control).ToString());
            if (GestureMonitor != null)
                GestureMonitor.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.DoubleTap));
        }

        void Control_Hold(object sender, System.Windows.Input.GestureEventArgs e)
        {
            FrameworkElement control = sender as FrameworkElement;
            GestureRenderer renderer = control.Parent as GestureRenderer;
            Element element = renderer.Element;
            Console.WriteLine("Control_Hold [" + control.Name + "] " + e.GetPosition(control).ToString());
            if (GestureMonitor != null)
                GestureMonitor.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.Hold));
        }
    }
}
