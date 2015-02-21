using System.Globalization;
using System.Windows.Data;

namespace ModernApp4Me.WP8.Converter
{

    /// <summary>
    /// Enables to convert a <see cref="string"/> to uppercase.
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.04.24</since>
    public sealed class M4MUpperCaseConverter : IValueConverter
    {

        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            var content = (string) value;
            return content.ToUpperInvariant();
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

    }

}
