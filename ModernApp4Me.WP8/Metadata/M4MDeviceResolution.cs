using System;
using System.Text;
using System.Windows;

namespace ModernApp4Me.WP8.Resolution.Helper
{

    /// <summary>
    /// Provides information about the device resolution.
    /// </summary>
    /// 
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.21</since> 
    // Inspired by https://msdn.microsoft.com/en-us/library/windows/apps/jj206974%28v=vs.105%29.aspx
    public static class M4MDeviceResolution
    {

        public enum Resolution
        {
            WVGA, WXGA, HD
        }

        private static bool IsWvga()
        {
           return Application.Current.Host.Content.ScaleFactor == 100;
        }

        private static bool IsWxga()
        {
            return Application.Current.Host.Content.ScaleFactor == 160;
        }

        private static bool IsHD()
        {
            return Application.Current.Host.Content.ScaleFactor == 150;
        }

        public static Resolution CurrentResolution
        {
            get
            {
                if (IsWvga() == true)
                {
                    return Resolution.WVGA;
                }

                if (IsWxga() == true)
                {
                    return Resolution.WXGA;
                }

                if (IsHD() == true)
                {
                    return Resolution.HD;
                }

                throw new InvalidOperationException("Unknown resolution");
            }
        }

    }

}
