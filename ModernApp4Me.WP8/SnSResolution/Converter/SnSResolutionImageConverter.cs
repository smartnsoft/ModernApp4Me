using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using ModernApp4Me.WP8.SnSResolution.Chooser;

namespace ModernApp4Me.WP8.SnSResolution.Converter
{

    /// <summary>
    /// Converter that calls the ResolutionImageChooser class according to the parameter.
    /// This converter permits an easy data binding.
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.21</since> 
    public sealed class SnSResolutionImageConverter : IValueConverter
    {

        /// <summary>
        /// Converts the data binding parameter to the an BitmapImage.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>The bitmap image</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new BitmapImage(SnSResolutionImageChooser.BestResolutionImage((string)parameter));
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

    }

}
