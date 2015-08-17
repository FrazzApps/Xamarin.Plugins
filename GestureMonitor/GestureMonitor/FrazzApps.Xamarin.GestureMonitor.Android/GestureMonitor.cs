using FrazzApps.Xamarin.GestureMonitor.Abstractions;
using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.GestureMonitor.Android;
using Android.Views;
using System.Collections.Generic;
using Xamarin.Forms.Platform.Android;
using Android.App;
using System.Runtime;

[assembly: Dependency(typeof(GestureMonitor))]
namespace FrazzApps.Xamarin.GestureMonitor.Android
{
    /// <summary>
    /// GestureMonitor Implementation
    /// </summary>
    public class GestureMonitor : Java.Lang.Object, IGestureMonitor, global::Android.Views.View.IOnTouchListener, global::Android.Views.View.IOnLongClickListener, global::Android.Views.View.IOnDragListener
    {
        private static GestureListener _listener;
        private static GestureDetector _detector;

        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init()
        {
            _listener = new GestureListener();
            _detector = new GestureDetector(_listener);
        }


        public event EventHandler<GestureMonitorEventArgs> GestureOccured;


        internal void RaiseEvent(Element element, GestureMonitorEventArgs eventArgs)
        {
            if (this.GestureOccured != null)
                this.GestureOccured(element, eventArgs);
        }


        public void MonitorView(global::Xamarin.Forms.View view)
		{
			try{
            view.PropertyChanged += (_, propArgs) =>
				{
					try{
                // subscribe to UIElement events when the attached property Renderer is set
                if (propArgs.PropertyName == "Renderer")
                {
							
							VisualElementRenderer<global::Xamarin.Forms.View> renderer = ViewExtensions.GetRenderer(view) as VisualElementRenderer<global::Xamarin.Forms.View>;
                    if (renderer != null)
                    {
                      //  renderer.SetOnTouchListener(this);
                                     renderer.Touch += renderer_Touch;
                       
                    }
              
                      }
					}catch(Exception ex)
					{
						string t = ex.Message;
					}
				};
			}catch(Exception ex)
			{
				string t = ex.Message;
			}
        }

		private bool _singleTouch;
		private float _downX;
		private float _downY;
		private float _downX2;
		private float _downY2;
        private MotionEventActions _lastState;

        
        private void HandleTouch(Element element, MotionEvent e)
        {
            Console.WriteLine("renderer_Touch Down pointCount = " + e.PointerCount + "[" + e.Action + "]");

            double time = e.EventTime - e.DownTime;
            double movement = CalculateDistanceSquared(_downX, _downY, e.GetX(0), e.GetY(0));
            int pointCount = e.PointerCount;

            switch (e.Action)
            {
                case MotionEventActions.Down:
                    _downX = e.GetX(0);
                    _downY = e.GetY(0);
                    _singleTouch = true;
                    break;
                case MotionEventActions.Up:

                    if (((_lastState == MotionEventActions.Down) || (_lastState == MotionEventActions.Move)) && _singleTouch)
                    {

                        Console.WriteLine("renderer_Touch Up Time = " + time + ", Movement = " + movement + ", PointCount =" + pointCount);

                        if ((time <= 200) && (movement <= 1936))
                        {
                            this.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.Tap));
                        }
                        else if ((time > 200) && (movement < 1936))
                        {
                            this.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.Hold));
                        }
                        else if ((time < 400) && (movement >= 1936))
                        {
                            this.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.Swipe));

                            if (e.GetX(0) - _downX > 300)
                            {
                                this.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.SwipeRight));
                            }

                            if (e.GetX(0) - _downX < -300)
                            {
                                this.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.SwipeLeft));
                            }

                            if (e.GetY(0) - _downY > 300)
                            {
                                this.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.SwipeDown));
                            }

                            if (e.GetY(0) - _downY < -300)
                            {
                                this.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.SwipeUp));
                            }
                        }
                    }
                    break;
                case MotionEventActions.Pointer2Down:
                    _singleTouch = false;
                    _downX2 = e.GetX(1);
                    _downY2 = e.GetY(1);
                    break;
                case MotionEventActions.Pointer2Up:
                    _singleTouch = false;
                    double startDistance = CalculateDistanceSquared(_downX, _downY, _downX2, _downY2);
                    double endDistance = CalculateDistanceSquared(e.GetX(0), e.GetY(0), e.GetX(1), e.GetY(1));

                    if (startDistance > endDistance)
                    {
                        this.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.Pinch));
                    }
                    else
                    {
                        this.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.Stretch));
                    }
                    break;
                case MotionEventActions.Move:

                    if (_singleTouch)
                    {

                        if ((time > 400) && (movement >= 1936))
                        {
                            this.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.Pan));

                            if (e.GetX(0) - _downX > 200)
                            {
                                this.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.PanRight));
                            }

                            if (e.GetX(0) - _downX < -200)
                            {
                                this.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.PanLeft));
                            }

                            if (e.GetY(0) - _downY > 200)
                            {
                                this.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.PanDown));
                            }

                            if (e.GetY(0) - _downY < -200)
                            {
                                this.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.PanUp));
                            }
                        }

                    }
                    break;
            }
            _lastState = e.Action;
        }

		private double CalculateDistanceSquared(float x1, float y1, float x2, float y2)
		{
			return Math.Pow((x1 - x2),2) + Math.Pow((y1 - y2),2);
		}

        void renderer_Touch(object sender, global::Android.Views.View.TouchEventArgs e)
        {
                            VisualElementRenderer<global::Xamarin.Forms.View> renderer = sender as VisualElementRenderer<global::Xamarin.Forms.View>;
                            Element element = renderer.Element;
                            HandleTouch(element, e.Event);
                            e.Handled = true;
        }

		public bool OnTouch(global::Android.Views.View v, MotionEvent e)
        {            
            HandleTouch(null, e);
            return true;
        }
        

        public bool OnLongClick(global::Android.Views.View v)
        {
            return true;
        }

        public bool OnDrag(global::Android.Views.View v, DragEvent e)
        {
            return true;
        }


    }

	public static class ViewExtensions
	{
		private static readonly Lazy<BindableProperty> RendererPropertyLazy = new Lazy<BindableProperty>(
			() =>
			{
				var assembly = typeof(EntryRenderer).Assembly;
				// TODO: hack
				Type platformType = assembly.GetType("Xamarin.Forms.Platform.Android.Platform");
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
