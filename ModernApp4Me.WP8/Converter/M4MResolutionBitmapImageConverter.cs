using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using ModernApp4Me.WP8.Image;

namespace ModernApp4Me.WP8.Converter
{

    /// <summary>
    /// Enables to choose the best <see cref="BitmapImage"/> from the assets according to device resolution.
    /// This class uses the <see cref="M4MResolutionImageChooser"/> to take its choice.
    /// The parameter should be something like "Path/to/Image/ImageName-Resolution.Extension" where resolution can be HD, WXGA or WVGA.
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.21</since> 
    public sealed class M4MResolutionBitmapImageConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new BitmapImage(M4MResolutionImageChooser.GetBestResolutionImage((string)parameter));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

    }

}
