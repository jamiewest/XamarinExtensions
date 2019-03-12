using System;
using System.Globalization;
using Xamarin.Forms;

namespace West.Extensions.Xamarin
{
    internal class LowercaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string output;

            if (value is string input)
            {
                output = input?.ToLowerInvariant();
            }
            else
            {
                throw new ArgumentException("Value must be string.", nameof(value));
            }

            return output;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
