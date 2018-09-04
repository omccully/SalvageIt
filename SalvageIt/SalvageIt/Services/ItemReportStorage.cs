using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace SalvageIt.Services
{
    using Models;
    using System.Collections.Specialized;

    abstract class ItemReportStorage
    {
        public ReadOnlyCollection<IItemReport> ItemReports => 
            new ReadOnlyCollection<IItemReport>(ObservableItemReports);
        //new List<IItemReport>(ObservableItemReports).AsReadOnly();

        private ObservableCollection<IItemReport> _ObservableItemReports = null;
        protected ObservableCollection<IItemReport> ObservableItemReports
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

        public abstract void SubmitItem(IItemReport item_report);

        private void Value_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ItemReportsChanged?.Invoke(this, e);
        }
    }
}
