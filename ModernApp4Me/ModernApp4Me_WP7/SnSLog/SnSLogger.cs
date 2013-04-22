using System;
using System.Text;

namespace ModernApp4Me_WP7.SnSLog
{
    /// <summary>
    /// Logger for Windows Phonet and Windows 8 apps.
    /// </summary>
    public static class SnSLogger
    {
        /*******************************************************/
        /** METHODS.
        /*******************************************************/
        /// <summary>
        /// Displays a debug message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="className"></param>
        /// <param name="functionName"></param>
        public static void Debug(string message, string className = "", string functionName = "")
        {
            var header = new StringBuilder("[DEBUG] ");
            header.Append(DateTime.Now.ToString("HH:mm:ss"));

            DisplayLog(className, functionName, header.ToString(), message);
        }

        /// <summary>
        /// Displays an info message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="className"></param>
        /// <param name="functionName"></param>
        public static void Info(string message, string className = "", string functionName = "")
        {
            var header = new StringBuilder("[INFO] ");
            header.Append(DateTime.Now.ToString("HH:mm:ss"));

            DisplayLog(className, functionName, header.ToString(), message);
        }

        /// <summary>
        /// Displays a warning message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="className"></param>
        /// <param name="functionName"></param>
        public static void Warn(string message, string className = "", string functionName = "")
        {
            var header = new StringBuilder("[WARN] ");
            header.Append(DateTime.Now.ToString("HH:mm:ss"));

            DisplayLog(className, functionName, header.ToString(), message);
        }

        /// <summary>
        /// Displays a fatal message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="className"></param>
        /// <param name="functionName"></param>
        public static void Fatal(string message, string className = "", string functionName = "")
        {
            var header = new StringBuilder("[ERROR] ");
            header.Append(DateTime.Now.ToString("HH:mm:ss"));

            DisplayLog(className, functionName, header.ToString(), message);
        }

        /// <summary>
        /// Displays an error message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="className"></param>
        /// <param name="functionName"></param>
        public static void Error(string message, string className = "", string functionName = "")
        {
            var header = new StringBuilder("[ERROR] ");
            header.Append(DateTime.Now.ToString("HH:mm:ss"));

            DisplayLog(className, functionName, header.ToString(), message);
        }
        
        /// <summary>
        /// Displays the log message into the console.
        /// </summary>
        /// <param name="className"></param>
        /// <param name="functionName"></param>
        /// <param name="header"></param>
        /// <param name="message"></param>
        private static void DisplayLog(string className, string functionName, string header, string message)
        {
            System.Diagnostics.Debug.WriteLine("{0} {1} {2} \n\t {3}", header, className, functionName, message);
        }
    }
}
