using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SalvageIt.Views
{
    using ViewModels;
    using Models;
    using Services;

    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainViewModel(Navigation,
                DependencyService.Get<ItemReportStorage>(),
                DependencyService.Get<IGeolocator>());

            ReportItemsListView.ItemsSource = 
                DependencyService.Get<ItemReportStorage>().LocalItemReports;
        }
    }
}
