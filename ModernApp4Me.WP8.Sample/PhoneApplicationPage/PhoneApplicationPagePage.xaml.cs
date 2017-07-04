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
            var weather = await Services.Instance.GetWeather(cities[currentCity % cities.Length]);
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

        protected override Panel RetrieveLoadingContainer()
        {
          return LoaderPanel;
        }

        private async void RetryButton_Click(object sender, EventArgs e)
        {
            ApplicationBar = defaultApplicationBar;
            changeButton.IsEnabled = false;
            displayLoaderPanelNextTime = false;
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