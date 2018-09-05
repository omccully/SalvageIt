using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace SalvageIt.Models
{
    using Models;
    using System.Collections.Specialized;

    public abstract class ItemReportStorage
    {
        public ReadOnlyObservableCollection<ItemReport> ItemReports => 
            new ReadOnlyObservableCollection<ItemReport>(ObservableItemReports);
        //new List<IItemReport>(ObservableItemReports).AsReadOnly();

        private ObservableCollection<ItemReport> _ObservableItemReports = null;
        protected ObservableCollection<ItemReport> ObservableItemReports
        {
            get
            {
                return _ObservableItemReports;
            }
            set
            {
                if(_ObservableItemReports != null)
                {
                    _ObservableItemReports.CollectionChanged -= Value_CollectionChanged;
                }
                value.CollectionChanged += Value_CollectionChanged;
                _ObservableItemReports = value;
            }
        }

        public event NotifyCollectionChangedEventHandler ItemReportsChanged;

        public abstract int SubmitItem(ItemReport item_report);

        private void Value_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ItemReportsChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Throws exceptions if the ItemReport is invalid
        /// </summary>
        /// <param name="item_report"></param>
        /// <returns></returns>
        protected void AssertItemReportValid(ItemReport item_report)
        {
            if(item_report.ItemPhoto == null)
            {
                throw new Exception("Item reports require a photo");
            }

            if(item_report.ItemLocation == null)
            {
                throw new Exception("Item reports require a location");
            }
        }
    }
}
