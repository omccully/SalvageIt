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
        public ReadOnlyCollection<ItemReport> ItemReports => 
            new ReadOnlyCollection<ItemReport>(ObservableItemReports);
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

        public abstract void SubmitItem(ItemReport item_report);

        private void Value_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ItemReportsChanged?.Invoke(this, e);
        }
    }
}
