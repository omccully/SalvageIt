using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalvageIt.ViewModels;
using SalvageIt.Models;
using SalvageIt.Services;
using Moq;
using System.Threading.Tasks;


namespace SalvageIt.UnitTest.ViewModels
{
    using TestHelpers;

    [TestClass]
    public class MainViewModelTest
    {
        /*[TestMethod]
        public void RefreshCommand()
        {
            RefreshCommandAsync();
        }

        public async void RefreshCommandAsync()
        {
            Mock<IGeolocator> geoloc_mock = new Mock<IGeolocator>();
            Mock<ItemReportStorage> irs_mock = new Mock<ItemReportStorage>();
            //irs_mock.Setup((irs) => irs.Refresh())

            MainViewModel vm = new MainViewModel(
                new VolatileItemReportStorage(),
                geoloc_mock.Object);

            Assert.IsFalse(vm.IsRefreshing);
            vm.RefreshCommand.Execute(null);
            Assert.IsFalse(vm.IsRefreshing);
            await Task.Delay(100);
        }*/

        /*[TestMethod] 
        public void IsRefreshing()
        {
            Mock<IGeolocator> geoloc_mock = new Mock<IGeolocator>();
            MainViewModel vm = new MainViewModel(
                new VolatileItemReportStorage(), geoloc_mock.Object);

            PropertyChangedQueue pc_q = new PropertyChangedQueue(vm);

            Assert.AreEqual(0, pc_q.Count);
            vm.IsRefreshing = true;
            Assert.AreEqual(1, pc_q.Count);
            Assert.AreEqual("IsRefreshing", pc_q.Dequeue());
            Assert.AreEqual(0, pc_q.Count);
            vm.IsRefreshing = false;
            Assert.AreEqual("IsRefreshing", pc_q.Dequeue());
        }*/
    }
}
