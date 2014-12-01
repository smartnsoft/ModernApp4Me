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
