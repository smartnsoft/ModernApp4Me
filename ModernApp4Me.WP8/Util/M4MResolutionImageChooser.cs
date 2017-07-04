// The MIT License (MIT)
//
// Copyright (c) 2017 Smart&Soft
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.Text;
using ModernApp4Me.WP8.Metadata;

namespace ModernApp4Me.WP8.Util
{

    /// <summary>
    /// Chooses the best image from the assets according to the device resolution.
    /// </summary>
    /// 
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.21</since>
    // Inspired by https://msdn.microsoft.com/en-us/library/windows/apps/jj206974%28v=vs.105%29.aspx
    public static class M4MResolutionImageChooser
    {

        public static Uri GetBestResolutionImage(string imageName)
        {
            var array = imageName.Split('.');
            var imageNameBase = array[0];
            var imageExtension = array[1];
            var imageNameChooser = new StringBuilder(imageNameBase);

            switch (M4MDeviceResolution.CurrentResolution)
            {
                case M4MDeviceResolution.Resolution.HD:
                    imageNameChooser.Append("-HD.");
                    break;

                case M4MDeviceResolution.Resolution.WXGA:
                    imageNameChooser.Append("-WXGA.");
                    break;

                case M4MDeviceResolution.Resolution.WVGA:
                    imageNameChooser.Append("-WVGA.");
                    break;

                default :
                    imageNameChooser.Append("-WVGA.");
                    break;
            }

            imageNameChooser.Append(imageExtension);

            return new Uri(imageNameChooser.ToString(), UriKind.Relative);
        }

    }

}
