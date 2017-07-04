// The MIT License (MIT)
//
// Copyright (c) 2017 Smart&Soft
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

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
