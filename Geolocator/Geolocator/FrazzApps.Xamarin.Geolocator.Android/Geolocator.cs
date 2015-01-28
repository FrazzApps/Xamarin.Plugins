using Android.App;
using Android.Locations;
using Android.OS;
using Android.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FrazzApps.Xamarin.Geolocator.Abstractions;
using FrazzApps.Xamarin.Geolocator;
using Xamarin.Forms;
using FrazzApps.Xamarin.Geolocator.Android;

[assembly: Dependency(typeof(FrazzApps.Xamarin.Geolocator.Android.Geolocator))]
namespace FrazzApps.Xamarin.Geolocator.Android
{


    public class Geolocator : Activity, IGeolocator, ILocationListener
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init() { }
        public Geolocator() { }

        public double DesiredAccuracy { get; set; }
        public bool IsGeolocationEnabled { get { return (!String.IsNullOrWhiteSpace(_locationProvider)); } }
        public bool IsListening { get; private set; }
        public bool SupportsHeading { get; private set; }

        public event EventHandler<PositionEventArgs> PositionChanged;
        public event EventHandler<PositionErrorEventArgs> PositionError;


        internal Position ToPosition(Location location)
        {
            return new Position()
            {
                Accuracy = location.Accuracy,
                Altitude = location.Altitude,
                AltitudeAccuracy = null, // TODO confirm it is not supported on Andriod
                Heading = (location.HasBearing) ? (double?)location.Bearing : null,
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                Speed = location.Speed,
                Timestamp = new DateTimeOffset(1970,1,1,0,0,0, new TimeSpan(0,0,0,0, (int)location.Time))
            };
        }


        #region Get Position Calls

        public async Task<Position> GetPositionAsync(bool includeHeading)
        {
                if (_locationManager == null)
                    InitializeLocationManager();

                if (this._locationManager != null)
                {
                    Criteria locationCriteria = new Criteria();

                    locationCriteria.Accuracy = Accuracy.Coarse;
                    locationCriteria.PowerRequirement = Power.Medium;
                    locationCriteria.BearingRequired = includeHeading;

                    this._locationProvider = this._locationManager.GetBestProvider(locationCriteria, true);


                    if (this._locationProvider != null)
                    {
                        this._newLocationRequestAvailable = false;
                        this._locationManager.RequestSingleUpdate(this._locationProvider, this, null);
                        while (!this._newLocationRequestAvailable)
                        {
                           await Task.Delay(10);
                        }

                        return ToPosition(this._currentLocation);
                    }
                    else
                    {
                        Log.Info("Geolocator", "No location providers available");
                    }
                }
            return null;
        }

        #endregion

        #region Listener Calls

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
                this._minDistance = minDistance;
                this._includeHeading = includeHeading;

                if ((!this.SupportsHeading) && (includeHeading))
                {
                    //TODO: let them know you aren't going to return a heading...
                    includeHeading = false;
                }

                if (_locationManager == null)
                    InitializeLocationManager();

                if (_locationManager != null)
                {
                    Criteria locationCriteria = new Criteria();

                    locationCriteria.Accuracy = Accuracy.Coarse;
                    locationCriteria.PowerRequirement = Power.Medium;
                    locationCriteria.BearingRequired = includeHeading;

                    _locationProvider = _locationManager.GetBestProvider(locationCriteria, true);

                    if (_locationProvider != null)
                    {
                        _locationManager.RequestLocationUpdates(_locationProvider, (long)minTime, (float)minDistance, this);
                    }
                    else
                    {
                        Log.Info("Geolocator", "No location providers available");
                    }

                    this.IsListening = true;
                }
                else
                {
                    //TODO: let them know you aren't listening...
                    this.IsListening = false;
                }
            }
        }
        //
        // Summary:
        //     Stop listening to location changes
        public void StopListening()
        {
            if (_locationManager != null)
                _locationManager.RemoveUpdates(this);

            this.IsListening = false;
        }

        protected override void OnCreate(global::Android.OS.Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            InitializeLocationManager();
        }
        protected override void OnPause()
        {
            base.OnPause();

            StopListening();
        }

        public void OnLocationChanged(Location location) 
        {
            _currentLocation = location;
            _newLocationRequestAvailable = true;
        }

        public void OnProviderDisabled(string provider) 
        {
            if (this._locationProvider.Equals(provider))
            {
                if (this.IsListening)
                {
                    StopListening();
                    StartListening(_minTime, _minDistance, _includeHeading);
                }
            }
        }

        public void OnProviderEnabled(string provider)
        {
            if (this._locationProvider.Equals(provider))
            {
                if (this.IsListening)
                {
                    StopListening();
                    StartListening(_minTime, _minDistance, _includeHeading);
                }
            }
        }

        public void OnStatusChanged(string provider, Availability status, Bundle extras) { }

        #endregion

        uint _minTime; double _minDistance; bool _includeHeading;
        

        Location _currentLocation;
        String _locationProvider;
        LocationManager _locationManager;
        Boolean _newLocationRequestAvailable;

        private void InitializeLocationManager()
        {
            _locationManager = (LocationManager)GetSystemService(LocationService);

            Criteria criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.Fine
            };
            IList<string> acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);

            if (acceptableLocationProviders.Count > 0)
            {
                _locationProvider = acceptableLocationProviders[0];
            }
            else
            {
                _locationProvider = String.Empty;
            }
            
        }


    }
}
