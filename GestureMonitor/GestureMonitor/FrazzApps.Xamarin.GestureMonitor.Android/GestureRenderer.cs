using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using FrazzApps.Xamarin.GestureMonitor.Android;

[assembly: ExportRenderer(typeof(global::Xamarin.Forms.View), typeof(GestureRenderer))]

namespace FrazzApps.Xamarin.GestureMonitor.Android
{
    public class GestureRenderer : global::Xamarin.Forms.Platform.Android.ViewRenderer
    {
        //////private GestureMonitor GestureMonitor;
        //////private readonly GestureListener _listener;
        //////private readonly GestureDetector _detector;

        //////public GestureRenderer()
        //////{
        //////    _listener = new GestureListener ();
        //////    _detector = new GestureDetector (_listener);

        //////}

        //////public void SetMonitor(GestureMonitor monitor)
        //////{
        //////    GestureMonitor = monitor;
        //////}

        protected override void OnElementChanged (ElementChangedEventArgs<global::Xamarin.Forms.View> e)
        {
            base.OnElementChanged (e);

            ////if (Control != null) {
            ////    // do whatever you want to the textField here!
            ////    Control.SetBackgroundColor(global::Android.Graphics.Color.DarkGray);
            ////}
            //////if (e.NewElement == null) {
            //////    if (this.GenericMotion != null) {
            //////        this.GenericMotion -= HandleGenericMotion;
            //////    }
            //////    if (this.Touch != null) {
            //////        this.Touch -= HandleTouch;
            //////    }
            //////}

            //////if (e.OldElement == null) {
            //////    this.GenericMotion += HandleGenericMotion;
            //////    this.Touch += HandleTouch;
            //////}
        }

        //////void HandleTouch (object sender, TouchEventArgs e)
        //////{
        //////    _detector.OnTouchEvent(e.Event);


        //////    //FrameworkElement control = sender as FrameworkElement;
        //////    //GestureRenderer renderer = control.Parent as GestureRenderer;
        //////    //Element element = renderer.Element;

        //////    //if (GestureMonitor != null)
        //////    //    GestureMonitor.RaiseEvent(element, new GestureMonitorEventArgs(element, GestureType.Tap));
        //////}

        //////void HandleGenericMotion (object sender, GenericMotionEventArgs e)
        //////{
        //////    _detector.OnTouchEvent (e.Event);
        //////}
     
    }
}