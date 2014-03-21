using System;

namespace ModernApp4Me_Core.SnSRandom
{
    /// <summary>
    /// Provides methods to generate random numbers.
    /// </summary>
    public static class SnSRandomNumber
    {
        /*******************************************************/
        /** METHODS.
        /*******************************************************/
        /// <summary>
        /// Returns a random number between min and max.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int GenerateRandomNumber(int max, int min = 0)
        {
            return new Random().Next(min, max);
        }
    }
}
