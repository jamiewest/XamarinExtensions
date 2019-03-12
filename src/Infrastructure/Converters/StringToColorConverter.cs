using System;
using System.Globalization;
using Xamarin.Forms;

namespace West.Extensions.Xamarin
{
    public class StringToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return Color.Default;
            }

            var valueAsString = value.ToString();
            switch (valueAsString)
            {
                case "":
                    {
                        return Color.Default;
                    }
                case "Accent":
                    {
                        return Color.Accent;
                    }
                default:
                    {
                        return Color.FromHex(value.ToString());
                    }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}