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

        protected StringBuilder BuildHeader(M4MLogLevel logLevel, string callerMemberName, string callerFilePath, int callerLineNumber)
        {
            var header = new StringBuilder();

            switch (logLevel)
            {
                case M4MLogLevel.Debug:
                    header.Append("[DEBUG] ");
                    break;

                case M4MLogLevel.Info:
                    header.Append("[INFO] ");
                    break;

                case M4MLogLevel.Warn:
                    header.Append("[WARN] ");
                    break;

                case M4MLogLevel.Error:
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

        protected abstract void DisplayLog(M4MLogLevel logLevel, string message, string callerMemberName,
                                           string callerFilePath, int callerLineNumber);

        protected abstract void DisplayLog(M4MLogLevel logLevel, string message, Exception exception,
                                           string callerMemberName, string callerFilePath, int callerLineNumber);

    }

}
