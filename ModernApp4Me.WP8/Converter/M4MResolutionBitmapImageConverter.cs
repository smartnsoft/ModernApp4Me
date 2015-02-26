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
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using ModernApp4Me.WP8.Util;

namespace ModernApp4Me.WP8.Converter
{

    /// <summary>
    /// Enables to choose the best <see cref="BitmapImage"/> from the assets according to device resolution.
    /// This class uses the <see cref="M4MResolutionImageChooser"/> to take its choice.
    /// The parameter should be something like "Path/to/Image/ImageName-Resolution.Extension" where resolution can be HD, WXGA or WVGA.
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.21</since> 
    public sealed class M4MResolutionBitmapImageConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new BitmapImage(M4MResolutionImageChooser.GetBestResolutionImage((string)parameter));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

    }

}
