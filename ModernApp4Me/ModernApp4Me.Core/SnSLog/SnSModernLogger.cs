using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace ModernApp4Me.Core.SnSLog
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

        public override void Debug(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1)
        {
            if (IsDebugEnabled() == true)
            {
                DisplayLog(SnSLogLevel.Debug, message, callerMemberName, callerFilePath, callerLineNumber);
            }
        }

        public override void Info(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1)
        {
            if (IsInfoEnabled() == true)
            {
                DisplayLog(SnSLogLevel.Info, message, callerMemberName, callerFilePath, callerLineNumber);
            }
        }

        public override void Warn(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1)
        {
            if (IsWarnEnabled() == true)
            {
                DisplayLog(SnSLogLevel.Warn, message, callerMemberName, callerFilePath, callerLineNumber);
            }
        }

        public override void Warn(string message, Exception exception, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1)
        {
            if (IsWarnEnabled() == true)
            {
                DisplayLog(SnSLogLevel.Warn, message, exception, callerMemberName, callerFilePath, callerLineNumber);
            }
        }

        public override void Error(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1)
        {
            if (IsErrorEnabled() == true)
            {
                DisplayLog(SnSLogLevel.Error, message, callerMemberName, callerFilePath, callerLineNumber);
            }
        }

        public override void Error(string message, Exception exception, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1)
        {
            if (IsErrorEnabled() == true)
            {
                DisplayLog(SnSLogLevel.Error, message, exception, callerMemberName, callerFilePath, callerLineNumber);
            }
        }

        public override void Fatal(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1)
        {
            if (IsErrorEnabled() == true)
            {
                DisplayLog(SnSLogLevel.Error, message, callerMemberName, callerFilePath, callerLineNumber);
            }
        }

        public override void Fatal(string message, Exception exception, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1)
        {
            if (IsErrorEnabled() == true)
            {
                DisplayLog(SnSLogLevel.Error, message, exception, callerMemberName, callerFilePath, callerLineNumber);
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

        private StringBuilder BuildHeader(SnSLogLevel logLevel,string callerMemberName, string callerFilePath, int callerLineNumber)
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

            if (callerFilePath.Equals("") == false && callerMemberName.Equals("") == false)
            {
                header.Append("\n");
                header.Append("File : '").Append(callerFilePath).Append("'\n");
                header.Append("Method : '").Append(callerMemberName).Append("'");

                if (callerLineNumber != -1)
                {
                    header.Append(" at line '").Append(callerLineNumber).Append("'");
                }
            }

            return header;
        }

        private void DisplayLog(SnSLogLevel logLevel, string message, string callerMemberName, string callerFilePath, int callerLineNumber)
        {
            var header = BuildHeader(logLevel, callerMemberName, callerFilePath, callerLineNumber);

            System.Diagnostics.Debug.WriteLine("{0} \n\t {1} ", header, message);
        }

        private void DisplayLog(SnSLogLevel logLevel, string message, Exception exception, string callerMemberName , string callerFilePath, int callerLineNumber)
        {
            var header = BuildHeader(logLevel, callerMemberName, callerFilePath, callerLineNumber);

            System.Diagnostics.Debug.WriteLine("{0} \n\t {1} \n\t {2} ", header, message, exception.StackTrace);
        }

    }

}