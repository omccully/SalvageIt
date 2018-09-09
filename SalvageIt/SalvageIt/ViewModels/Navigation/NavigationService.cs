using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SalvageIt.ViewModels.Navigation
{
    using Translator;

    public class NavigationService : INavigationService
    {
        public ViewModel PreviousPageViewModel
        {
            get
            {
                Page prev_page = PreviousPage;
                
                return prev_page == null ? null : 
                    (prev_page.BindingContext as ViewModel);
            }
        }

        Page PreviousPage
        {
            get
            {
                try
                {
                    NavigationPage nav_page = (NavigationPage)Application.Current.MainPage;

                    int nav_stack_size = nav_page.Navigation.NavigationStack.Count;
                    return nav_page.Navigation
                        .NavigationStack[nav_stack_size - 2];
                }
                catch
                {
                    return null;
                }
            }
        }

        IViewModelViewTranslator ViewModelViewTranslator;
        public NavigationService(IViewModelViewTranslator translator)
        {
            this.ViewModelViewTranslator = translator;
        }

        public Task InitializeAsync()
        {
            return NavigateToAsync<MainViewModel>();
        }

        public async Task<TViewModel> NavigateToAsync<TViewModel>(object parameter = null) where TViewModel : ViewModel
        {
            Page page = CreatePage(typeof(TViewModel), parameter);

            var navigationPage = Application.Current.MainPage as NavigationPage;
            if (navigationPage != null)
            {
                await navigationPage.PushAsync(page);
            }
            else
            {
                Application.Current.MainPage = new NavigationPage(page);
            }

            TViewModel vm = page.BindingContext as TViewModel;
            await vm.InitializeAsync(parameter);

            return vm;
        }


        private Page CreatePage(Type vm_type, object parameter)
        {
            Type page_type = ViewModelViewTranslator.GetViewTypeForViewModel(vm_type);
            if (page_type == null)
            {
                throw new Exception($"Cannot locate page type for {vm_type}");
            }

            return Activator.CreateInstance(page_type) as Page;
        }

        public Task RemoveBackStackAsync()
        {
            NavigationPage nav_page = Application.Current.MainPage as NavigationPage;

            if (nav_page != null)
            {
                for (int i = 0; i < nav_page.Navigation.NavigationStack.Count - 1; i++)
                {
                    Page page = nav_page.Navigation.NavigationStack[i];
                    nav_page.Navigation.RemovePage(page);
                }
            }

            return Task.CompletedTask;
        }

        public Task RemoveLastFromBackStackAsync()
        {
            NavigationPage nav_page = Application.Current.MainPage as NavigationPage;

            if(nav_page != null)
            {
                nav_page.Navigation.RemovePage(PreviousPage);
            }

            return Task.CompletedTask;
        }

        public Task GoBackAsync(object parameter = null)
        {
            NavigationPage nav_page = Application.Current.MainPage as NavigationPage;

            if (nav_page != null)
            {
                ViewModel previous = PreviousPageViewModel;

                nav_page.Navigation.PopAsync();

                previous.ReturnToAsync(parameter);
            }

            return Task.CompletedTask;
        }
    }
}
