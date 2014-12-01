using System;

namespace ModernApp4Me.Core.Log
{

    /// <summary>
    /// A class which enables to wrap a <see cref="M4MLogger"/>, and to delegate all its interface methods to its underlying logger.
    /// </summary>
    /// 
    /// <author>Ludovic Roland</author>
    /// <since>2014.03.24</since>
    public class M4MLoggerWrapper
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