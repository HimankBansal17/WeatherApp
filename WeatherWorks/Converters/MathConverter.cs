using System;
using System.Globalization;
using System.Windows.Data;

namespace WeatherWorks.Converters
{
    public class MathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
              object parameter, CultureInfo culture)
        {
            string? s = parameter.ToString();
            return (double)value + int.Parse(s);
        }

        public object? ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
