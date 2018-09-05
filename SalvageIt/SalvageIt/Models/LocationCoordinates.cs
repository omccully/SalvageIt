using System;
using System.Collections.Generic;
using System.Text;

namespace SalvageIt.Models
{
    public class LocationCoordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        
        public LocationCoordinates(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public override string ToString()
        {
            return $"{Latitude},{Longitude}";
        }
    }
}
