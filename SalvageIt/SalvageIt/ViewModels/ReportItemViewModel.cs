using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SalvageIt.ViewModels
{
    using Services;
    using Models;
    using Views;
    using Navigation;
    using Models.Validators;

    public class ReportItemViewModel : ViewModel
    {
        IPictureTaker PictureTaker;
        IToaster Toaster;
        ItemReportStorage ItemReportStorage;

        public ReportItemViewModel(IPictureTaker picture_taker, 
            IToaster toaster, ItemReportStorage item_report_storage)
        {
            this.PictureTaker = picture_taker;
            this.Toaster = toaster;
            this.ItemReportStorage = item_report_storage;

            this.Title = "Create new item report";
        }

        public event EventHandler<ItemReportEventArgs> ReportItemSubmitted;
        protected void OnReportItemSubmitted(ItemReport item_report)
        {
            ReportItemSubmitted?.Invoke(this, new ItemReportEventArgs(item_report));
        }

        #region Item geolocator
        ICommand _SelectLocationCommand;
        public ICommand SelectLocationCommand
        {
            get
            {
                return _SelectLocationCommand ??
                      (_SelectLocationCommand = new Command(SelectLocationAction));
            }
        }

        async void SelectLocationAction()
        {
            //SelectLocationPage select_loc_page = new SelectLocationPage();
            //select_loc_page.LocationChosen += Select_loc_page_LocationChosen;
            //await Navigation.PushAsync(select_loc_page);

            await NavigationService.NavigateToAsync<SelectLocationViewModel>();
        }

        public override Task ReturnToAsync(object return_data)
        {
            SelectedLocation = (LocationCoordinates)return_data;
            return Task.CompletedTask;
        }

        string _SelectedLocationText;
        public string SelectedLocationText
        {
            get
            {
                return _SelectedLocationText;
            }
            private set
            {
                SetProperty(ref _SelectedLocationText, value,
                    "SelectedLocationText");
            }
        }

        LocationCoordinates _SelectedLocation;
        public LocationCoordinates SelectedLocation
        {
            get
            {
                return _SelectedLocation;
            }
            set
            {
                SetProperty(ref _SelectedLocation, value,
                    "SelectedLocation");
                SelectedLocationText = value.ToString();
            }
        }
        #endregion

        #region Select image
        ICommand _CameraButtonCommand;
        public ICommand CameraButtonCommand
        {
            get
            {
                return _CameraButtonCommand ??
                    (_CameraButtonCommand = new Command(CameraButtonAction));
            }
        }

        async void CameraButtonAction()
        {
            PhotoSelection = await PictureTaker.TakePicture();
        }

        ICommand _SelectPhotoCommand;
        public ICommand SelectPhotoCommand
        {
            get
            {
                return _SelectPhotoCommand ??
                    (_SelectPhotoCommand = new Command(SelectPhotoAction));
            }
        }

        async void SelectPhotoAction()
        {
            PhotoSelection = await PictureSelector.SelectPicture();
        }

        string _PhotoStatusText = "No picture taken yet";
        public string PhotoStatusText {
            get
            {
                return _PhotoStatusText;
            }
            private set
            {
                SetProperty(ref _PhotoStatusText, value,
                   "PhotoStatusText");
            }
        } 

        ImageSource _PhotoSelection;
        public ImageSource PhotoSelection
        {
            get
            {
                return _PhotoSelection;
            }
            set
            {
                SetProperty(ref _PhotoSelection, value,
                    "PhotoSelection");

                if (value != null)
                {
                    PhotoStatusText = "Picture taken successfully";
                }
                else
                {
                    PhotoStatusText = "Picture failed";
                }
            }
        }
        #endregion

        string _TitleInputText;
        public string TitleInputText
        {
            get
            {
                return _TitleInputText;
            }
            set
            {
                SetProperty(ref _TitleInputText, value,
                    "TitleInputText");
            }
        }

        string _DescriptionInputText;
        public string DescriptionInputText
        {
            get
            {
                return _DescriptionInputText;
            }
            set
            {
                SetProperty(ref _DescriptionInputText, value,
                    "DescriptionInputText");
            }
        }

        ICommand _SubmitButtonCommand;
        public ICommand SubmitButtonCommand
        {
            get
            {
                return _SubmitButtonCommand ??
                    (_SubmitButtonCommand = new Command(SubmitButtonAction));
            }
        }

        async void SubmitButtonAction()
        {
            ItemReport ir = new ItemReport()
            {
                Title = TitleInputText,
                Description = DescriptionInputText,
                ItemLocation = SelectedLocation,
                ItemPhoto = PhotoTaken,
                ReportTime = DateTime.Now
            };

            System.Diagnostics.Debug.WriteLine(ir);

            try
            {
                int ID = await ItemReportStorage.SubmitItem(ir);
                await NavigationService.GoBackAsync(ir);
                OnReportItemSubmitted(ir);
            }
            catch(Exception e)
            {
                Toaster.DisplayError(e.Message);
            }
        }
    }
}
