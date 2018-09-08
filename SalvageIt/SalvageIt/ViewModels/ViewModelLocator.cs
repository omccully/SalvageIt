using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace SalvageIt.ViewModels
{
    public static class ViewModelLocator
    {
        static string ViewFullNameToViewModelFullName(string view_full_name)
        {
            string vm_namespace = view_full_name.Replace(".Views.", ".ViewModels.");

            Regex r = new Regex("Page$");
            string page_removed = r.Replace(vm_namespace, delegate { return ""; });

            return page_removed + "ViewModel";
        }

        static Type GetViewModelTypeFromViewType(Type view_type)
        {
            string vm_fullname = ViewFullNameToViewModelFullName(view_type.FullName);
            string assembly_fullname = view_type.GetTypeInfo().Assembly.FullName;

            return Type.GetType($"{vm_fullname}, {assembly_fullname}");
        }

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            Element view = bindable as Element;
            if (view == null)
            {
                System.Diagnostics.Debug.WriteLine(
                    "OnAutoWireViewModelChanged: bindable isn't a view");
                return;
            }

            Type view_type = view.GetType();
            Type vm_type = GetViewModelTypeFromViewType(view_type);
            if (vm_type == null)
            {
                System.Diagnostics.Debug.WriteLine(
                    "OnAutoWireViewModelChanged: vm_type not resolved for " + view_type.FullName);
                return;
            }

            return;
            //object vm = _container.Resolve(vm_type);
            //view.BindingContext = vm;
        }
    }
}
