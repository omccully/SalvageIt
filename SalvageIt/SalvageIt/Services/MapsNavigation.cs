using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SalvageIt.Services
{
    using Models;
    using System.Net;

    public class MapsNavigation : IMapsNavigation
    {
        public void NavigateToCoordinates(LocationCoordinates coords)
        {
            Device.OpenUri(GetMapsUri(coords));
        }

        public void NavigateToAddress(string address)
        {
            Device.OpenUri(GetMapsUri(address));
        }

        Uri GetMapsUri(LocationCoordinates coords)
        {
            return GetMapsUri(coords.ToString());
        }

        Uri GetMapsUri(string address)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    return new Uri(string.Format("http://maps.apple.com/?q={0}",
                        WebUtility.UrlEncode(address)));
                case Device.Android:
                    return new Uri(string.Format("geo:0,0?q={0}",
                        WebUtility.UrlEncode(address)));
                case Device.UWP:
                    return new Uri(string.Format("bingmaps:?where={0}",
                        Uri.EscapeDataString(address)));
            }
            return null;
        }
    }
}
