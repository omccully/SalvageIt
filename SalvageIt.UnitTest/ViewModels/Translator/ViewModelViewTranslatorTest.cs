using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalvageIt.ViewModels;
using SalvageIt.ViewModels.Translator;
using SalvageIt.Views;

namespace SalvageIt.UnitTest.ViewModels.Translator
{
    [TestClass]
    public class ViewModelViewTranslatorTest
    {
        [TestMethod]
        public void GetViewTypeForViewModel()
        {
            var translator = new ViewModelViewTranslator();

            Assert.AreEqual(typeof(MainPage),
                translator.GetViewTypeForViewModel(typeof(MainViewModel)));
            Assert.AreEqual(typeof(ReportItemPage),
               translator.GetViewTypeForViewModel(typeof(ReportItemViewModel)));
        }

        [TestMethod]
        public void ViewModelFullNameToViewFullName()
        {
            var translator = new ViewModelViewTranslator();

            Assert.AreEqual("SalvageIt.Views.MainPage",
                translator.ViewModelFullNameToViewFullName(
                    "SalvageIt.ViewModels.MainViewModel"));

            Assert.AreEqual("SalvageIt.Views.ReportItemPage",
                translator.ViewModelFullNameToViewFullName(
                    "SalvageIt.ViewModels.ReportItemViewModel"));
        }


        [TestMethod]
        public void GetViewModelTypeForView()
        {
            var translator = new ViewModelViewTranslator();
            Assert.AreEqual(typeof(MainViewModel),
                translator.GetViewModelTypeForView(typeof(MainPage)));
            Assert.AreEqual(typeof(ReportItemViewModel),
                translator.GetViewModelTypeForView(typeof(ReportItemPage)));
        }

        [TestMethod]
        public void ViewFullNameToViewModelFullName()
        {
            var translator = new ViewModelViewTranslator();
            
            Assert.AreEqual("SalvageIt.ViewModels.MainViewModel",
                translator.ViewFullNameToViewModelFullName(
                   "SalvageIt.Views.MainPage"));
            
            Assert.AreEqual("SalvageIt.ViewModels.ReportItemViewModel",
                translator.ViewFullNameToViewModelFullName(
                   "SalvageIt.Views.ReportItemPage"));
        }
    }
}
