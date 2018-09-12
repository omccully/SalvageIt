using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Unity;
using Unity.Registration;
using Unity.Resolution;
using Unity.Injection;

namespace SalvageIt.ViewModels
{
    using Services;
    using Models;
    using SalvageIt.Models.Validators;
    using Translator;
    using Navigation;
    using Unity.Lifetime;

    public static class ViewModelLocator
    {
        static IUnityContainer _container;

        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", 
                typeof(bool), typeof(ViewModelLocator), default(bool), 
                propertyChanged: OnAutoWireViewModelChanged);

        static ViewModelLocator()
        { 
            // register
            _container = new UnityContainer();

            _container.RegisterType<IGeolocator, Geolocator>(new InjectionConstructor());
            _container.RegisterType<IPictureTaker, PictureTaker>();
            _container.RegisterType<IPictureSelector, PictureSelector>();
            _container.RegisterType<IValidator<ItemReport>, SubmitItemReportValidator>();
            _container.RegisterType<IViewModelViewTranslator, ViewModelViewTranslator>();
            _container.RegisterType<INavigationService, NavigationService>();
            _container.RegisterType<IMapsNavigation, MapsNavigation>();
            _container.RegisterInstance<IToaster>(DependencyService.Get<IToaster>());
            _container.RegisterType<ItemReportStorage, VolatileItemReportStorage>(
                new ContainerControlledLifetimeManager());

#if DEBUG
            var uis = new UriImageSource();
            uis.Uri = new Uri("http://png.icons8.com/metro/1600/settings.png");
            var irs1 = _container.Resolve<ItemReportStorage>();

            irs1.SubmitItem(new ItemReport()
            {
                Title = "vml item",
                ItemPhoto = uis,
                ItemLocation = new LocationCoordinates(42.2440242, -83.6222102),
                ReportTime = new DateTime(2018, 9, 8)
            });
#endif
        }

        public static INavigationService NavigationService
        {
            get
            {
                return _container.Resolve<INavigationService>();
            }
        }

        static IViewModelViewTranslator ViewModelViewTranslator
        {
            get
            {
                return _container.Resolve<IViewModelViewTranslator>();
            }
        }

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(AutoWireViewModelProperty, value);
        }

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            bindable.BindingContext = CreateViewModelForPage(bindable as Page);

            // bind cp.Title to vm.Title
            ContentPage cp = bindable as ContentPage;
            if (cp != null)
            {
                cp.SetBinding(ContentPage.TitleProperty, "Title");
            }
        }

        public static ViewModel CreateViewModelForPage(Page page)
        {
            if (page == null)
            {
                throw new ArgumentNullException("page is null");
            }

            Type page_type = page.GetType();
            Type vm_type = ViewModelViewTranslator.GetViewModelTypeForView(page_type);
            if (vm_type == null)
            {
                // this should never happen. programmer error.
                throw new ArgumentException("VM not found for " + page_type);
            }

            ViewModel vm = (ViewModel)_container.Resolve(vm_type);
            vm.NavigationService = NavigationService;
            return vm;
        }
    }
}
