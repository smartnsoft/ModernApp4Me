using System.Globalization;
using System.Windows.Data;

namespace ModernApp4Me.WP7.SnSConverter
{

    /// <author>Ludovic ROLAND</author>
    /// <since>2014.04.24</since>
    /// <summary>Converter to use in a Control to display the content in uppercase (because of charactercasing does not always exist).</summary>
    public sealed class SnSUpperCaseConverter : IValueConverter
    {

        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            var stringToUpperCase = (string) value;

            return stringToUpperCase.ToUpperInvariant();
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

    }

}
