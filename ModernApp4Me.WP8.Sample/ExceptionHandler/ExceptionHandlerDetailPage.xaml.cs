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
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Phone.Shell;
using ModernApp4Me.Core.LifeCycle;
using ModernApp4Me.Core.ViewModel;
using ModernApp4Me.WP8.Sample.ViewModel;
using ModernApp4Me.WP8.Sample.WebService;

namespace ModernApp4Me.WP8.Sample.ExceptionHandler
{

    /// <author>Ludovic ROLAND</author>
    /// <since>2015.03.05</since>
    public sealed partial class ExceptionHandlerDetailPage
    {

        private string param;

        public ExceptionHandlerDetailPage()
        {
            InitializeComponent();

            isManagingProgressIndicatorItself = true;
        }

        protected override void LoadQueryString()
        {
            param = NavigationContext.QueryString["param"];
        }

        protected async override Task<M4MBaseViewModel> ComputeViewModel()
        {
            if (ExceptionHandlerPage.BO.Equals(param) == true)
            {
                throw new M4MBusinessObjectUnavailableException();
            }

            if (ExceptionHandlerPage.EXCEPTION.Equals(param) == true)
            {
                throw new NullReferenceException();
            }

            var weather = await Services.Instance.GetWeather("London");
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
            return null;
        }
    }
}