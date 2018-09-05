using SalvageIt.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SalvageIt.ViewModels
{
    public class MainPageViewModel
    {
        INavigation Navigation;

        public MainPageViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
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
    }
}
