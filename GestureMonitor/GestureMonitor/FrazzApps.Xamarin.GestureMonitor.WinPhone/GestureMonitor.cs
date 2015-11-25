using FrazzApps.Xamarin.GestureMonitor.Abstractions;
using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.GestureMonitor.WinPhone;
using System.Windows;
using Microsoft.Phone.Controls;
using System.Collections.Generic;
using Xamarin.Forms.Platform.WinPhone;

[assembly: Dependency(typeof(GestureMonitor))]
namespace FrazzApps.Xamarin.GestureMonitor.WinPhone
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

                    ViewRenderer renderer = view.GetRenderer() as ViewRenderer;
                 //   renderer.Element.BackgroundColor = Color.Red;

                    renderer.Hold += Control_Hold;
                    renderer.DoubleTap += Control_DoubleTap;
                    renderer.Tap += Control_Tap;
                    renderer.ManipulationCompleted += Control_ManipulationCompleted;
                    renderer.ManipulationStarted += Control_ManipulationStarted;
                    renderer.ManipulationDelta += Control_ManipulationDelta;
                }
            };
        }


        void Control_ManipulationStarted(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
          //  e.ManipulationOrigin
        }

        void Control_ManipulationDelta(object sender, System.Windows.Input.ManipulationDeltaEventArgs e)
        {
            Element element = GetElement(sender);

            if (e.PinchManipulation != null)
            {
                double pinch = e.PinchManipulation.CumulativeScale;
                Console.WriteLine("Control_ManipulationDelta pinch = " + pinch);
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
            else if (e.DeltaManipulation != null)
            {                
                System.Windows.Point p = e.DeltaManipulation.Translation;
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
                Console.WriteLine("Control_ManipulationDelta ");
            }
        }



        void Control_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            
            if (e.TotalManipulation != null)
            {
                System.Windows.Point p = e.TotalManipulation.Translation;
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

        void Control_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Element element = GetElement(sender);
            this.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.Tap));
        }

        void Control_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Element element = GetElement(sender);
            this.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.DoubleTap));
        }

        void Control_Hold(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Element element = GetElement(sender);
            this.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.Hold));
        }

        Element GetElement(object sender)
        {
            ViewRenderer renderer = sender as ViewRenderer;
            if (renderer == null)
            {
                FrameworkElement control = sender as FrameworkElement;
                renderer = control.Parent as ViewRenderer;
            }

            Element element = renderer.Element;

            return element;
        }
    }
}
