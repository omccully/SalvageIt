using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SalvageIt.Views
{
    using ViewModels;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ReportItemPage : ContentPage
	{
        ReportItemViewModel Vm;

		public ReportItemPage ()
		{
			InitializeComponent ();

            Vm = new ReportItemViewModel();
		}

        private async void CameraButton_Clicked(object sender, EventArgs e)
        {
            ImageSource image_source = await Vm.TakePicture();
            System.Diagnostics.Debug.WriteLine("image_source = " + image_source);

            PhotoImage.Source = image_source;
        }

        private async void SelectLocationButton_Clicked(object sender, EventArgs e)
        {
            Plugin.Geolocator.Abstractions.Position current_pos = await Vm.GetLocation();
            //System.Diagnostics.Debug.WriteLine("Position = " + pos.);
            System.Diagnostics.Debug.WriteLine("Position = " + current_pos.Longitude);
            System.Diagnostics.Debug.WriteLine("Position = " + current_pos.Latitude);
            System.Diagnostics.Debug.WriteLine("Position = " + current_pos.Timestamp);

            Xamarin.Forms.Maps.Position map_current_pos =
                new Xamarin.Forms.Maps.Position(current_pos.Latitude, current_pos.Longitude);

            await Navigation.PushAsync(new SelectLocationPage(map_current_pos));
        }
    }
}