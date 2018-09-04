using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace SalvageIt.Services
{
    using Models;
    
    /// <summary>
    /// Abstraction for submitting and receiving IItemReports
    /// 
    /// Test with VolatileItemReportStorage:    
    /// Production with AwsItemReportStorage:
    /// </summary>
    interface IItemReportStorage
    {
        ReadOnlyCollection<IItemReport> ItemReports { get; }

        void SubmitItem(IItemReport item_report);
    }
}
