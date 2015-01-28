using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FrazzApps.Xamarin.Geolocator.Abstractions
{
    public interface IGeolocator
    {
        double DesiredAccuracy { get; set; }
        bool IsGeolocationEnabled { get; }
        bool IsListening { get; }
        bool SupportsHeading { get; }

        event EventHandler<PositionEventArgs> PositionChanged;
        event EventHandler<PositionErrorEventArgs> PositionError;

        Task<Position> GetPositionAsync(bool includeHeading);
        //
        // Summary:
        //     Start listening to location changes
        //
        // Parameters:
        //   minTime:
        //     Minimum interval in milliseconds
        //
        //   minDistance:
        //     Minimum distance in meters
        void StartListening(uint minTime, double minDistance);
        //
        // Summary:
        //     Start listening to location changes
        //
        // Parameters:
        //   minTime:
        //     Minimum interval in milliseconds
        //
        //   minDistance:
        //     Minimum distance in meters
        //
        //   includeHeading:
        //     Include heading information
        void StartListening(uint minTime, double minDistance, bool includeHeading);
        //
        // Summary:
        //     Stop listening to location changes
        void StopListening();
    }
}
