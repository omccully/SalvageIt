using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SalvageIt.ViewModels
{
    using Services;
    using Models;

    public class SelectLocationViewModel : ViewModel
    {
        public IGeolocator Geolocator { get; private set; }
        public IToaster Toaster { get; private set; }

        public SelectLocationViewModel(IGeolocator geolocator, IToaster toaster)
        {
            this.Geolocator = geolocator;
            this.Toaster = toaster;
        }

        public override async Task InitializeAsync(object navigation_data)
        {
            LocationCoordinates initial_position = navigation_data as LocationCoordinates;

            if(initial_position != null)
            {
                InitialMapLocationCenter = initial_position;
            }
            else
            {
                InitialMapLocationCenter = await Geolocator.GetPositionAsync();
            } 
        }

        LocationCoordinates _InitialMapLocationCenter;
        public LocationCoordinates InitialMapLocationCenter
        {
            get
            {
                return _InitialMapLocationCenter;
            }
            set
            {
                System.Diagnostics.Debug.WriteLine("InitialMapLocationCenter = " + value);
                SetProperty(ref _InitialMapLocationCenter, value, "InitialMapLocationCenter");
            }
        }

        public void LocationChosen(LocationCoordinates coords)
        {
            NavigationService.GoBackAsync(coords);
        }
    }
}
