using System;
using System.Collections.Generic;
using System.Text;

namespace SalvageIt.Services
{
    using Models;

    public class LocationCoordinatesEventArgs
    {
        public LocationCoordinates LocationCoordinates { get; set; }

        public LocationCoordinatesEventArgs(LocationCoordinates location_coords)
        {
            this.LocationCoordinates = location_coords;
        }
    }
}
