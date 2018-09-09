using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalvageIt.Models;
using Moq;
using SalvageIt.Models.Validators;
using System.Collections;
using System.Collections.Generic;
using System;

namespace SalvageIt.UnitTest.Models
{
    [TestClass]
    public class VolatileItemReportStorageTest
    {

        [TestInitialize]
        public void Setup()
        {


  
        }

        [TestMethod]
        public void SubmitItem_InvalidItemReport()
        {
            SubmitItem_InvalidItemReport_Async();
        }

        public async void SubmitItem_InvalidItemReport_Async()
        {
            Mock<IValidator<ItemReport>> validator_mock = new Mock<IValidator<ItemReport>>();
            validator_mock.Setup(v => v.BrokenRules(It.IsAny<ItemReport>()))
                .Throws(new DataInvalidException(
                        new List<string>() { "The item report is invalid" }
                    )
                );

            VolatileItemReportStorage virs = new VolatileItemReportStorage(validator_mock.Object);

            await Assert.ThrowsExceptionAsync<DataInvalidException>(
                () => virs.SubmitItem(new ItemReport()));
        }

        [TestMethod]
        public void SubmitItem_ValidItemReport()
        {
            SubmitItem_ValidItemReport_Async();
        }

        public async void SubmitItem_ValidItemReport_Async()
        {
            Mock<IValidator<ItemReport>> validator_mock = new Mock<IValidator<ItemReport>>();
            
            VolatileItemReportStorage virs = new VolatileItemReportStorage(validator_mock.Object);

            ItemReport ir = new ItemReport();
            await virs.SubmitItem(ir);
            Assert.AreEqual(ir, virs.LocalItemReports[0]);
            Assert.AreEqual(1, ir.ID); // sets ID to 1. 0 is invalid    
        }

        [TestMethod]
        public void SubmitItem_InvalidEdit()
        {
            SubmitItem_InvalidEdit_Async();
        }

        public async void SubmitItem_InvalidEdit_Async()
        {
            Mock<IValidator<ItemReport>> validator_mock = new Mock<IValidator<ItemReport>>();

            VolatileItemReportStorage virs = new VolatileItemReportStorage(validator_mock.Object);

            ItemReport ir = new ItemReport() { ID = 5 };

            await Assert.ThrowsExceptionAsync<InvalidOperationException>(
                () => virs.SubmitItem(ir));
        }


        [TestMethod]
        public void SubmitItem_EditItemReport()
        {
            SubmitItem_EditItemReport_Async();
        }

        public async void SubmitItem_EditItemReport_Async()
        {
            Mock<IValidator<ItemReport>> validator_mock = new Mock<IValidator<ItemReport>>();

            VolatileItemReportStorage virs = new VolatileItemReportStorage(validator_mock.Object);

            ItemReport ir = new ItemReport();
            await virs.SubmitItem(ir);
            Assert.AreEqual(ir, virs.LocalItemReports[0]);
            Assert.AreEqual(1, ir.ID); // sets ID to 1. 0 is invalid    

            ItemReport edited = new ItemReport()
            {
                ID = 1,
                Title = "new title"
            };

            await virs.SubmitItem(edited);
            Assert.AreNotEqual(ir, virs.LocalItemReports[0]);
            Assert.AreEqual(edited, virs.LocalItemReports[0]);
            Assert.AreEqual("new title", virs.LocalItemReports[0].Title);
        }
    }
}
