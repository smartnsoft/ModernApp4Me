// Copyright (C) 2015 Smart&Soft SAS (http://www.smartnsoft.com/) and contributors
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 3 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details.
//
// Contributors:
//   Smart&Soft - initial API and implementation

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
