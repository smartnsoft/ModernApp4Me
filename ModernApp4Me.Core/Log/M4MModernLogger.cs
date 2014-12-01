using System;
using System.Runtime.CompilerServices;

namespace ModernApp4Me.Core.Log
{

    /// <summary>
    /// The default logger implementation.
    /// <para>This implementation can only be used when the code integrating the library runs in debug mode.</para>
    /// </summary>
    /// 
    /// <author>Ludovic Roland</author>
    /// <since>2014.03.21</since>
    public abstract class M4MModernLogger : M4MLogger
    {

        public M4MLogLevel LogLevel { get; set; }

        public override void Debug(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1)
        {
            if (IsDebugEnabled() == true)
            {
                DisplayLog(M4MLogLevel.Debug, message, callerMemberName, callerFilePath, callerLineNumber);
            }
        }

        public override void Info(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1)
        {
            if (IsInfoEnabled() == true)
            {
                DisplayLog(M4MLogLevel.Info, message, callerMemberName, callerFilePath, callerLineNumber);
            }
        }

        public override void Warn(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1)
        {
            if (IsWarnEnabled() == true)
            {
                DisplayLog(M4MLogLevel.Warn, message, callerMemberName, callerFilePath, callerLineNumber);
            }
        }

        public override void Warn(string message, Exception exception, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1)
        {
            if (IsWarnEnabled() == true)
            {
                DisplayLog(M4MLogLevel.Warn, message, exception, callerMemberName, callerFilePath, callerLineNumber);
            }
        }

        public override void Error(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1)
        {
            if (IsErrorEnabled() == true)
            {
                DisplayLog(M4MLogLevel.Error, message, callerMemberName, callerFilePath, callerLineNumber);
            }
        }

        public override void Error(string message, Exception exception, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1)
        {
            if (IsErrorEnabled() == true)
            {
                DisplayLog(M4MLogLevel.Error, message, exception, callerMemberName, callerFilePath, callerLineNumber);
            }
        }

        public override void Fatal(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1)
        {
            if (IsErrorEnabled() == true)
            {
                DisplayLog(M4MLogLevel.Error, message, callerMemberName, callerFilePath, callerLineNumber);
            }
        }

        public override void Fatal(string message, Exception exception, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1)
        {
            if (IsErrorEnabled() == true)
            {
                DisplayLog(M4MLogLevel.Error, message, exception, callerMemberName, callerFilePath, callerLineNumber);
            }
        }

        public override bool IsDebugEnabled()
        {
            return LogLevel <= M4MLogLevel.Debug;
        }

        public override bool IsInfoEnabled()
        {
            return LogLevel <= M4MLogLevel.Info;
        }

        public override bool IsWarnEnabled()
        {
            return LogLevel <= M4MLogLevel.Warn;
        }

        public override bool IsErrorEnabled()
        {
            return LogLevel <= M4MLogLevel.Error;
        }

        public override bool IsFatalEnabled()
        {
            return LogLevel <= M4MLogLevel.Error;
        }

    }

}