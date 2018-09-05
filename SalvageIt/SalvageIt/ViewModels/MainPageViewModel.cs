using SalvageIt.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SalvageIt.ViewModels
{
    using Models;
    using System.Collections.ObjectModel;

    public class MainPageViewModel
    {
        INavigation Navigation;
        ItemReportStorage ItemReportStorage;

        public MainPageViewModel(INavigation navigation, ItemReportStorage item_report_storage)
        {
            this.Navigation = navigation;
            this.ItemReportStorage = item_report_storage;
            ItemReports = item_report_storage.ItemReports;
        }

        public ReadOnlyObservableCollection<ItemReport> ItemReports { get; set; }

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
            ReportItemPage rip = new ReportItemPage();
            rip.ReportItemSubmitted += Rip_ReportItemSubmitted;
            Navigation.PushAsync(rip);
        }

        private void Rip_ReportItemSubmitted(object sender, Models.ItemReportEventArgs e)
        {
            // refresh list
        }
    }
}
