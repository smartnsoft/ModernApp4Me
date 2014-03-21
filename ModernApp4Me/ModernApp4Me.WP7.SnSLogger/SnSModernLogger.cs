using System;
using System.Text;

namespace ModernApp4Me.WP7.SnSLogger
{

    /// <summary>
    /// The default logger.
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.21</since>
    public class SnSModernLogger : SnSLogger
    {

        private static volatile SnSModernLogger instance;

        private static readonly object Sync = new Object();

        private SnSModernLogger() { }

        public static SnSModernLogger Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (Sync)
                    {
                        if (instance == null)
                        {
                            instance = new SnSModernLogger();
                        }
                    }
                }

                return instance;
            }
        }

        public SnSLogLevel LogLevel { get; set; }

        public override void Debug(string message)
        {
            if (IsDebugEnabled() == true)
            {
                DisplayLog(SnSLogLevel.Debug, message);
            }
        }

        public override void Info(string message)
        {
            if (IsInfoEnabled() == true)
            {
                DisplayLog(SnSLogLevel.Info, message);
            }
        }

        public override void Warn(string message)
        {
            if (IsWarnEnabled() == true)
            {
                DisplayLog(SnSLogLevel.Warn, message);
            }
        }

        public override void Warn(string message, Exception exception)
        {
            if (IsWarnEnabled() == true)
            {
                DisplayLog(SnSLogLevel.Warn, message, exception);
            }
        }

        public override void Error(string message)
        {
            if (IsErrorEnabled() == true)
            {
                DisplayLog(SnSLogLevel.Error, message);
            }
        }

        public override void Error(string message, Exception exception)
        {
            if (IsErrorEnabled() == true)
            {
                DisplayLog(SnSLogLevel.Error, message, exception);
            }
        }

        public override void Fatal(string message)
        {
            if (IsErrorEnabled() == true)
            {
                DisplayLog(SnSLogLevel.Error, message);
            }
        }

        public override void Fatal(string message, Exception exception)
        {
            if (IsErrorEnabled() == true)
            {
                DisplayLog(SnSLogLevel.Error, message, exception);
            }
        }

        public override bool IsDebugEnabled()
        {
            return LogLevel >= SnSLogLevel.Debug;
        }

        public override bool IsInfoEnabled()
        {
            return LogLevel >= SnSLogLevel.Info;
        }

        public override bool IsWarnEnabled()
        {
            return LogLevel >= SnSLogLevel.Warn;
        }

        public override bool IsErrorEnabled()
        {
            return LogLevel >= SnSLogLevel.Error;
        }

        public override bool IsFatalEnabled()
        {
            return LogLevel >= SnSLogLevel.Error;
        }

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

        private void DisplayLog(SnSLogLevel logLevel, string message)
        {
            var header = BuildHeader(logLevel);

            System.Diagnostics.Debug.WriteLine("{0} \n\t {1} ", header, message);
        }

        private void DisplayLog(SnSLogLevel logLevel, string message, Exception exception)
        {
            var header = BuildHeader(logLevel);

            System.Diagnostics.Debug.WriteLine("{0} \n\t {1} \n\t {2} ", header, message, exception.StackTrace);
        }

    }

}