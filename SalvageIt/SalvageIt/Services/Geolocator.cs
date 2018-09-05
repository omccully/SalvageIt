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
        }

        public Geolocator() : this(DefaultTimeout)
        {
        }

        public async Task<LocationCoordinates> GetPositionAsync()
        {
            var current_pos = await CrossGeolocator.Current.GetPositionAsync(Timeout, CancelToken, IncludeHeading);

            return new LocationCoordinates(current_pos.Latitude, current_pos.Longitude);
        }
    }
}
