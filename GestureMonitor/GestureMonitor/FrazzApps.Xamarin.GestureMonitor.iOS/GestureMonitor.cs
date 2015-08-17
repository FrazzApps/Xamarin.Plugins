using FrazzApps.Xamarin.GestureMonitor.Abstractions;
using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.GestureMonitor.iOS;
using System.Collections.Generic;
using MonoTouch.ObjCRuntime;
using Xamarin.Forms.Platform.iOS;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

[assembly: Dependency(typeof(GestureMonitor))]
namespace FrazzApps.Xamarin.GestureMonitor.iOS
{
    /// <summary>
    /// GestureMonitor Implementation
    /// </summary>
    public class GestureMonitor : UIViewController, IGestureMonitor
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



        public void MonitorView(global::Xamarin.Forms.View view)
        {
            try
            {
                view.PropertyChanged += (_, propArgs) =>
                {
                    try
                    {
                        // subscribe to UIElement events when the attached property Renderer is set
                        if (propArgs.PropertyName == "Renderer")
                        {

                            VisualElementRenderer<global::Xamarin.Forms.View> renderer = ViewExtensions.GetRenderer(view) as VisualElementRenderer<global::Xamarin.Forms.View>;
                            if (renderer != null)
                            {
                                //  renderer.SetOnTouchListener(this);
                                renderer.MultipleTouchEnabled = true;
                                renderer.UserInteractionEnabled = true;

                                renderer.ExclusiveTouch = false;
                                

                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        string t = ex.Message;
                    }
                };
            }
            catch (Exception ex)
            {
                string t = ex.Message;
            }
        }
        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            foreach (UITouch touch in touches.ToArray<UITouch>())
            {
                Console.WriteLine(touch);
            }
        }

    }




    public static class ViewExtensions
    {
        private static readonly Lazy<BindableProperty> RendererPropertyLazy = new Lazy<BindableProperty>(
            () =>
            {
                var assembly = typeof(EntryRenderer).Assembly;
                // TODO: hack
                Type platformType = assembly.GetType("Xamarin.Forms.Platform.iOS.Platform");
                var rendererProperty = (BindableProperty)platformType.GetField("RendererProperty").GetValue(null);
                //
                return rendererProperty;
            });
        //
        public static IVisualElementRenderer GetRenderer(this global::Xamarin.Forms.View view)
        {
            return (IVisualElementRenderer)view.GetValue(RendererPropertyLazy.Value);
        }
    }
}
