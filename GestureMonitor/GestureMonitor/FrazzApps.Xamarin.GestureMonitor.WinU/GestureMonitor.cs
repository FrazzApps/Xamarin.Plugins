using FrazzApps.Xamarin.GestureMonitor.Abstractions;
using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.GestureMonitor.WinU;
using System.Windows;
using System.Collections.Generic;
using Xamarin.Forms.Platform.UAP;
using Windows.UI.Xaml;
using Xamarin.Forms.Platform.UWP;

[assembly: Dependency(typeof(GestureMonitor))]
namespace FrazzApps.Xamarin.GestureMonitor.WinU
{
    /// <summary>
    /// GestureMonitor Implementation
    /// </summary>
    public class GestureMonitor : IGestureMonitor 
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init() { }


        public event EventHandler<GestureMonitorEventArgs> GestureOccured;

        
        internal void RaiseEvent(Element element, GestureMonitorEventArgs eventArgs)
        {
                if (this.GestureOccured!=null)
                this.GestureOccured(element, eventArgs);
        }


        public void MonitorView(View view)
        {
            view.PropertyChanged += (_, propArgs) =>
            {
                // subscribe to UIElement events when the attached property Renderer is set
                if (propArgs.PropertyName == "Renderer")//Platform.RendererProperty.PropertyName)
                {
                    LayoutRenderer renderer = view.GetOrCreateRenderer() as LayoutRenderer;
                   //   renderer.Element.BackgroundColor = Color.Red;

                       renderer.Holding += Control_Hold;
                       renderer.DoubleTapped += Control_DoubleTap;
                       renderer.Tapped += Control_Tap;
                       renderer.ManipulationCompleted += Control_ManipulationCompleted;
                       renderer.ManipulationStarted += Control_ManipulationStarted;
                       renderer.ManipulationDelta += Control_ManipulationDelta;

                }
            };
        }
        

        private void Renderer_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
          //  throw new NotImplementedException();
        }

        void Control_ManipulationStarted(object sender, Windows.UI.Xaml.Input.ManipulationStartedRoutedEventArgs e)
        {
          //  e.ManipulationOrigin
        }

        void Control_ManipulationDelta(object sender, Windows.UI.Xaml.Input.ManipulationDeltaRoutedEventArgs e)
        {
            Element element = GetElement(sender);

            if (e.Cumulative.Scale != 0)
            {
                float pinch = e.Cumulative.Scale;
                System.Diagnostics.Debug.WriteLine("Control_ManipulationDelta pinch = " + pinch);
                if (pinch > 1)
                {
                    this.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.Stretch));
                    e.Handled = true;
                }
                else if (pinch < 1)
                {
                    this.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.Pinch));
                    e.Handled = true;
                }
            }
            else if (e.Delta.Translation != null)
            {                
                Windows.Foundation.Point p = e.Delta.Translation;
                if (((p.X * p.X) + (p.Y * p.Y)) > 244)
                {
                    this.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.Swiping));

                    if (p.X > 10)
                    {
                        this.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.SwipingRight));
                    }
                    if (p.X < -10)
                    {
                        this.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.SwipingLeft));
                    }
                    
                    if (p.Y > 10)
                    {
                        this.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.SwipingDown));
                    }
                    if (p.Y < -10)
                    {
                        this.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.SwipingUp));
                    }
                }
                else
                {
                    this.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.Panning));

                    if (p.X > 2)
                    {
                        this.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.PanningRight));
                    }
                    if (p.X < -2)
                    {
                        this.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.PanningLeft));
                    }

                    if (p.Y > 2)
                    {
                        this.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.PanningDown));
                    }
                    if (p.Y < -2)
                    {
                        this.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.PanningUp));
                    }
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Control_ManipulationDelta ");
            }
        }



          private void Control_ManipulationCompleted(object sender, Windows.UI.Xaml.Input.ManipulationCompletedRoutedEventArgs e)
          {
            
            if (e.Cumulative.Translation != null)
            {
                Windows.Foundation.Point p = e.Cumulative.Translation;
                if (((p.X * p.X) + (p.Y * p.Y)) > 244)
                {

                    Element element = GetElement(sender);
                    this.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.Swipe));

                    if (p.X > 10)
                    {
                        this.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.SwipeRight));
                    }
                    if (p.X < -10)
                    {
                        this.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.SwipeLeft));
                    }


                    if (p.Y> 10)
                    {
                        this.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.SwipeDown));
                    }
                    if (p.Y < -10)
                    {
                        this.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.SwipeUp));
                    }
                }
            }
        }

        void Control_Tap(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Element element = GetElement(sender);
            this.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.Tap));
        }

        void Control_DoubleTap(object sender, Windows.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
        {
            Element element = GetElement(sender);
            this.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.DoubleTap));
        }

        void Control_Hold(object sender, Windows.UI.Xaml.Input.HoldingRoutedEventArgs e)
        {
            Element element = GetElement(sender);
            this.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.Hold));
        }

        Element GetElement(object sender)
        {

            LayoutRenderer renderer = sender as LayoutRenderer;
        //    ViewRenderer<View, FrameworkElement> renderer = sender as ViewRenderer<View, FrameworkElement>;
            if (renderer == null)
            {
                FrameworkElement control = sender as FrameworkElement;
                renderer = control.Parent as LayoutRenderer;
            }

            Element element = renderer.Element;

            return element;
        }
    }
}
