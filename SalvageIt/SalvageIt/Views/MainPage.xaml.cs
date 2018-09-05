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

    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainPageViewModel(Navigation,
                DependencyService.Get<ItemReportStorage>());
        }
    }
}
