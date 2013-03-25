using System;
using System.Windows.Media;

namespace wp4me.SnSColorUtils
{
    /// <summary>
    /// Class that provides functions to deal with colors.
    /// </summary>
    public sealed class SnSColorTranslator
    {
        /*******************************************************/
        /** METHODS AND FUNCTIONS.
        /*******************************************************/
        public static SolidColorBrush ColorFromHtml(string htmlColor)
        {
            return new SolidColorBrush(
                Color.FromArgb(
                    Convert.ToByte(htmlColor.Substring(1, 2), 16),
                    Convert.ToByte(htmlColor.Substring(3, 2), 16),
                    Convert.ToByte(htmlColor.Substring(5, 2), 16),
                    Convert.ToByte(htmlColor.Substring(7, 2), 16)
                )
            );
        }
    }
}
