using System;
using System.Collections.Generic;
using System.Text;

namespace SalvageIt.ViewModels.Navigation
{
    using System.Threading.Tasks;
    using ViewModels;

    public interface INavigationService
    {
        ViewModel PreviousPageViewModel { get; }
        Task InitializeAsync();
        Task<TViewModel> NavigateToAsync<TViewModel>(object parameter=null) where TViewModel : ViewModel;
        Task GoBackAsync(object parameter=null);
        Task RemoveLastFromBackStackAsync();
        Task RemoveBackStackAsync();
    }
}
