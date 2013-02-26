using System;
using System.Diagnostics;

namespace wp4me.SnSDebugUtils
{
    /// <summary>
    /// Class that provides function to work with debugs tools.
    /// </summary>
    public sealed class SnSDebug
    {
        /*******************************************************/
        /** METHODS AND FUNCTIONS.
        /*******************************************************/
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
