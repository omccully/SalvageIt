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
        MainViewModel Vm;

        public MainPage()
        {
            InitializeComponent();
            this.Vm = (MainViewModel)BindingContext;
        }

        private void ReportItemsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Vm.SelectItemReportCommand.Execute(e.Item);
        }
    }
}
