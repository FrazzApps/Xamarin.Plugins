using System;
using Xamarin.Forms;

namespace FrazzApps.Xamarin.GestureMonitor.Abstractions
{
    /// <summary>
    /// GestureMonitor Interface
    /// </summary>
    public interface IGestureMonitor
    {
        void MonitorView(View view);

        event EventHandler<GestureMonitorEventArgs> GestureOccured;
    }
}
