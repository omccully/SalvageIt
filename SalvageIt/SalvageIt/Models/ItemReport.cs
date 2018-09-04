using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SalvageIt.Models
{
    class ItemReport : IItemReport
    {
        public int ID { get; set; }
        public Position ItemLocation { get; set; }
        public ImageSource ItemImage { get; set; }
        public DateTime ReportTime { get; set; }
    }
}
