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


        public double DistanceTo(LocationCoordinates lc, DistanceUnits units)
        {
            const double EarthRadiusKm = 6371.0;
            const double EarthRadiusMi = 3959.0;
            double radius =
                (units == DistanceUnits.Kilometers ? EarthRadiusKm : EarthRadiusMi);

            double dLat_rads = DegreesToRadians(lc.Latitude - Latitude);
            double dLong_rads = DegreesToRadians(lc.Longitude - Longitude);

            double a = Math.Pow(Math.Sin(dLat_rads / 2), 2) +
                Math.Cos(DegreesToRadians(Latitude)) *
                Math.Cos(DegreesToRadians(lc.Latitude)) *
                Math.Pow(Math.Sin(dLong_rads / 2), 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return radius * c;
        }

        double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180.0);
        }

        public override string ToString()
        {
            return $"{Math.Round(Latitude,5)},{Math.Round(Longitude, 5)}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Routine return false.
            LocationCoordinates r = obj as LocationCoordinates;

            return this.Equals(r);
        }

        public bool Equals(LocationCoordinates lc)
        {
            if ((Object)lc == null)
            {
                return false;
            }

            return this.Latitude == lc.Latitude &&
                this.Longitude == lc.Longitude;
        }

        public static bool operator ==(LocationCoordinates a, LocationCoordinates b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a.Equals(b);
        }

        public static bool operator !=(LocationCoordinates a, LocationCoordinates b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return Latitude.GetHashCode() ^ Longitude.GetHashCode();
        }
    }
}
