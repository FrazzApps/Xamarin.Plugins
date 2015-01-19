[assembly: Xamarin.Forms.Dependency(typeof(FrazzApps.Xamarin.Geolocator.iOSUnified.Geolocator_iOSUnified))]
namespace FrazzApps.Xamarin.Geolocator.iOSUnified
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using FrazzApps.Geolocator.Abstractions;
    using CoreLocation;
    using FrazzApps.Geolocator;

    public class Geolocator_iOSUnified : IGeolocator
    {
        public double DesiredAccuracy { get; set; }
        public bool IsGeolocationEnabled { get; private set; }
        public bool IsListening { get; private set; }
        public bool SupportsHeading { get; private set; }

        public event EventHandler<PositionEventArgs> PositionChanged;
        public event EventHandler<PositionErrorEventArgs> PositionError;


        internal Position ToPosition(CLLocation location)
        {
            DateTime time = (new DateTime(2001, 1, 1, 0, 0, 0)).AddSeconds(location.Timestamp.SecondsSinceReferenceDate);
            //TODO Review -> DateTime time = DateTime.SpecifyKind(location.Timestamp, DateTimeKind.Unspecified);
            return new Position()
            {
                Accuracy = location.HorizontalAccuracy,
                Altitude = location.Altitude,
                AltitudeAccuracy = location.VerticalAccuracy,
                Heading = location.Course,
                Latitude = location.Coordinate.Latitude,
                Longitude = location.Coordinate.Longitude,
                Speed = location.Speed,
                Timestamp = time
            };
        }


        public async Task<Position> GetPositionAsync(bool includeHeading)
        {
            if (CLLocationManager.LocationServicesEnabled)
            {
                if (_locationManager == null)
                {
                    InitializeLocationManager();
                }

                this._newLocationRequestAvailable = false;
                _locationManager.StartUpdatingLocation();
                if ((_includeHeading) && (CLLocationManager.HeadingAvailable))
                    _locationManager.StartUpdatingHeading();
                while (!this._newLocationRequestAvailable)
                {
                    await Task.Delay(10);
                }
                if (!this.IsListening)
                {
                    _locationManager.StopUpdatingLocation();
                    if ((_includeHeading) && (CLLocationManager.HeadingAvailable))
                    {
                        _locationManager.StopUpdatingHeading();
                    }
                }

                var location = _locationManager.Location;
                if (location != null)
                {
                    if ((includeHeading) && (CLLocationManager.HeadingAvailable))
                    {
                        var heading = _locationManager.Heading;
                       //TODO -> location.Course = heading.TrueHeading;
                    }
                    return ToPosition(location);
                }

                return null;

            }
            return null;

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
        {
            StartListening(minTime, minDistance, false);
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
        //
        //   includeHeading:
        //     Include heading information
        public void StartListening(uint minTime, double minDistance, bool includeHeading)
        {
            if (!this.IsListening)
            {
                this._minTime = minTime;
                this._includeHeading = includeHeading;

                if (CLLocationManager.LocationServicesEnabled)
                {
                    if (_locationManager == null)
                    {
                        InitializeLocationManager();
                    }

                    _locationManager.DesiredAccuracy = CLLocation.AccuracyNearestTenMeters;
                    _locationManager.DistanceFilter = minDistance;



                    _locationManager.StartUpdatingLocation();
                    if ((_includeHeading) && (CLLocationManager.HeadingAvailable))
                        _locationManager.StartUpdatingHeading();


                    this.IsListening = true;
                }
                else
                {
                    Console.WriteLine("Location services not enabled");
                    this.IsListening = false;
                }

            }
        }

        private void _locationManager_MonitoringFailed(object sender, CLRegionErrorEventArgs e)
        {
            this.PositionError(sender, new PositionErrorEventArgs(GeolocationError.PositionUnavailable));
        }

        void _locationManager_Failed(object sender, Foundation.NSErrorEventArgs e)
        {
            this.PositionError(sender, new PositionErrorEventArgs(GeolocationError.Unauthorized));
        }
        //
        // Summary:
        //     Stop listening to location changes
        public void StopListening()
        {
            _locationManager.StopUpdatingLocation();
            if ((_includeHeading) && (CLLocationManager.HeadingAvailable))
            {
                _locationManager.StopUpdatingHeading();
            }

            this.IsListening = true;
        }


        async void _locationManager_LocationsUpdated(object sender, CLLocationsUpdatedEventArgs e)
        {
            _newLocationRequestAvailable = true;
            if (this.IsListening)
            {
                if ((e.Locations != null) && (e.Locations.Length > 0))
                {
                    var location = e.Locations[e.Locations.Length - 1];
                    Position position = ToPosition(location);
                    this.PositionChanged(sender, new PositionEventArgs(position));
                }

                _locationManager.StopUpdatingLocation();
                if ((_includeHeading) && (CLLocationManager.HeadingAvailable))
                    _locationManager.StopUpdatingHeading();

                await Task.Delay((int)_minTime);

                _locationManager.StartUpdatingLocation();
                if (_includeHeading)
                    _locationManager.StartUpdatingHeading();
            }
        }

        private bool _newLocationRequestAvailable;
        private uint _minTime;
        private bool _includeHeading;
        protected CLLocationManager _locationManager;

        private void InitializeLocationManager()
        {
            _locationManager = new CLLocationManager();
            _locationManager.LocationsUpdated += _locationManager_LocationsUpdated;
            _locationManager.Failed += _locationManager_Failed;
            _locationManager.MonitoringFailed += _locationManager_MonitoringFailed;
        }
    }
}
