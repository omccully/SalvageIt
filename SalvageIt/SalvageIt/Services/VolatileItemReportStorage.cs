using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace SalvageIt.Services
{
    using Models;

    class VolatileItemReportStorage : ItemReportStorage
    {
        public VolatileItemReportStorage()
        {
            ObservableItemReports = new ObservableCollection<IItemReport>();
        }

        public override void SubmitItem(IItemReport item_report)
        {
            ObservableItemReports.Add(item_report);
        }
    }
}
