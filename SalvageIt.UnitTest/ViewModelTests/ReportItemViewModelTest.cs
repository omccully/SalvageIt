using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalvageIt.ViewModels;
using SalvageIt.Models;
using Moq;
using Xamarin.Forms;
using SalvageIt.Services;
using System.Threading.Tasks;

namespace SalvageIt.UnitTest.ViewModelTests
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

        INavigation nav;
        Mock<INavigation> nav_mock;




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
            nav_mock = new Mock<INavigation>();
            nav = nav_mock.Object;

            toaster_mock = new Mock<IToaster>();
            toaster = toaster_mock.Object;

            item_report_storage = new VolatileItemReportStorage();
        }

        [TestMethod]
        public async Task EnterAllDataAndSubmit()
        {
            string title_input = "A couch I found";
            string description_input = "Looks alright";

            ReportItemViewModel vm = new ReportItemViewModel(
                nav, pic_taker, null, item_report_storage);

            vm.TitleInputText = title_input;
            vm.DescriptionInputText = description_input;

            // IPictureTaker should return an ImageSource
            // to set PhotoTaken asynchronously
            vm.CameraButtonCommand.Execute(null);

            vm.SelectedLocation = MicrosoftHQ;

            // give time for async picture
            //System.Threading.Thread.Sleep(100);
            System.Diagnostics.Debug.WriteLine("BeforeTakePic");
            await pic_taker.TakePicture();
            System.Diagnostics.Debug.WriteLine("AfterTakePic");

            ItemReport item_report_from_event = null;
            vm.ReportItemSubmitted += (s, e) =>
            {
                item_report_from_event = e.ItemReport;
            };
            vm.SubmitButtonCommand.Execute(null);

            
            nav_mock.Verify(n => n.PopAsync());

            // make sure the ItemReport that was put in storage
            // matches the input
            Assert.IsTrue(item_report_storage.ItemReports.Count == 1);
            ItemReport ir = item_report_storage.ItemReports[0];
            Assert.IsTrue(ir.Title == title_input);
            Assert.IsTrue(ir.Description == description_input);
            Assert.IsTrue(ir.ItemLocation == MicrosoftHQ);
            Assert.IsTrue(ir.ItemPhoto == ImageSourceExample);

            // make sure the ItemReport from event is the same
            // as the one found in storage
            Assert.IsTrue(item_report_from_event != null &&
                item_report_from_event == ir);


            // TODO: test to make sure INotifyPropertyChanged is working
        }

        [TestMethod]
        public void MissingTitleSubmit()
        {
            // keep ItemReportStorage and Navigation = null
            ReportItemViewModel vm = new ReportItemViewModel(
                null, pic_taker, toaster, null);

            

            vm.DescriptionInputText = "Looks alright";

            // IPictureTaker should return an ImageSource
            // to set PhotoTaken asynchronously
            vm.CameraButtonCommand.Execute(null);

            vm.SelectedLocation = MicrosoftHQ;

            vm.SubmitButtonCommand.Execute(null);
            

            toaster_mock.Verify(t => t.DisplayError("You must enter a title"));
        }
    }
}
