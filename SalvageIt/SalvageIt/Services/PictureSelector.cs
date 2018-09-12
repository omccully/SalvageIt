using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.IO;

namespace SalvageIt.Services
{
    public class PictureSelector : IPictureSelector
    {
        public async Task<ImageSource> SelectPicture()
        {
            MediaFile mf = await CrossMedia.Current.PickPhotoAsync();
            StreamImageSource sis = new StreamImageSource();
            sis.Stream = (t => new Task<Stream>(() => mf.GetStream()));
            return sis;
        }
    }
}
