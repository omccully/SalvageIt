﻿using System;
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
            _container.RegisterType<ItemReportStorage, VolatileItemReportStorage>();
            _container.RegisterType<IValidator<ItemReport>, SubmitItemReportValidator>();
            _container.RegisterInstance<IToaster>(DependencyService.Get<IToaster>());
            _container.RegisterType<IViewModelViewTranslator, ViewModelViewTranslator>();
            _container.RegisterType<INavigationService, NavigationService>();

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

        /*static string ViewFullNameToViewModelFullName(string view_full_name)
        {
            string vm_namespace = view_full_name.Replace(".Views.", ".ViewModels.");

            Regex r = new Regex("Page$");
            string page_removed = r.Replace(vm_namespace, delegate { return ""; });

            return page_removed + "ViewModel";
        }

        static Type GetViewModelTypeForPage(Type view_type)
        {
            string vm_fullname = ViewFullNameToViewModelFullName(view_type.FullName);
            string assembly_fullname = view_type.GetTypeInfo().Assembly.FullName;

            return Type.GetType($"{vm_fullname}, {assembly_fullname}");
        }*/

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