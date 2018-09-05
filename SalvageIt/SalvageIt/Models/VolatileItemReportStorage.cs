using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using SalvageIt.Models;
using System.Linq;

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

        public override int SubmitItem(ItemReport item_report)
        {
            AssertItemReportValid(item_report);

            if (item_report.ID == 0) // no ID, create new
            {
                int id = ObservableItemReports.Max(ir => ir.ID) + 1;
                item_report.ID = id;
                ObservableItemReports.Add(item_report);
                return id;
            }
            else
            {
                // when cloud storage is implemented, this should 
                // check the user credentials to make sure they're allowed
                // to edit that ItemReport.
                int i = 0;
                foreach(ItemReport ir in ObservableItemReports)
                {
                    if(ir.ID == item_report.ID)
                    {
                        ObservableItemReports.RemoveAt(i);
                        ObservableItemReports.Add(item_report);
                        return item_report.ID;
                    }
                    i++;
                }

                throw new InvalidOperationException("No item report" +
                    " with this ID exists, so it cannot be edited");
            }
        }
    }
}
