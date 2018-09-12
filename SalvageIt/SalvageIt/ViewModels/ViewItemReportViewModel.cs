using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SalvageIt.ViewModels
{
    using Models;
    using Services;
    using Xamarin.Forms;

    public class ViewItemReportViewModel : ViewModel
    {
        IMapsNavigation MapsNavigation;
        public ViewItemReportViewModel(IMapsNavigation maps_navigation)
        {
            this.MapsNavigation = maps_navigation;
        }

        public override Task InitializeAsync(object navigation_data)
        {
            ItemReport = (ItemReport)navigation_data;
            this.Title = ItemReport.Title;
            return Task.CompletedTask;
        }

        ItemReport _ItemReport;
        public ItemReport ItemReport
        {
            get
            {
                return _ItemReport;
            }
            set
            {
                SetProperty(ref _ItemReport, value, "ItemReport");
            }
        }

        ICommand _NavigateToItemCommand;
        public ICommand NavigateToItemCommand
        {
            get
            {
                return _NavigateToItemCommand ??
                    (_NavigateToItemCommand = new Command(NavigateToItemAction));
            }
        }

        void NavigateToItemAction()
        {
            MapsNavigation.NavigateToCoordinates(ItemReport.ItemLocation);
        }

        ICommand _EditItemReportCommand;
        public ICommand EditItemReportCommand
        {
            get
            {
                return _EditItemReportCommand ??
                    (_EditItemReportCommand = new Command(EditItemReportAction));
            }
        }

        void EditItemReportAction()
        {
            NavigationService.NavigateToAsync<ReportItemViewModel>(ItemReport);
        }

    }
}
