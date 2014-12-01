using System;

namespace ModernApp4Me.Core.Log
{

    /// <summary>
    /// Wrapper to hold an instance of a SnSLogger.
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.24</since>
    public sealed class M4MLoggerWrapper
    {

        private static volatile M4MLoggerWrapper instance;

        private static readonly object InstanceLock = new Object();

        private M4MLoggerWrapper() { }

        public static M4MLoggerWrapper Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (InstanceLock)
                    {
                        if (instance == null)
                        {
                            instance = new M4MLoggerWrapper();
                        }
                    }
                }

                return instance;
            }
        }

        public M4MLogger Logger { get; set; }

    }

}