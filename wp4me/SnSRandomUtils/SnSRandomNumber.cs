using System;

namespace wp4me.SnSRandomUtils
{
    /// <summary>
    /// Class that provides methods to generate random numbers.
    /// </summary>
    public sealed class SnSRandomNumber
    {
        /// <summary>
        /// Methods that returns a random number between min and max.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int GenerateRandomNumber(int min, int max)
        {
            return new Random().Next(min, max);
        }

        /// <summary>
        /// Methods that returns a random number between 0 and max
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int GenerateRandomNumber(int max)
        {
            return new Random().Next(0, max);
        }
    }
}
