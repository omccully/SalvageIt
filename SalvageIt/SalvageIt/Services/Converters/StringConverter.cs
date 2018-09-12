using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

// This needs to be in the base namespace to be accessed from XAML?
namespace SalvageIt.Services.Converters
{
    public class StringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
