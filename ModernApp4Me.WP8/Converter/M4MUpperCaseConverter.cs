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

using System.Globalization;
using System.Windows.Data;

namespace ModernApp4Me.WP8.Converter
{

    /// <summary>
    /// Enables to convert a <see cref="string"/> to uppercase.
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.04.24</since>
    public sealed class M4MUpperCaseConverter : IValueConverter
    {

        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            var content = (string) value;
            return content.ToUpperInvariant();
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

    }

}
