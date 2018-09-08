using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SalvageIt.Models;

namespace SalvageIt.Models
{
    using Validators;

    public class AwsItemReportStorage : ItemReportStorage
    {
        public AwsItemReportStorage(IValidator<ItemReport> item_report_validator, 
            IUserCredentials creds) : base(item_report_validator)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Attempts to send the item_report to the cloud. 
        /// </summary>
        /// <param name="item_report"></param>
        public override async Task<int> SubmitItem(ItemReport item_report)
        {
            throw new NotImplementedException();
        }

        public override async Task Refresh(LocationCoordinates location, double radius_miles)
        {
            throw new NotImplementedException();
        }
    }
}
