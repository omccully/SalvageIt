using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SalvageIt.Models
{
    using Services;

    public class ItemReport
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public LocationCoordinates ItemLocation { get; set; }
        public ImageSource ItemPhoto { get; set; }
        public DateTime ReportTime { get; set; }
        public DateTime EditTime { get; set; }

        public bool IsMine { get; set; } = false;


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Item #{ID}");
            if (!String.IsNullOrWhiteSpace(Title))
            {
                sb.AppendLine(Title);
            }
            if (!String.IsNullOrWhiteSpace(Description))
            {
                sb.AppendLine(Description);
            }

            sb.AppendLine($"Location: {ItemLocation}")
                .AppendLine($"Time: {ReportTime}");

            return sb.ToString();
        }
    }
}
