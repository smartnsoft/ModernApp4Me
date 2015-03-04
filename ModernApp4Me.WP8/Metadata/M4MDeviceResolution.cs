// Copyright (C) 2015 Smart&Soft SAS (http://www.smartnsoft.com/) and contributors
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 3 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details.
//
// Contributors:
//   Smart&Soft - initial API and implementation

using System;
using System.Windows;

namespace ModernApp4Me.WP8.Metadata
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
