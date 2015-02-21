using System;
using System.Windows.Media;

namespace ModernApp4Me.WP8.Util
{

    /// <summary>
    /// A toolbox, that handle <see cref="Color"/> transformations and manipulations.
    /// </summary>
    /// 
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.24</since>
    public static class M4MColorToolbox
    {

        /// <summary>
        /// Converts an hexadecimal color to a <see cref="SolidColorBrush"/>.
        /// </summary>
        /// <param name="hexColor">the hexadecimal color code</param>
        /// <returns>the <see cref="SolidColorBrush"/> equivalent to the hexadecimal code</returns>
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
