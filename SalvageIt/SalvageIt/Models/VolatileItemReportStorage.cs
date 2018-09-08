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
    using SalvageIt.Models.Validators;
    using System.Threading.Tasks;

    public class VolatileItemReportStorage : ItemReportStorage
    {
        public VolatileItemReportStorage() :
            base(new SubmitItemReportValidator())
        {

        }

        public VolatileItemReportStorage(IValidator<ItemReport> item_report_validator)
            : base(item_report_validator)
        {

        }

        public override async Task Refresh(LocationCoordinates location, double radius_miles)
        {
            await Task.Delay(500);
        }

        public override async Task<int> SubmitItem(ItemReport item_report)
        {
            AssertItemReportValid(item_report);

            if (item_report.ID == 0) // no ID, create new
            {
                int max_or_zero = EditableLocalItemReports.Count() > 0 ?
                    EditableLocalItemReports.Max(ir => ir.ID) : 0;

                int id = max_or_zero + 1;
                item_report.ID = id;
                EditableLocalItemReports.Add(item_report);
                return id;
            }
            else
            {
                // when cloud storage is implemented, this should 
                // check the user credentials to make sure they're allowed
                // to edit that ItemReport.
                int i = 0;
                foreach(ItemReport ir in EditableLocalItemReports)
                {
                    if(ir.ID == item_report.ID)
                    {
                        EditableLocalItemReports.RemoveAt(i);
                        EditableLocalItemReports.Add(item_report);
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
