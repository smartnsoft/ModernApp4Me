using System;
using System.Threading;
using Microsoft.Phone.Info;

namespace ModernApp4Me.WP8.Debug
{

    /// <summary>
    /// Enables to profile the usage of the memory.
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.09.08</since>
    // Inpired by http://developer.nokia.com/community/wiki/Techniques_for_memory_analysis_of_Windows_Phone_apps
    public abstract class M4MMemoryProfiler
    {
        
        public void BeginRecording()
        {
            new Timer(state =>
            {
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
                    "\tDeviceTotalMemory: " + 
                        DeviceStatus.DeviceTotalMemory + 
                        Environment.NewLine +
                    "\tApplicationWorkingSetLimit: " +
                        DeviceExtendedProperties.GetValue("ApplicationWorkingSetLimit") +
                        Environment.NewLine;

                WriteReport(report);
            },
                null,
                TimeSpan.FromSeconds(2),
                TimeSpan.FromSeconds(2));
        }

        /// <summary>
        /// Enables to write the memory state where you want like the isostore or the debug console.
        /// </summary>
        /// <param name="report">the memory state</param>
        protected abstract void WriteReport(string report);

    }

}
