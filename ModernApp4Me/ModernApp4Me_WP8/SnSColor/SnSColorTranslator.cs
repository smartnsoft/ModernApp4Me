using System;
using System.Windows.Media;

namespace ModernApp4Me_WP8.SnSColor
{
    /// <summary>
    /// Class that provides functions to deal with colors.
    /// </summary>
    public static class SnSColorTranslator
    {
        /*******************************************************/
        /** METHODS.
        /*******************************************************/
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
