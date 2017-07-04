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
