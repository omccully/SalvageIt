﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SalvageIt.Views
{
    using Services;
    using ViewModels;
    using Models;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ReportItemPage : ContentPage
    { 
        public ReportItemPage ()
		{
			InitializeComponent ();
        }
    }
}