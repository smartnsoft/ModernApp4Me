using System;

namespace wp4me.SnSDateUtils
{
    /// <summary>
    /// Class that provides functions to work with date.
    /// </summary>
    public sealed class SnSDate
    {
        /// <summary>
        /// Function that returns the number of ticks since January 1st 1970.
        /// </summary>
        /// <returns></returns>
        public static long GetTicksSinceJanuary1970()
        {
            return new DateTime(1970, 1, 1, 0, 0, 0).Ticks;
        }
    }
}
