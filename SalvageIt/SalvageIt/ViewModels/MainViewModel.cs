﻿using SalvageIt.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace SalvageIt.ViewModels
{
    using Models;
    using Services;
    using Navigation;

    public class MainViewModel : ViewModel
    {
        ItemReportStorage ItemReportStorage;
        IGeolocator Geolocator;

        public MainViewModel(ItemReportStorage item_report_storage, 
            IGeolocator geolocator)
        {
            this.ItemReportStorage = item_report_storage;
            this.LocalItemReports = item_report_storage.LocalItemReports;
            this.Geolocator = geolocator;
            this.Title = "Salvage It";

            RefreshActionAsync();
        }

        public ReadOnlyObservableCollection<ItemReport> LocalItemReports { get; set; } 

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
            NavigationService.NavigateToAsync<ReportItemViewModel>();
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
                            ReportTime = DateTime.Now - TimeSpan.FromDays(10),
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
                SetProperty(ref _IsRefreshing, value,
                    "IsRefreshing");
            }
        }

        ICommand _SelectItemReportCommand;
        public ICommand SelectItemReportCommand
        {
            get
            {
                return _SelectItemReportCommand ??
                    (_SelectItemReportCommand = new Command(SelectItemReportAction));
            }
        }

        void SelectItemReportAction(object item_report_obj)
        {
            NavigationService.NavigateToAsync<ViewItemReportViewModel>(item_report_obj);
        }
    }
}
