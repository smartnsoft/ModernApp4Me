using System;
using System.Globalization;
using System.Windows.Data;

namespace ModernApp4Me.WP8.Converter
{

    /// <summary>
    /// Enables to convert a timespan to seconds.
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.09.08</since>
    public sealed class M4MTimeSpanToSecondsConverter : IValueConverter
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
