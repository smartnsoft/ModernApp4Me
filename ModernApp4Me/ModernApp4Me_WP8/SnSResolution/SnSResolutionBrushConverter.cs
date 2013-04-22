using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ModernApp4Me_WP8.SnSResolution
{
    /// <summary>
    /// Converter that calls the ResolutionImageChooser class according to the parameter.
    /// This converter permits an easy data binding.
    /// </summary>
    public sealed class SnSResolutionBrushConverter : IValueConverter
    {
        /// <summary>
        /// Converts the data binding parameter to the an ImageBrush.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
             return new ImageBrush { ImageSource = new BitmapImage(SnSResolutionImageChooser.BestResolutionImage((string)parameter)) };
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>null</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
