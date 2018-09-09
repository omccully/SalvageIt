using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace SalvageIt.ViewModels.Translator
{
    /// <summary>
    /// Assumes:
    /// Views.{0}Page
    /// ViewModels.{0}ViewModel
    /// </summary>
    public class ViewModelViewTranslator : IViewModelViewTranslator
    {
        public Type GetViewModelTypeForView(Type view_type)
        {
            string vm_fullname = ViewFullNameToViewModelFullName(view_type.FullName);
            string assembly_fullname = view_type.GetTypeInfo().Assembly.FullName;

            return Type.GetType($"{vm_fullname}, {assembly_fullname}");
        }

        public string ViewFullNameToViewModelFullName(string view_full_name)
        {
            string vm_namespace = view_full_name.Replace(".Views.", ".ViewModels.");

            Regex r = new Regex("Page$");
            string page_removed = r.Replace(vm_namespace, "");

            return page_removed + "ViewModel";
        }



        public Type GetViewTypeForViewModel(Type vm_type)
        {
            string view_full_name = ViewModelFullNameToViewFullName(vm_type.FullName);
            var assembly_name = vm_type.GetTypeInfo().Assembly.FullName;

            return Type.GetType($"{view_full_name}, {assembly_name}");
        }

        public string ViewModelFullNameToViewFullName(string vm_full_name)
        {
            string v_namespace = vm_full_name.Replace(".ViewModels.", ".Views.");

            Regex r = new Regex("ViewModel$");
            string page_removed = r.Replace(v_namespace, "Page");

            return page_removed;
        }
    }
}
