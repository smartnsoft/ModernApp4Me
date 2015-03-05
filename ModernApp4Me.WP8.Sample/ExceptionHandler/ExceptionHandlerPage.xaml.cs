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