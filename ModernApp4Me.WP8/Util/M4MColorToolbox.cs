// Copyright (C) 2015 Smart&Soft SAS (http://www.smartnsoft.com/) and contributors
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details.
//
// Contributors:
//   Smart&Soft - initial API and implementation

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
