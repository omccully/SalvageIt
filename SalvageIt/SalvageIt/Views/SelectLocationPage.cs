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
        public event EventHandler<LocationCoordinatesEventArgs> LocationChosen;
        protected void OnLocationChosen(LocationCoordinates location_coords)
        {
            LocationChosen?.Invoke(this,
                new LocationCoordinatesEventArgs(location_coords));
        }

		public SelectLocationPage (LocationCoordinates initial_position = null)
		{
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
        Thread PinUpdateThread;

        void InitializeMap(LocationCoordinates initial_position)
        {
            // TODO: when the user moves the map,
            // put a market in the middle. the user moves/zooms 
            // to select location, then clicks a button.

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
            
            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(TheMap);
            Content = stack;

            PinUpdateThread = new Thread(PinUpdater);
            PinUpdateThread.Start();
        }

        void PinUpdater()
        {
            while(TheMap != null)
            {
                UpdatePin();
                Thread.Sleep(30);
            }
        }

        void UpdatePin()
        {
            MapSpan region = TheMap.VisibleRegion;

            if (region == null) return;

            UpdatePinToPosition(region.Center);
        }

        Position LastPinPosition = new Position();
        void UpdatePinToPosition(Position pos)
        {
            if (pos == LastPinPosition) return;

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
            PinUpdateThread.Abort();
        }
    }
}