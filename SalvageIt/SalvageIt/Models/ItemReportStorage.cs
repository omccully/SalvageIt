using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Linq;

namespace SalvageIt.Models
{
    using Models;
    using Models.Validators;
    using Services;

    public abstract class ItemReportStorage
    {
        /// <summary>
        /// LocalItemReports updates when Refresh() or SubmitItem() is called
        /// </summary>
        public ReadOnlyObservableCollection<ItemReport> LocalItemReports => 
            new ReadOnlyObservableCollection<ItemReport>(EditableLocalItemReports);

        protected ObservableCollection<ItemReport> EditableLocalItemReports { get; private set; } = 
            new ObservableCollection<ItemReport>();

        public abstract Task<int> SubmitItem(ItemReport item_report);

        public abstract Task Refresh(LocationCoordinates location, double radius_miles);

        public IValidator<ItemReport> ItemReportValidator { get; set; }

        protected ItemReportStorage(IValidator<ItemReport> item_report_validator)
        {
            this.ItemReportValidator = item_report_validator;
        }

        /// <summary>
        /// Throws exceptions if the ItemReport is invalid
        /// </summary>
        /// <param name="item_report"></param>
        /// <returns></returns>
        protected void AssertItemReportValid(ItemReport item_report)
        {
            IEnumerable<string> broken_rules = ItemReportValidator.BrokenRules(item_report);
            if (broken_rules.Count() > 0) throw new DataInvalidException(broken_rules);
        }
    }
}
