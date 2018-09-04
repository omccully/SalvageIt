using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Plugin.Geolocator.Abstractions;

namespace SalvageIt.Models
{
    interface IItemReport
    {
        int ID { get; set; }
        Position ItemLocation { get; set; }
        ImageSource ItemImage { get; set; }
        DateTime ReportTime { get; set; }
    }
}
