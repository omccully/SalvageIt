using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using SalvageIt.Models;

[assembly: Dependency(typeof(VolatileItemReportStorage))]
namespace SalvageIt.Models
{
    using Models;

    public class VolatileItemReportStorage : ItemReportStorage
    {
        public VolatileItemReportStorage()
        {
            ObservableItemReports = new ObservableCollection<ItemReport>();
        }

        public override void SubmitItem(ItemReport item_report)
        {
            ObservableItemReports.Add(item_report);
        }
    }
}
