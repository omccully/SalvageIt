using System;
using System.Collections.Generic;
using System.Text;
using SalvageIt.Models;

namespace SalvageIt.Models
{
    public class AwsItemReportStorage : ItemReportStorage
    {
        public AwsItemReportStorage(IUserCredentials creds)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Attempts to send the item_report to the cloud. 
        /// </summary>
        /// <param name="item_report"></param>
        public override int SubmitItem(ItemReport item_report)
        {
            throw new NotImplementedException();
        }
    }
}
