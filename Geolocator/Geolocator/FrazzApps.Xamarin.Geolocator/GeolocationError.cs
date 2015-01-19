using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrazzApps.Geolocator
{    
    public enum GeolocationError
    {
        // Summary:
        //     The provider was unable to retrieve any position data.
        PositionUnavailable = 0,
        //
        // Summary:
        //     The app is not, or no longer, authorized to receive location data.
        Unauthorized = 1,
    }
}
