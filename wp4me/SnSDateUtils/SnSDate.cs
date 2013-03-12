using System;

namespace wp4me.SnSDateUtils
{
    /// <summary>
    /// Class that provides functions to work with date.
    /// </summary>
    public sealed class SnSDate
    {
        /// <summary>
        /// Function that converts an unix timestamp to a datetime.
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        public static DateTime UnixTimestampToDateTime(double unixTimeStamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(unixTimeStamp).ToLocalTime();
        }
        
        /// <summary>
        /// Function that converts a java timestamp to a datetime.
        /// </summary>
        /// <param name="javaTimeStamp"></param>
        /// <returns></returns>
        public static DateTime JavaTimestampToDateTime(double javaTimeStamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Math.Round(javaTimeStamp/1000)).ToLocalTime();
        }
    }
}
