using System;
using System.Collections.Generic;
using System.Text;
using SalvageIt.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;

namespace SalvageIt.UnitTest.Models
{
    [TestClass]
    public class ObservableCollectionFilterTest
    {
        List<int> initial_data;
        ObservableCollection<int> source;
        ObservableCollectionFilter<int> filterer;
        ReadOnlyObservableCollection<int> results;

        [TestInitialize]
        public void Startup()
        {
            initial_data = new List<int>()
            {
                5, 10, 212
            };

            source = new ObservableCollection<int>(initial_data);

            filterer = new ObservableCollectionFilter<int>(source);

            results = filterer.FilteredResults;
        }

        [TestMethod]
        public void Filter_PermissiveFilter_LoadsInitialData()
        {
            CollectionAssert.AreEquivalent(
                initial_data, results);
        }

        [TestMethod]
        public void Filter_StrictFilter_FiltersInitialData()
        {
            filterer = new ObservableCollectionFilter<int>(source, 
                i => i < 100);

            results = filterer.FilteredResults;

            CollectionAssert.AreEquivalent(
                new List<int>() { 5, 10 },
                results);
        }

        [TestMethod]
        public void Filter_PermissiveFilter_AllowsAdding()
        {
            source.Add(500);

            initial_data.Add(500);
            CollectionAssert.AreEquivalent(initial_data, results);
        }

        [TestMethod]
        public void Filter_StrictFilterSet_FiltersExistingData()
        {
            filterer.Filter = (i) => i < 100;

            CollectionAssert.AreEquivalent(
                new List<int>() { 5, 10 }, results);
        }
    }
}
