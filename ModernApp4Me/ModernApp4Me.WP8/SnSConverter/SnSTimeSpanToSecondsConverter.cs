using System;
using System.Globalization;
using System.Windows.Data;

namespace ModernApp4Me.WP8.SnSConverter
{

    /// <author>Ludovic ROLAND</author>
    /// <since>2014.09.08</since>
    /// <summary>Converter to use in a Control in order to convert a timespan to seconds.</summary>
    public sealed class TimeSpanToSecondsConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var timespan = (TimeSpan)value;

            return System.Convert.ToInt32(timespan.TotalSeconds);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

    }

}
