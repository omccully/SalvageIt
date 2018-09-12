using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalvageIt.Models;
using SalvageIt.Models.Validators;
using System;
using Xamarin.Forms;

namespace SalvageIt.UnitTest.Models.Validators
{
    [TestClass]
    public class SubmitItemReportValidatorTest
    {
        SubmitItemReportValidator Validator = new SubmitItemReportValidator();
        ItemReport test_item_report;

        [TestInitialize]
        public void Startup() {
            test_item_report = new ItemReport()
            {
                Title = "Test item",
                Description = "found on side of road on my way to work this morning",
                ItemLocation = new LocationCoordinates(1.0, 1.0),
                ReportTime = DateTime.Now,
                ItemPhoto = new StreamImageSource()
            };
        }

        [TestMethod]
        public void IsValid_True()
        {
            Assert.IsTrue(Validator.IsValid(test_item_report));
        }

        [TestMethod]
        public void IsValid_NoTitle()
        {
            test_item_report.Title = " ";

            Assert.IsFalse(Validator.IsValid(test_item_report));
        }

        [TestMethod]
        public void IsValid_TitleTooLong()
        {
            test_item_report.Title = new string('D', 21);

            Assert.IsFalse(Validator.IsValid(test_item_report));
        }

        [TestMethod]
        public void IsValid_DescriptionTooLong()
        {
            test_item_report.Description = new string('D', 301);

            Assert.IsFalse(Validator.IsValid(test_item_report));
        }

        [TestMethod]
        public void IsValid_ItemPhotoNull()
        {
            test_item_report.ItemPhoto = null;

            Assert.IsFalse(Validator.IsValid(test_item_report));
        }

        [TestMethod]
        public void IsValid_ItemLocationNull()
        {
            test_item_report.ItemLocation = null;

            Assert.IsFalse(Validator.IsValid(test_item_report));
        }
    }
}
