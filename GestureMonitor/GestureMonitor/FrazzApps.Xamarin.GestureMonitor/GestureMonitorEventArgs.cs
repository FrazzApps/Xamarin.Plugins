using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FrazzApps.Xamarin.GestureMonitor
{
    public class GestureMonitorEventArgs : EventArgs
    {

        public GestureMonitorEventArgs(Element element, GestureType type)
		{ this.Element = element; this.GestureType = type; this.Handled = false; }

        public Element Element { get; private set; }
		public GestureType GestureType { get; private set; }
		public Boolean Handled { get; set; }
    }

    public enum GestureType
    {
        Tap,
        DoubleTap,
		Swipe,
		SwipeUp,
		SwipeDown,
		SwipeLeft,
        SwipeRight,
        Swiping,
        SwipingUp,
        SwipingDown,
        SwipingLeft,
        SwipingRight,
		Pinch,
		Pan,
		PanUp,
		PanDown,
		PanLeft,
        PanRight,
        Panning,
        PanningUp,
        PanningDown,
        PanningLeft,
        PanningRight,
		Stretch,
        Hold
    }
}
