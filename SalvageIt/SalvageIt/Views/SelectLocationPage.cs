using System;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace SalvageIt.Views
{
    using Models;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectLocationPage : SelectLocationPageBase, IDisposable
    {
        Distance DefaultMapRadius = Distance.FromMiles(0.3);
        Distance MaxAcceptableZoom = Distance.FromMiles(1.0);

        Map TheMap;
        Timer PinUpdateTimer;
        TimeSpan PinRefreshPeriod = TimeSpan.FromMilliseconds(100);

        public SelectLocationPage() : base()
        {
           
        }

        protected override void InitializeMap(LocationCoordinates initial_center)
        {
            System.Diagnostics.Debug.WriteLine("InitializeMap(" + initial_center);
            InitializeMap(new Position(initial_center.Latitude, initial_center.Longitude));

            PinUpdateTimer = new Timer(UpdatePin, null,
                TimeSpan.Zero, PinRefreshPeriod);
        }

        void InitializeMap(Position initial_map_pos)
        {
            TheMap = new Map(
                MapSpan.FromCenterAndRadius(
                    initial_map_pos, Distance.FromMiles(0.3)))
            {
                IsShowingUser = true,
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

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
            Button done_button = new Button()
            {
                Text = "Done"
            };
            AbsoluteLayout.SetLayoutBounds(done_button, new Rectangle(.5, 1, .5, .1));
            AbsoluteLayout.SetLayoutFlags(done_button, AbsoluteLayoutFlags.All);
            done_button.Clicked += Done_Clicked;
            absolute.Children.Add(done_button);

            System.Diagnostics.Debug.WriteLine("Setting SelectLocationPage content");
            Device.BeginInvokeOnMainThread(delegate
            {
                Content = absolute;
            });
        }

        void SetMapCenter(Position map_pos)
        {
            Distance radius = TheMap.VisibleRegion == null ?
                TheMap.VisibleRegion.Radius :
                DefaultMapRadius;

            TheMap.SetVisibleRegion(MapSpan
                .FromCenterAndRadius(map_pos, radius));
        }

        void UpdatePin(object state)
        {
            if (PinUpdateTimer == null) return; // disposed

            MapSpan region = TheMap.VisibleRegion;

            if (region == null) return; // map not loaded yet

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

        private void Done_Clicked(object sender, EventArgs e)
        {
            MapSpan region = TheMap.VisibleRegion;
            if (region == null)
            {
                ViewModel.Toaster.DisplayError("Map not loaded");
                return;
            }

            if (region.Radius.Meters > MaxAcceptableZoom.Meters)
            {
                ViewModel.Toaster.DisplayError("Please zoom in more " +
                    "to get a more precise location");
                return;
            }

            Position pos = region.Center;

            ViewModel.LocationChosen(new LocationCoordinates(pos.Latitude, pos.Longitude));
        }

        public void Dispose()
        {
            PinUpdateTimer.Dispose();
            PinUpdateTimer = null;
        }
    }
}