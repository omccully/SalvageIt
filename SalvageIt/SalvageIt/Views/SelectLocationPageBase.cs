using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.ComponentModel;

namespace SalvageIt.Views
{
    using ViewModels;
    using Models;

    [ContentProperty("Content")]
    public class SelectLocationPageBase : ContentPage
    {
        protected SelectLocationViewModel ViewModel;
        public SelectLocationPageBase()
        { 
            // BindingContext is necessary for NavigationService
            BindingContext = ViewModel = (SelectLocationViewModel)
                ViewModelLocator.CreateViewModelForPage(this);

            ViewModel.PropertyChanged += Vm_PropertyChanged;
        }

        private void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Vm_PropertyChanged(" + e.PropertyName);
            if (e.PropertyName != "InitialMapLocationCenter") return;

            SetInitialMapCenter(ViewModel.InitialMapLocationCenter);
        }

        LocationCoordinates InitialMapCenter = null;
        void SetInitialMapCenter(LocationCoordinates coords)
        {
            if (coords == null) return;

            if (InitialMapCenter != null) return;
            InitialMapCenter = coords;

            InitializeMap(coords);
        }

        protected virtual void InitializeMap(LocationCoordinates initial_center)
        {

        }
    }
}
