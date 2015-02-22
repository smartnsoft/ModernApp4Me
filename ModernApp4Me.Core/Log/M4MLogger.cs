using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace ModernApp4Me.Core.Log
{

    /// <summary>
    /// Just in order to have various loggers.
    /// </summary>
    /// 
    /// <author>Ludovic Roland</author>
    /// <since>2014.03.21</since>
    public abstract class M4MLogger
    {

        public enum LogLevel
        {
            Debug = 0, Info = 1, Warn = 2, Error = 3
        }

        public abstract void Debug(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1);

        public abstract void Info(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1);

        public abstract void Warn(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1);

        public abstract void Warn(string message, Exception exception, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1);

        public abstract void Error(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1);

        public abstract void Error(string message, Exception exception, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1);

        public abstract void Fatal(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1);

        public abstract void Fatal(string message, Exception exception, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1);

        public abstract bool IsDebugEnabled();

        public abstract bool IsInfoEnabled();

        public abstract bool IsWarnEnabled();

        public abstract bool IsErrorEnabled();

        public abstract bool IsFatalEnabled();

        private StringBuilder BuildLogInternal(LogLevel logLevel, string message, string callerMemberName, string callerFilePath, int callerLineNumber)
        {
            var header = new StringBuilder("\n");

            switch (logLevel)
            {
                case LogLevel.Debug:
                    header.Append("[DEBUG] ");
                    break;

                case LogLevel.Info:
                    header.Append("[INFO] ");
                    break;

                case LogLevel.Warn:
                    header.Append("[WARN] ");
                    break;

                case LogLevel.Error:
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

                header.Append("\n" + message + "\n");
            }

            return header;
        }

        protected string BuildLog(LogLevel logLevel, string message, string callerMemberName, string callerFilePath, int callerLineNumber)
        {
            return BuildLogInternal(logLevel, message, callerMemberName, callerFilePath, callerLineNumber).ToString();
        }

        protected string BuildLog(LogLevel logLevel, string message, string exceptionMessage, string stackTrace, string callerMemberName, string callerFilePath, int callerLineNumber)
        {
            var header = BuildLogInternal(logLevel, message, callerMemberName, callerFilePath, callerLineNumber);
            header.Append("\n" + exceptionMessage).Append("\n" + stackTrace);

            return header.ToString();
        }

    }

}
