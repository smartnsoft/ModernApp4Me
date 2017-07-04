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
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Info;
using Microsoft.Phone.Shell;
using ModernApp4Me.Core.ViewModel;
using ModernApp4Me.WP8.Sample.ViewModel;

namespace ModernApp4Me.WP8.Sample.MemoryProfiler
{

    /// <author>Ludovic ROLAND</author>
    /// <since>2015.03.06</since>
    public sealed partial class MemoryProfilerPage
    {

        //This class has been written only for the sample app.
        //Please refer to the MemoryProfilerCustom class written into the App class.
        //This classe should not be used like this :)
        public sealed class MemoryProfilerPageCustom
        {

            public MemoryProfilerPage Page { get; set; }

            public Timer timer { get; set; }

            public void BeginRecording()
            {
                timer = new Timer(state =>
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

            private void WriteReport(string report)
            {
                Page.UpdateViewModel(report);
            }

            public void Stop()
            {
                timer.Dispose();
            }
        }

        private MemoryProfilerPageCustom memoryProfiler;

        public MemoryProfilerPage()
        {
            InitializeComponent();

            isManagingProgressIndicatorItself = true;

            memoryProfiler = new MemoryProfilerPageCustom() {Page = this};
            memoryProfiler.BeginRecording();
        }

        protected async override Task<M4MBaseViewModel> ComputeViewModel()
        {
            return await Task.Run(() => new MemoryProfilerViewModel()
            {
                Report = string.Empty
            });
        }

        protected override void OnFullfillDisplayObjects()
        {
            DataContext = viewModel;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            memoryProfiler.Stop();
        }

        protected override Panel RetrieveMainContainer()
        {
            return ContentPanel;
        }

        protected override ProgressIndicator RetrieveProgressIndicator()
        {
            return null;
        }

        public void UpdateViewModel(string report)
        {
            if (viewModel != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    ((MemoryProfilerViewModel) viewModel).Report = report;
                });
            }
        }
    }
}
