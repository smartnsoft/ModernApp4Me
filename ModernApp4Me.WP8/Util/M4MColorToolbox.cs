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
