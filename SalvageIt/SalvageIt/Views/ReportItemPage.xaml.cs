using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SalvageIt.Views
{
    using Services;
    using ViewModels;
    using Models;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ReportItemPage : ContentPage
	{
        ReportItemViewModel Vm;

        public ReportItemPage ()
		{
			InitializeComponent ();

            BindingContext = Vm = new ReportItemViewModel(
                Navigation,
                DependencyService.Get<IPictureTaker>(),
                DependencyService.Get<IToaster>(),
                DependencyService.Get<ItemReportStorage>());
            Vm.ReportItemSubmitted += Vm_ReportItemSubmitted;
        }

        public event EventHandler<ItemReportEventArgs> ReportItemSubmitted;
        protected void OnReportItemSubmitted(ItemReport item_report)
        {
            ReportItemSubmitted?.Invoke(this, new ItemReportEventArgs(item_report));
        }

        private void Vm_ReportItemSubmitted(object sender, ItemReportEventArgs e)
        {
            OnReportItemSubmitted(e.ItemReport);
        }
    }
}