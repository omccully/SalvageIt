using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SalvageIt
{
    using Services;

    class DateTimeToTimeAgoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            TimeSpan time_since = (DateTime.Now - (DateTime)value);
            return time_since.ToUserFriendlyString() + " ago";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
