using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace SalvageIt
{
    using Services;
    using Models;

    class LocationToDistanceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            LocationCoordinates coords = (LocationCoordinates)value;

            IGeolocator geoloc = DependencyService.Get<IGeolocator>();

            if(geoloc.LastKnownLocation == null)
            {
                return coords.ToString();
            }

            double distance = geoloc.LastKnownLocation
                .DistanceTo(coords, DistanceUnits.Miles);

            // TODO: this should be randomized a bit
            // so that people can't easily find the location of
            // an item by checking the distance over and over.

            return $"{Math.Round(distance, 3)} miles away";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
