using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace wp4me.SnSMultiResolutionUtils
{
    /// <summary>
    /// Converter that call the MultiResImageChooser class according to the parameter.
    /// This converter permits an easy data binding.
    /// </summary>
    public sealed class SnSMultiResolutionBrushConverter : IValueConverter
    {
        /// <summary>
        /// This function call the BestResolutionImage function of the class SnSMultiResImageChooser and return a BitmapImage.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>the image to display</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
             return new ImageBrush { ImageSource = new BitmapImage(SnSMultiResImageChooser.BestResolutionImage((string)parameter)) };
        }

        /// <summary>
        /// Not implemented because not used.
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
