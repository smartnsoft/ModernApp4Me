using System;
using System.Runtime.CompilerServices;

namespace ModernApp4Me.Core.SnSLog
{

    /// <summary>
    /// Just in order to have various loggers.
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.21</since>
    public abstract class SnSLogger
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

    }

}
