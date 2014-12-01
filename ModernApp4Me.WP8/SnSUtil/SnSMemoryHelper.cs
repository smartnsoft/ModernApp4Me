using System;
using System.Threading;
using Microsoft.Phone.Info;

namespace ModernApp4Me.WP8.SnSUtil
{

    /// <author>Ludovic ROLAND</author>
    /// <since>2014.09.08</since>
    public abstract class SnSMemoryHelper
    {

        private static Timer timer = null;

        public static void BeginRecording()
        {
            // before we start recording we can clean up the previous session.
            // e.g. Get a logging file from IsoStore and upload to the server 

            // start a timer to report memory conditions every 2 seconds
            timer = new Timer(state =>
            {
                // every 2 seconds do something 
                var report =
                    DateTime.Now.ToLongTimeString() + " memory conditions: " +
                    Environment.NewLine +
                    "\tApplicationCurrentMemoryUsage: " +
                        DeviceStatus.ApplicationCurrentMemoryUsage +
                        Environment.NewLine +
                    "\tApplicationPeakMemoryUsage: " +
                        DeviceStatus.ApplicationPeakMemoryUsage +
                        Environment.NewLine +
                    "\tApplicationMemoryUsageLimit: " +
                        DeviceStatus.ApplicationMemoryUsageLimit +
                        Environment.NewLine +
                    "\tDeviceTotalMemory: " + DeviceStatus.DeviceTotalMemory + Environment.NewLine +
                    "\tApplicationWorkingSetLimit: " +
                        DeviceExtendedProperties.GetValue("ApplicationWorkingSetLimit") +
                        Environment.NewLine;

                // write to IsoStore or debug conolse
                WriteReport(report);
            },
                null,
                TimeSpan.FromSeconds(2),
                TimeSpan.FromSeconds(2));
        }

        protected static void WriteReport(string report)
        {
            //This method should be hidden using the 'new' keyword
        }

    }

}
