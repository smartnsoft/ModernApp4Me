using ModernApp4Me.Core.Log;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ModernApp4Me.WP8.Log
{

    /// <summary>
    /// The default base logger implementation.
    /// </summary>
    /// 
    /// <author>Ludovic Roland</author>
    /// <since>2014.03.21</since>
    public sealed class M4MModernLogger : M4MLogger
    {

        private static volatile M4MModernLogger instance;

        private static readonly object InstanceLock = new Object();

        public LogLevel ModernLogLevel { get; set; }

        private M4MModernLogger()
        {
            Debugger.Launch();
        }

        public static M4MModernLogger Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (InstanceLock)
                    {
                        if (instance == null)
                        {
                            instance = new M4MModernLogger();
                        }
                    }
                }

                return instance;
            }
        } 

        public override void Debug(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1)
        {
            if (Debugger.IsLogging() == true)
            {
                Debugger.Log(0, LogLevel.Debug.ToString(), BuildLog(LogLevel.Debug, message, callerMemberName, callerFilePath, callerLineNumber));
            }
        }

        public override void Info(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1)
        {
            if (Debugger.IsLogging() == true)
            {
                Debugger.Log(1, LogLevel.Debug.ToString(), BuildLog(LogLevel.Info, message, callerMemberName, callerFilePath, callerLineNumber));
            }
        }

        public override void Warn(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1)
        {
            if (Debugger.IsLogging() == true)
            {
                Debugger.Log(2, LogLevel.Debug.ToString(), BuildLog(LogLevel.Warn, message, callerMemberName, callerFilePath, callerLineNumber));
            }
        }

        public override void Warn(string message, Exception exception, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1)
        {
            if (Debugger.IsLogging() == true)
            {
                Debugger.Log(2, LogLevel.Debug.ToString(), BuildLog(LogLevel.Warn, message, exception.Message, exception.StackTrace, callerMemberName, callerFilePath, callerLineNumber));
            }
        }

        public override void Error(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1)
        {
            if (Debugger.IsLogging() == true)
            {
                Debugger.Log(3, LogLevel.Debug.ToString(), BuildLog(ModernLogLevel, message, callerMemberName, callerFilePath, callerLineNumber));
            }
        }

        public override void Error(string message, Exception exception, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1)
        {
            if (Debugger.IsLogging() == true)
            {
                Debugger.Log(3, LogLevel.Debug.ToString(), BuildLog(LogLevel.Warn, message, exception.Message, exception.StackTrace, callerMemberName, callerFilePath, callerLineNumber));
            }
        }

        public override void Fatal(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1)
        {
            if (Debugger.IsLogging() == true)
            {
                Debugger.Log(4, LogLevel.Debug.ToString(), BuildLog(ModernLogLevel, message, callerMemberName, callerFilePath, callerLineNumber));
            }
        }

        public override void Fatal(string message, Exception exception, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1)
        {
            if (Debugger.IsLogging() == true)
            {
                Debugger.Log(4, LogLevel.Debug.ToString(), BuildLog(LogLevel.Warn, message, exception.Message, exception.StackTrace, callerMemberName, callerFilePath, callerLineNumber));
            }
        }

        public override bool IsDebugEnabled()
        {
            return ModernLogLevel <= LogLevel.Debug;
        }

        public override bool IsInfoEnabled()
        {
            return ModernLogLevel <= LogLevel.Info;
        }

        public override bool IsWarnEnabled()
        {
            return ModernLogLevel <= LogLevel.Warn;
        }

        public override bool IsErrorEnabled()
        {
            return ModernLogLevel <= LogLevel.Error;
        }

        public override bool IsFatalEnabled()
        {
            return ModernLogLevel <= LogLevel.Error;
        }

    }

}