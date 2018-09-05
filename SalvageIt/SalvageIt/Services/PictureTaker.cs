using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SalvageIt.Services;

[assembly: Dependency(typeof(PictureTaker))]
namespace SalvageIt.Services
{
    public class PictureTaker : IPictureTaker
    {
        public async Task<ImageSource> TakePicture()
        {
            var cmo = new Plugin.Media.Abstractions.StoreCameraMediaOptions();
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(cmo);

            if (photo != null)
                return ImageSource.FromStream(() => { return photo.GetStream(); });
            return null;
        }
    }
}
