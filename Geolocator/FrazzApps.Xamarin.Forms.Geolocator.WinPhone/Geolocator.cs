[assembly: Xamarin.Forms.Dependency(typeof(FrazzApps.Xamarin.Forms.Geolocator.WinPhone.Geolocator_WinPhone))]
namespace FrazzApps.Xamarin.Forms.Geolocator.WinPhone
{
    using FrazzApps.Geolocator.Abstractions;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Windows.Devices.Geolocation;

    public class Geolocator_WinPhone : IGeolocator
    {
        Windows.Devices.Geolocation.Geolocator geolocator = new Windows.Devices.Geolocation.Geolocator();

        public double DesiredAccuracy { get { return (geolocator.DesiredAccuracyInMeters.HasValue) ? geolocator.DesiredAccuracyInMeters.Value : double.NaN; } set { geolocator.DesiredAccuracyInMeters = (uint)value; } }
        public bool IsGeolocationEnabled
        {
            get
            {
                if (geolocator == null) return false;
                switch (geolocator.LocationStatus)
                {
                    case PositionStatus.Disabled:
                    case PositionStatus.Initializing:
                    case PositionStatus.NoData:
                    case PositionStatus.NotAvailable:
                    case PositionStatus.NotInitialized:
                        return false;
                        break;
                    case PositionStatus.Ready:
                        return true;
                        break;
                }
                return false;
            }
        }
        public bool IsListening { get; private set; }
        public bool SupportsHeading { get; private set; } //TODO

        public event EventHandler<PositionEventArgs> PositionChanged;
        public event EventHandler<PositionErrorEventArgs> PositionError;

        public async Task<Position> GetPositionAsync(bool includeHeading)
        {
            var position = await geolocator.GetGeopositionAsync();
            
            return new Position()
            {
                Accuracy = position.Coordinate.Accuracy,
                Altitude = position.Coordinate.Altitude,
                AltitudeAccuracy = position.Coordinate.AltitudeAccuracy,
                Heading = (includeHeading) ? position.Coordinate.Heading : null,
                Latitude = position.Coordinate.Latitude,
                Longitude = position.Coordinate.Longitude,
                Speed = position.Coordinate.Speed,
                Timestamp = position.Coordinate.Timestamp
            };
        }


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
        public void StartListening(uint minTime, double minDistance)
        { StartListening(minTime, minDistance, true); }
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
        public void StartListening(uint minTime, double minDistance, bool includeHeading)
        {
            if (!this.IsListening) {
                this.IsListening = true;

                geolocator.ReportInterval = minTime;
                geolocator.MovementThreshold = minDistance;

                geolocator.PositionChanged += geolocator_PositionChanged;
                geolocator.StatusChanged += geolocator_StatusChanged;
            }

        }

        void geolocator_StatusChanged(Windows.Devices.Geolocation.Geolocator sender, StatusChangedEventArgs args)
        {
            if (this.IsListening)
            {
                switch(args.Status)
                {
                    case PositionStatus.Disabled:
                    case PositionStatus.Initializing:
                        break;
                    case PositionStatus.NoData:
                    case PositionStatus.NotAvailable:
                        this.PositionError(sender, new PositionErrorEventArgs(GeolocationError.PositionUnavailable));
                        break;
                    case PositionStatus.NotInitialized:
                        this.PositionError(sender, new PositionErrorEventArgs(GeolocationError.Unauthorized));
                        break;
                    case PositionStatus.Ready:
                        break;
                }                            
            }
        }

        void geolocator_PositionChanged(Windows.Devices.Geolocation.Geolocator sender, PositionChangedEventArgs args)
        {
            if (this.IsListening)
            {
                var position = args.Position;
                Position p = new Position()
                {
                    Accuracy = position.Coordinate.Accuracy,
                    Altitude = position.Coordinate.Altitude,
                    AltitudeAccuracy = position.Coordinate.AltitudeAccuracy,
                    Heading = position.Coordinate.Heading,
                    Latitude = position.Coordinate.Latitude,
                    Longitude = position.Coordinate.Longitude,
                    Speed = position.Coordinate.Speed,
                    Timestamp = position.Coordinate.Timestamp
                };
                this.PositionChanged(sender, new PositionEventArgs(p));
            }
        }
        //
        // Summary:
        //     Stop listening to location changes
        public void StopListening()
        {
            this.IsListening = false;
            geolocator.PositionChanged -= geolocator_PositionChanged;
        }
    }
}
