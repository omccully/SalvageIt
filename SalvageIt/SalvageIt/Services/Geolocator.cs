using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SalvageIt.Models;
using System.Threading;

using CrossGeolocator = Plugin.Geolocator.CrossGeolocator;
using Xamarin.Forms;
using SalvageIt.Services;

[assembly: Dependency(typeof(Geolocator))]
namespace SalvageIt.Services
{
    public class Geolocator : IGeolocator
    {
        static TimeSpan DefaultTimeout = TimeSpan.FromSeconds(10);

        TimeSpan? Timeout;
        CancellationToken? CancelToken;
        bool IncludeHeading;

        public Geolocator(TimeSpan? timeout, CancellationToken? cancel_token = null, bool include_heading = false)
        {
            this.Timeout = timeout ?? DefaultTimeout;
            this.CancelToken = cancel_token;
            this.IncludeHeading = include_heading;
            InitializeLastKnownLocation();
        }

        public Geolocator() : this(DefaultTimeout)
        {
        }

        object last_known_location_lock = new object();
        volatile LocationCoordinates _LastKnownLocation = null;
        public LocationCoordinates LastKnownLocation {
            get
            {
                lock (last_known_location_lock)
                {
                    return _LastKnownLocation;
                }
            }
            private set
            {
                lock (last_known_location_lock)
                {
                    _LastKnownLocation = value;
                }
            }
        }

        public async Task<LocationCoordinates> GetPositionAsync()
        {
            var current_pos = await CrossGeolocator.Current.GetPositionAsync(Timeout, CancelToken, IncludeHeading);

            var pos = new LocationCoordinates(current_pos.Latitude, current_pos.Longitude);
            LastKnownLocation = pos;

            // we return pos the variable instead of LastKnownLocation, 
            // since LastKnownLocation could possibly be changed 
            // since the last set
            return pos;
        }

        async void InitializeLastKnownLocation()
        {
            var last_pos = await CrossGeolocator.Current.GetLastKnownLocationAsync();

            LastKnownLocation = new LocationCoordinates(last_pos.Latitude, last_pos.Longitude);
        }
    }
}
