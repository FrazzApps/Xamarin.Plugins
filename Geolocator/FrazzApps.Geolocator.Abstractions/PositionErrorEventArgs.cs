using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrazzApps.Geolocator.Abstractions
{
    public class PositionErrorEventArgs : EventArgs
    {
        public PositionErrorEventArgs(GeolocationError error)
        { this.Error = error; }

        public GeolocationError Error { get; private set; }
    }
}
