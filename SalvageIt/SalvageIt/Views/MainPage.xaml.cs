using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SalvageIt.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void OnReportItem(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new ReportItemPage());
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                throw ex;
            }
        }
    }
}
