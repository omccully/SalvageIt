using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SalvageIt.ViewModels
{
    using Navigation;
    using System.Threading.Tasks;

    public abstract class ViewModel : INotifyPropertyChanged
    {
        // Most code here is not mine. It gets generated when you 
        // create a Xamarin.Forms app and you select the MVVM example 

        public INavigationService NavigationService { get; set; }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public virtual Task InitializeAsync(object navigation_data)
        {
            return Task.CompletedTask;
        }

        public virtual Task ReturnToAsync(object return_data)
        {
            return Task.CompletedTask;
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, 
                new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
