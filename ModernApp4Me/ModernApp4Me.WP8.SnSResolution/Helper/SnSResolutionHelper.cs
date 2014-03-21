using System.Windows;

namespace ModernApp4Me.WP8.SnSResolution.Helper
{

    /// <summary>
    /// Helper that detects the resolution of the screen.
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.21</since> 
    public static class SnSResolutionHelper
    {

        /// <summary>
        /// Returns true if the resolution of the screen is WVGA.
        /// </summary>
        /// <returns></returns>
        private static bool IsWvga()
        {
           return Application.Current.Host.Content.ScaleFactor == 100;
        }

        /// <summary>
        /// Returns true if the resolution of the screen is WXGA.
        /// </summary>
        /// <returns></returns>
        private static bool IsWxga()
        {
            return Application.Current.Host.Content.ScaleFactor == 160;
        }

        /// <summary>
        /// Returns true if the resolution of the screen is 720P.
        /// </summary>
        /// <returns></returns>
        private static bool Is720P()
        {
            return Application.Current.Host.Content.ScaleFactor == 150;
        }

        /// <summary>
        /// Returns the resolution of the screen.
        /// </summary>
        public static SnSResolutionEnum CurrentResolution()
        {
            if (IsWvga())
            {
                return SnSResolutionEnum.WVGA;
            }

            if (IsWxga())
            {
                return SnSResolutionEnum.WXGA;
            }

            if (Is720P())
            {
                return SnSResolutionEnum.HD720P;
            }

            SnSLogger.Warn("Unknown resolution", "SnSMultiResolutionHelper", "CurrentResolution");
            return SnSResolutionEnum.WVGA;
        }

    }

}
