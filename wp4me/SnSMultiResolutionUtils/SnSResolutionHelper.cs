using System;
using System.Windows;

namespace wp4me.SnSMultiResolutionUtils
{
    /// <summary>
    /// Enumeration
    /// </summary>
    public enum Resolutions { WVGA, WXGA, HD720P };


    /// <summary>
    /// Class that provides functions to work with the phone's resolution.
    /// </summary>
    public static class ResolutionHelper
    {
        /*******************************************************/
        /** PRIVATE PROPERTIES.
        /*******************************************************/
        private static bool IsWvga
        {
            get
            {
                return Application.Current.Host.Content.ScaleFactor == 100;
            }
        }

        private static bool IsWxga
        {
            get
            {
                return Application.Current.Host.Content.ScaleFactor == 160;
            }
        }

        private static bool Is720P
        {
            get
            {
                return Application.Current.Host.Content.ScaleFactor == 150;
            }
        }


        /*******************************************************/
        /** PUBLIC PROPERTIES.
        /*******************************************************/
        public static Resolutions CurrentResolution
        {
            get
            {
                if (IsWvga) return Resolutions.WVGA;
                if (IsWxga) return Resolutions.WXGA;
                if (Is720P) return Resolutions.HD720P;
                
                throw new InvalidOperationException("Unknown resolution");
            }
        }
    }
}
