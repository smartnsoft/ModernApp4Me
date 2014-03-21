using System;

namespace ModernApp4Me.WP7.SnSLogger
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

    }

}
