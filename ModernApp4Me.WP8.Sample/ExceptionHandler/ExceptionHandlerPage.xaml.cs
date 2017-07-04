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
using System.Windows;

namespace ModernApp4Me.WP8.Sample.ExceptionHandler
{

    /// <author>Ludovic ROLAND</author>
    /// <since>2015.03.05</since>
    public sealed partial class ExceptionHandlerPage
    {

        public const string BO = "bo";

        public const string EXCEPTION = "exception";

        public const string WS = "ws";

        public ExceptionHandlerPage()
        {
            InitializeComponent();
        }

        private void BtnExceptionHandlerNavigationException_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ThisPageDoesNotExist.xaml", UriKind.Relative));
        }

        private void BtnExceptionHandlerBusinessObjectException_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ExceptionHandler/ExceptionHandlerDetailPage.xaml?param=" + ExceptionHandlerPage.BO, UriKind.Relative));
        }

        private void BtnExceptionHandlerException_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ExceptionHandler/ExceptionHandlerDetailPage.xaml?param=" + ExceptionHandlerPage.EXCEPTION, UriKind.Relative));
        }

        private void BtnExceptionHandlerConnectivity_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Please before using this sample case, turn on the airplane mode.", "Warning", MessageBoxButton.OK);
            NavigationService.Navigate(new Uri("/ExceptionHandler/ExceptionHandlerDetailPage.xaml?param=" + ExceptionHandlerPage.WS, UriKind.Relative));
        }
    }
}