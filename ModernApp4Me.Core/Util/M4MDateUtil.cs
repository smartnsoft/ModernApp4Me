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

namespace ModernApp4Me.Core.Util
{

    /// <summary>
    /// A class which offers static methods in order to manipulate date.
    /// </summary>
    /// 
    /// <author>Ludovic Roland</author>
    /// <since>2014.03.24</since>
    public static class M4MDateUtil
    {

        /// <summary>
        /// Converts an unix timestamp to a datetime.
        /// </summary>
        /// <param name="unixTimeStamp">the timestamp to convert in <see cref="DateTime"/></param>
        /// <returns>the <see cref="DateTime"/></returns>
        public static DateTime UnixTimestampToDateTime(double unixTimeStamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(unixTimeStamp).ToLocalTime();
        }
        
        /// <summary>
        /// Converts a java timestamp to a datetime.
        /// </summary>
        /// <param name="javaTimeStamp">the timestamp to convert in <see cref="DateTime"/></param>
        /// <returns>the <see cref="DateTime"/></returns>
        public static DateTime JavaTimestampToDateTime(double javaTimeStamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Math.Round(javaTimeStamp/1000)).ToLocalTime();
        }

    }

}
