using FrazzApps.Xamarin.GestureMonitor.Abstractions;
using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.GestureMonitor.iOSUnified;
using System.Collections.Generic;
using Xamarin.Forms.Platform.iOS;

[assembly: Dependency(typeof(GestureMonitor))]
namespace FrazzApps.Xamarin.GestureMonitor.iOSUnified
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
            if (this.GestureOccured != null)
                this.GestureOccured(element, eventArgs);
        }


        public void MonitorView(View view)
        {
            view.PropertyChanged += (_, propArgs) =>
            {
                // subscribe to UIElement events when the attached property Renderer is set
                if (propArgs.PropertyName == "Platform.RendererProperty.PropertyName")
                {
                    GestureRenderer renderer = RendererFactory.GetRenderer(view) as GestureRenderer;
                    if (renderer != null)
                        renderer.SetMonitor(this);
                }
            };
        }
    }
}
