using System;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace ModernApp4Me.Win8.SnSUtil
{

    /// <summary>
    /// Class that provides functions to deal with colors.
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.24</since>
    public static class SnSColorTranslator
    {

        /// <summary>
        /// Converts an hexadecimal color to a SolidColorBrush.
        /// </summary>
        /// <param name="hexColor"></param>
        /// <returns></returns>
        public static SolidColorBrush ColorFromHex(string hexColor)
        {
            return new SolidColorBrush(
                Color.FromArgb(
                    Convert.ToByte(hexColor.Substring(1, 2), 16),
                    Convert.ToByte(hexColor.Substring(3, 2), 16),
                    Convert.ToByte(hexColor.Substring(5, 2), 16),
                    Convert.ToByte(hexColor.Substring(7, 2), 16)
                )
            );
        }

    }

}
