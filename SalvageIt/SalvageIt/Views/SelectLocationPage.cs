using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace SalvageIt.Views
{
    class TestRecognizer : IGestureRecognizer
    {
        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class SelectLocationPage : ContentPage
	{
        void MapTapped(View view)
        {
            System.Diagnostics.Debug.WriteLine("map tapped " + view);
        }

		public SelectLocationPage (Position pos)
		{
            

            var map = new Map(
                MapSpan.FromCenterAndRadius(
                    pos, Distance.FromMiles(0.3)))
                {
                    IsShowingUser = true,
                    HeightRequest = 100,
                    WidthRequest = 960,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };

            TapGestureRecognizer tgr = new TapGestureRecognizer(MapTapped);
            map.GestureRecognizers.Add(tgr);

            var pin = new Pin
            {
                Type = PinType.Generic,
                Position = pos,
                Label = "Item location" //,
                //Address = "custom detail info"
            };
            pin.Clicked += Pin_Clicked;

            map.Pins.Add(pin);

            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(map);
            Content = stack;
        }

        private void Pin_Clicked(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("pin clicked");
        }
    }
}