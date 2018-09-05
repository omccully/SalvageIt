using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace SalvageIt.Views
{
    using Models;
    using Services;

    public class SelectLocationPage : ContentPage, IDisposable
	{
        // TODO: require that user is zoomed close enough 
        // to select a precise location

        public event EventHandler<LocationCoordinatesEventArgs> LocationChosen;
        protected void OnLocationChosen(LocationCoordinates location_coords)
        {
            LocationChosen?.Invoke(this,
                new LocationCoordinatesEventArgs(location_coords));
        }

        IToaster Toaster;

		public SelectLocationPage(LocationCoordinates initial_position = null)
		{
            Toaster = DependencyService.Get<IToaster>();
            if (initial_position == null)
            {
                InitializeMap();
            }
            else
            {
                InitializeMap(initial_position);
            }
        }

        async void InitializeMap()
        {
            IGeolocator geolocator = DependencyService.Get<IGeolocator>();
            LocationCoordinates initial_position = 
                await geolocator.GetPositionAsync();

            InitializeMap(initial_position);
        }

        Map TheMap;
        Timer PinUpdateTimer;
        TimeSpan PinRefreshPeriod = TimeSpan.FromMilliseconds(100);

        void InitializeMap(LocationCoordinates initial_position)
        {
            Position map_position = new Position(initial_position.Latitude,
                initial_position.Longitude);

            TheMap = new Map(
                MapSpan.FromCenterAndRadius(
                    map_position, Distance.FromMiles(0.3)))
            {
                IsShowingUser = true,
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            //var stack = new StackLayout { Spacing = 0 };
            //stack.Children.Add(TheMap);
            //Content = stack;

            AbsoluteLayout absolute = new AbsoluteLayout();

            AbsoluteLayout.SetLayoutBounds(TheMap, new Rectangle(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(TheMap, AbsoluteLayoutFlags.All);
            absolute.Children.Add(TheMap);

            // ~~ Help label
            Label help_label = new Label()
            {
                Text = "Center your map on the item's location",
                BackgroundColor = Color.White
            };

            AbsoluteLayout.SetLayoutBounds(help_label, new Rectangle(.5, .04, .5, .1));
            AbsoluteLayout.SetLayoutFlags(help_label, AbsoluteLayoutFlags.All);
            absolute.Children.Add(help_label);

            // ~~ Finished button
            Button finished_button = new Button()
            {
                Text = "Done"
            };
            AbsoluteLayout.SetLayoutBounds(finished_button, new Rectangle(.5, 1, .5, .1));
            AbsoluteLayout.SetLayoutFlags(finished_button, AbsoluteLayoutFlags.All);
            finished_button.Clicked += Finished_button_Clicked;
            absolute.Children.Add(finished_button);

            Content = absolute;

            PinUpdateTimer = new Timer(UpdatePin, null,
                TimeSpan.Zero, PinRefreshPeriod);
        }

        private void Finished_button_Clicked(object sender, EventArgs e)
        {
            MapSpan region = TheMap.VisibleRegion;
            if (region == null)
            {
                Toaster.DisplayError("Map not loaded");
                return;
            }

            if(region.Radius.Meters > Distance.FromMiles(1.0).Meters)
            {
                Toaster.DisplayError("Please zoom in more to get a more " +
                    "precise location");
                return;
            }

            Position pos = region.Center;

            Navigation.PopAsync();
            OnLocationChosen(new LocationCoordinates(pos.Latitude, pos.Longitude));
        }

        void UpdatePin(object state)
        {
            if(TheMap == null)
            {
                PinUpdateTimer.Dispose();
            }

            MapSpan region = TheMap.VisibleRegion;

            if (region == null) return;

            UpdatePinToPosition(region.Center);
        }

        Position LastPinPosition = new Position();
        void UpdatePinToPosition(Position pos)
        {
            if (pos == LastPinPosition && TheMap.Pins.Count > 0) return;

            Pin pin = new Pin
            {
                Type = PinType.Generic,
                Position = pos,
                Label = "Item location"
            };

            Device.BeginInvokeOnMainThread(delegate
            {
                TheMap.Pins.Clear();
                TheMap.Pins.Add(pin);
            });
        }

        public void Dispose()
        {
            TheMap = null;
            PinUpdateTimer.Dispose();
            //PinUpdateThread.Abort();
        }
    }
}