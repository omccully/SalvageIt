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

        public VolatileItemReportStorage(IValidator<ItemReport> item_report_validator,
            IEnumerable<ItemReport> initial_data)
            : base(item_report_validator)
        {
            foreach(ItemReport ir in initial_data)
            {
                EditableLocalItemReports.Add(ir);
            }
        }

        public override async Task Refresh(LocationCoordinates location, double radius_miles)
        {
            await Task.Delay(500);

            List<ItemReport> item_reports = 
                new List<ItemReport>(LocalItemReports);

            EditableLocalItemReports.Clear();
            foreach(ItemReport ir in item_reports)
            {
                EditableLocalItemReports.Add(ir);
            }
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
                item_report.ReportTime = DateTime.Now;
                item_report.EditTime = item_report.ReportTime;
                item_report.IsMine = true;

                EditableLocalItemReports.Add(item_report);
                return id;
            }
            else
            { 
                int i = 0;
                foreach(ItemReport ir in EditableLocalItemReports)
                {
                    if(ir.ID == item_report.ID)
                    {
                        if(!ir.IsMine)
                        {
                            // this should be verified server-side as well
                            throw new Exception(
                                "You do not have permission to edit this item report.");
                        }
                        item_report.ReportTime = ir.ReportTime;
                        item_report.EditTime = DateTime.Now;
                        item_report.IsMine = true;

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
