using System;
using System.Diagnostics;
using System.Text;

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
        /// <param name="methodName"></param>
        /// <param name="text"></param>
        public static void ConsoleWriteLine(string methodName, string text)
        {
            var message = new StringBuilder(DateTime.Now.ToString("HH:mm:ss"));
            message.Append(' ');
            message.Append(methodName);
            message.Append(' ');
            message.Append(text);

            Debug.WriteLine(message.ToString());
        }
    }
}
