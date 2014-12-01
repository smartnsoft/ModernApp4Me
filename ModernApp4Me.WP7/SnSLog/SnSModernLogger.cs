using System;
using ModernApp4Me.WP7.SnSLogger;

namespace ModernApp4Me.WP7.SnSLog
{

    /// <summary>
    /// The default logger.
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.21</since>
    public class SnSModernLogger : SnSLogger
    {

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

    }

}