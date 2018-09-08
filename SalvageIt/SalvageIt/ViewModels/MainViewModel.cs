using SalvageIt.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace SalvageIt.ViewModels
{
    using Models;
    using Services;

    public class MainViewModel : BaseViewModel
    {
        INavigation Navigation;
        ItemReportStorage ItemReportStorage;
        IGeolocator Geolocator;

        public MainViewModel(INavigation navigation, 
            ItemReportStorage item_report_storage, IGeolocator geolocator)
        {
            this.Navigation = navigation;
            this.ItemReportStorage = item_report_storage;

            this.Geolocator = geolocator;

            RefreshActionAsync();
        }

        ICommand _ReportItemCommand;
        public ICommand ReportItemCommand
        {
            get
            {
                return _ReportItemCommand ??
                    (_ReportItemCommand = new Command(ReportItemAction));
            }
        }

        void ReportItemAction()
        {
            Navigation.PushAsync(new ReportItemPage());
        }

        public ICommand AddReportTest
        {
            get
            {
                return new Command(delegate()
                {
                    ItemReportStorage.SubmitItem(
                        new ItemReport()
                        {
                            Title = "Test item",
                            Description = "found on side of road on my way to work this morning",
                            ItemLocation = new LocationCoordinates(1.0, 1.0),
                            ReportTime = DateTime.Now,
                            ItemPhoto = new StreamImageSource()
                        });
                });
            }
        }

        ICommand _RefreshCommand;
        public ICommand RefreshCommand
        {
            get
            {
                return _RefreshCommand ??
                    (_RefreshCommand = new Command(RefreshActionAsync));
            }
        }

        async void RefreshActionAsync()
        {
            IsRefreshing = true;
            await ItemReportStorage.Refresh(await Geolocator.GetPositionAsync(), 1.0);
            IsRefreshing = false;
        }

        bool _IsRefreshing = false;
        public bool IsRefreshing
        {
            get
            {
                return _IsRefreshing;
            }
            set
            {
                SetProperty(ref _IsRefreshing, value);
            }
        }
    }
}
