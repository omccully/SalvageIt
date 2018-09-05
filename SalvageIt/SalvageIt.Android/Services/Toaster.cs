using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.CurrentActivity;
using SalvageIt.Services;
using Xamarin.Forms;
using SalvageIt.Droid.Services;

[assembly: Dependency(typeof(Toaster))]
namespace SalvageIt.Droid.Services
{
    public class Toaster : IToaster
    {
        public void DisplayMessage(string message)
        {
            Toast.MakeText(CrossCurrentActivity.Current.AppContext, 
                message, ToastLength.Long).Show();
        }

        public void DisplayError(string message)
        {
            DisplayMessage(message);
        }
    }
}