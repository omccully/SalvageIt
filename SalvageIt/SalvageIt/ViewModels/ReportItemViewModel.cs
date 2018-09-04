using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SalvageIt.ViewModels
{
    public class ReportItemViewModel
    {
        public ReportItemViewModel()
        {

        }

        public async Task<Position> GetLocation()
        {
            var locator = CrossGeolocator.Current;

            return await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
        }

        public async Task<ImageSource> TakePicture()
        {
            var cmo = new Plugin.Media.Abstractions.StoreCameraMediaOptions();
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(cmo);

            if (photo != null)
                return ImageSource.FromStream(() => { return photo.GetStream(); });
            return null;
        }

        // take picture
        // determine location
        // display location to user and have them select & confirm a location
        // 
    }
}
