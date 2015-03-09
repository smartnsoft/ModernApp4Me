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
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Phone.Shell;
using ModernApp4Me.Core.LifeCycle;
using ModernApp4Me.Core.ViewModel;
using ModernApp4Me.WP8.Sample.Bo;
using ModernApp4Me.WP8.Sample.Resources;
using ModernApp4Me.WP8.Sample.ViewModel;
using ModernApp4Me.WP8.Sample.WebService;

namespace ModernApp4Me.WP8.Sample.PhoneApplicationPage
{

    /// <author>Ludovic ROLAND</author>
    /// <since>2015.03.09</since>
    public partial class PhoneApplicationPagePage
    {

        private readonly string[] cities = { "London", "Paris", "Munich", "Milan" };

        private ApplicationBar defaultApplicationBar;

        private ApplicationBar connectivityApplicationBar;

        private ApplicationBarIconButton changeButton;

        private ApplicationBarIconButton retryButton;

        private bool throwException = true;

        private int currentCity = 0;

        public PhoneApplicationPagePage()
        {
            InitializeComponent();
            BuildApplicationBars();
        }

        private void BuildApplicationBars()
        {
            BuildDefaultApplicationBar();
            BuildConnectivityApplicationBar();

            ApplicationBar = defaultApplicationBar;
        }

        private void BuildDefaultApplicationBar()
        {
            defaultApplicationBar = new ApplicationBar()
            {
                Mode = ApplicationBarMode.Minimized,
                Opacity = 1
            };

            changeButton = new ApplicationBarIconButton()
            {
                IconUri = new Uri("/Assets/questionmark.png", UriKind.Relative),
                Text = AppResources.ChangeCity,
                IsEnabled = false
            };

            changeButton.Click += ChangeButton_Click;

            defaultApplicationBar.Buttons.Add(changeButton);
        }

        private void BuildConnectivityApplicationBar()
        {
            //Basically, it is the same thing that the defaultApplicationBar
            //It is juste for the example :)
            connectivityApplicationBar = new ApplicationBar()
            {
                Mode = ApplicationBarMode.Default,
                Opacity = 1
            };

            retryButton = new ApplicationBarIconButton()
            {
                IconUri = new Uri("/Assets/refresh.png", UriKind.Relative),
                Text = AppResources.Retry,
                IsEnabled = false
            };

            retryButton.Click += RetryButton_Click;

            connectivityApplicationBar.Buttons.Add(retryButton);
        }

        protected async override Task<M4MBaseViewModel> ComputeViewModel()
        {
            //for the example, we throw a M4MConnectivityException the first time
            if (throwException == true)
            {
                throwException = false;
                throw new M4MConnectivityException();
            }

            var forecasts = await RetrieveBusinessObjects();
            return new ForecastsViewModel() {Forecasts = forecasts};
        }

        protected async override Task RefreshViewModel()
        {
            ((ForecastsViewModel) viewModel).Forecasts = await RetrieveBusinessObjects();
        }

        private async Task<List<Weather.Forecast>> RetrieveBusinessObjects()
        {
            var weather = await Services.Instance.GetRetentionWeather(cities[currentCity % cities.Length]);
            return weather.Forecasts;
        }

        protected override void OnFullfillDisplayObjects()
        {
            DataContext = viewModel;
        }

        protected override Panel RetrieveMainContainer()
        {
            return ContentPanel;
        }

        protected override Panel RetrieveConnectivityContainer()
        {
            return ConnectivityPanel;
        }

        protected override ProgressIndicator RetrieveProgressIndicator()
        {
            return ProgressIndicatorBar;
        }

        private async void RetryButton_Click(object sender, EventArgs e)
        {
            ApplicationBar = defaultApplicationBar;
            changeButton.IsEnabled = false;
            await Refresh();
            changeButton.IsEnabled = true;
        }

        private async void ChangeButton_Click(object sender, EventArgs e)
        {
            currentCity++;
            changeButton.IsEnabled = false;
            await Refresh();
            changeButton.IsEnabled = true;
        }

        protected override void OnDisplayConnectivityLayout()
        {
            base.OnDisplayConnectivityLayout();
            ApplicationBar = connectivityApplicationBar;
            retryButton.IsEnabled = true;
        }
    }
}