using System;
using System.Text;
using ModernApp4Me.WP7.SnSLogger;

namespace ModernApp4Me.WP7.SnSLog
{

    /// <summary>
    /// Just in order to have various loggers.
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.21</since>
    public abstract class SnSLogger
    {

        public abstract void Debug(string message);

        public abstract void Info(string message);

        public abstract void Warn(string message);

        public abstract void Warn(string message, Exception exception);

        public abstract void Error(string message);

        public abstract void Error(string message, Exception exception);

        public abstract void Fatal(string message);

        public abstract void Fatal(string message, Exception exception);

        public abstract bool IsDebugEnabled();

        public abstract bool IsInfoEnabled();

        public abstract bool IsWarnEnabled();

        public abstract bool IsErrorEnabled();

        public abstract bool IsFatalEnabled();

        private StringBuilder BuildHeader(SnSLogLevel logLevel)
        {
            var header = new StringBuilder();

            switch (logLevel)
            {
                case SnSLogLevel.Debug:
                    header.Append("[DEBUG] ");
                    break;

                case SnSLogLevel.Info:
                    header.Append("[INFO] ");
                    break;

                case SnSLogLevel.Warn:
                    header.Append("[WARN] ");
                    break;

                case SnSLogLevel.Error:
                    header.Append("[ERROR] ");
                    break;
            }

            header.Append(DateTime.Now.ToString("HH:mm:ss"));

            return header;
        }

        protected void DisplayLog(SnSLogLevel logLevel, string message)
        {
            var header = BuildHeader(logLevel);

            System.Diagnostics.Debug.WriteLine("{0} \n\t {1} ", header, message);
        }

        protected void DisplayLog(SnSLogLevel logLevel, string message, Exception exception)
        {
            var header = BuildHeader(logLevel);

            System.Diagnostics.Debug.WriteLine("{0} \n\t {1} \n\t {2} ", header, message, exception.StackTrace);
        }

    }

}
