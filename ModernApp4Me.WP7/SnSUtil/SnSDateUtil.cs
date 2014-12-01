using System;

namespace ModernApp4Me.WP7.SnSUtil
{

    /// <summary>
    /// Util class to manipulate the dates.
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.24</since>
    public static class SnSDateUtil
    {

        /// <summary>
        /// Converts an unix timestamp to a datetime.
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        public static DateTime UnixTimestampToDateTime(double unixTimeStamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(unixTimeStamp).ToLocalTime();
        }
        
        /// <summary>
        /// Converts a java timestamp to a datetime.
        /// </summary>
        /// <param name="javaTimeStamp"></param>
        /// <returns></returns>
        public static DateTime JavaTimestampToDateTime(double javaTimeStamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Math.Round(javaTimeStamp/1000)).ToLocalTime();
        }

    }

}
