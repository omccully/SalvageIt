using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalvageIt.ViewModels;
using SalvageIt.Models;
using Moq;
using Xamarin.Forms;
using SalvageIt.Services;
using System.Threading.Tasks;
using SalvageIt.ViewModels.Navigation;
using System;
using SalvageIt.Models.Validators;

namespace SalvageIt.UnitTest.ViewModels
{
    class ImageSourceExample : ImageSource
    {
        public ImageSourceExample()
        {

        }
    }

    [TestClass]
    public class ReportItemViewModelTest
    {
        LocationCoordinates MicrosoftHQ =
            new LocationCoordinates(47.6424779, -122.136859);

        INavigationService nav;
        Mock<INavigationService> nav_mock;




        IPictureTaker pic_taker = Mock.Of<IPictureTaker>(p =>
            p.TakePicture() == ImageSourceTask);
        static ImageSource ImageSourceExample = new ImageSourceExample();
        static Task<ImageSource> ImageSourceTask = new Task<ImageSource>(ReturnExampleImageSource);
        static ImageSource ReturnExampleImageSource()
        {
            return ImageSourceExample;
        }

        IToaster toaster;
        Mock<IToaster> toaster_mock;

        VolatileItemReportStorage item_report_storage; 


        [TestInitialize()]
        public void Startup()
        {
            nav_mock = new Mock<INavigationService>();
            nav = nav_mock.Object;

            toaster_mock = new Mock<IToaster>();
            toaster = toaster_mock.Object;

            item_report_storage = new VolatileItemReportStorage();
        }

        [TestMethod]
        public void EnterAllDataAndSubmit()
        {
            // EnterAllDataAndSubmitAsync().GetAwaiter().GetResult();
            EnterAllDataAndSubmitAsync();
        }

        void EnterAllDataAndSubmitAsync()
        {
            string title_input = "A couch I found";
            string description_input = "Looks alright";

            ReportItemViewModel vm = new ReportItemViewModel(
                pic_taker, null, null, null, item_report_storage);
            vm.NavigationService = nav;

            vm.TitleInputText = title_input;
            vm.DescriptionInputText = description_input;

            // IPictureTaker should return an ImageSource
            // to set PhotoTaken asynchronously
            //vm.CameraButtonCommand.Execute(null);
            vm.PhotoSelection = ImageSourceExample;

            vm.SelectedLocation = MicrosoftHQ;

            // give time for async picture
            //System.Threading.Thread.Sleep(100);
            System.Diagnostics.Debug.WriteLine("BeforeTakePic");
            //await pic_taker.TakePicture();
            System.Diagnostics.Debug.WriteLine("AfterTakePic");

            /*ItemReport item_report_from_event = null;
            vm.ReportItemSubmitted += (s, e) =>
            {
                item_report_from_event = e.ItemReport;
            };*/
            vm.SubmitButtonCommand.Execute(null);

            
            //nav_mock.Verify(n => n.GoBackAsync(item_report_from_event));

            // make sure the ItemReport that was put in storage
            // matches the input
            Assert.AreEqual(1, item_report_storage.LocalItemReports.Count);

            ItemReport ir = item_report_storage.LocalItemReports[0];

            Assert.AreEqual(title_input, ir.Title);
            Assert.AreEqual(description_input, ir.Description);
            Assert.AreEqual(MicrosoftHQ, ir.ItemLocation);
            Assert.AreEqual(ImageSourceExample, ir.ItemPhoto);

            // make sure the ItemReport from event is the same
            // as the one found in storage
            //Assert.IsNotNull(item_report_from_event);
            //Assert.AreEqual(ir, item_report_from_event);


            // TODO: test to make sure INotifyPropertyChanged is working
        }

        [TestMethod]
        public void ExceptionThrownWhenSubmitting()
        {
            ReportItemViewModel vm = new ReportItemViewModel(
                pic_taker, null, toaster, null, null);

            vm.DescriptionInputText = "Looks alright";

            // IPictureTaker should return an ImageSource
            // to set PhotoTaken asynchronously
            vm.CameraButtonCommand.Execute(null);

            vm.SelectedLocation = MicrosoftHQ;

            vm.SubmitButtonCommand.Execute(null);
            
            // ItemReportStorage not set, so throws exception
            toaster_mock.Verify(t => t.DisplayError(
                "Object reference not set to an instance of an object."));
        }
    }
}
