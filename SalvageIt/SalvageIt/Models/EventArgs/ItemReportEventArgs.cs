using System;
using System.Collections.Generic;
using System.Text;

namespace SalvageIt.Models
{
    public class ItemReportEventArgs : EventArgs
    {
        public ItemReport ItemReport { get; private set; }

        public ItemReportEventArgs(ItemReport item_report)
        {
            this.ItemReport = item_report;
        }
    }
}
