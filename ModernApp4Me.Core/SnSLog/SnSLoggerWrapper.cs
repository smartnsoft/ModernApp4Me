using System;

namespace ModernApp4Me.Core.SnSLog
{

    /// <summary>
    /// Wrapper to hold an instance of a SnSLogger.
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.24</since>
    public sealed class SnSLoggerWrapper
    {

        private static volatile SnSLoggerWrapper instance;

        private static readonly object InstanceLock = new Object();

        private SnSLoggerWrapper() { }

        public static SnSLoggerWrapper Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (InstanceLock)
                    {
                        if (instance == null)
                        {
                            instance = new SnSLoggerWrapper();
                        }
                    }
                }

                return instance;
            }
        }

        public SnSLogger Logger { get; set; }

    }

}