using System;

namespace wp4me.SnsDebugUtils
{
    /// <summary>
    /// Class that provides function to work debugs tools.
    /// </summary>
    public sealed class SnSDebugUtils
    {
        /// <summary>
        /// Method that writes text into the console.
        /// </summary>
        /// <param name="text"></param>
        public static void ConsoleWriteLine(string text)
        {
            System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " " + text);
        }
    }
}
