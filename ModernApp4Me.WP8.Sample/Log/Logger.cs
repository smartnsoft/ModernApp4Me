using System;
using ModernApp4Me.Core.Log;

namespace ModernApp4Me.WP8.Sample.Log
{

    public sealed class Logger : M4MLogger
    {

        private static volatile Logger instance;

        private static readonly object InstanceLock = new Object();

        public static Logger Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (InstanceLock)
                    {
                        if (instance == null)
                        {
                            instance = new Logger();
                        }
                    }
                }

                return instance;
            }
        }

        public override void WriteTrace(string log)
        {
            System.Diagnostics.Debug.WriteLine(log);
        }
    }
}
