using System;
using System.Diagnostics;

namespace wp4me.SnSDebugUtils
{
    /// <summary>
    /// Class that provides function to work debugs tools.
    /// </summary>
    public sealed class SnSDebug
    {
        /// <summary>
        /// Method that writes text into the console.
        /// </summary>
        /// <param name="text"></param>
        public static void ConsoleWriteLine(string text)
        {
            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " " + text);
        }
    }
}
