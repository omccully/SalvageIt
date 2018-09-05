using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SalvageIt.Services
{
    public interface IPictureTaker
    {
        Task<ImageSource> TakePicture();
    }
}
