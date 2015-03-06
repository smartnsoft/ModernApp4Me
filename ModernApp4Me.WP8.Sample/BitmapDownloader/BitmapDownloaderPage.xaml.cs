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

using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Phone.Shell;
using ModernApp4Me.Core.ViewModel;
using ModernApp4Me.WP8.Sample.ViewModel;
using ModernApp4Me.WP8.Sample.WebService;

namespace ModernApp4Me.WP8.Sample.BitmapDownloader
{

    /// <author>Ludovic ROLAND</author>
    /// <since>2015.03.06</since>
    public sealed partial class BitmapDownloaderPage
    {

        public BitmapDownloaderPage()
        {
            InitializeComponent();
        }

        protected async override Task<M4MBaseViewModel> ComputeViewModel()
        {
            var weather = await Services.Instance.GetRetentionWeather("London");
            return new ForecastsViewModel() { Forecasts = weather.Forecasts };
        }

        protected override void OnFullfillDisplayObjects()
        {
            DataContext = viewModel;
        }

        protected override Panel RetrieveMainContainer()
        {
            return ContentPanel;
        }

        protected override ProgressIndicator RetrieveProgressIndicator()
        {
            return ProgressIndicatorBar;
        }

    }
}
