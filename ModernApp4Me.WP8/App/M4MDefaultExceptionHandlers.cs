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

using System.Windows;
using Microsoft.Phone.Controls;
using ModernApp4Me.Core.App;

namespace ModernApp4Me.WP8.App
{

    /// <summary>
    /// A extension implementation for Windows Phone, which displays MessageBoxes.
    /// </summary>
    /// 
    /// <author>Ludovic Roland</author>
    /// <since>2014.03.24</since>
    public class M4MDefaultExceptionHandlers : M4MExceptionHandlers
    {

        public PhoneApplicationFrame RootFrame { get; set; }

        protected override void ShowMessageBox(string message)
        {
            var result = MessageBox.Show(message, I18N.DialogBoxErrorTitle, MessageBoxButton.OK);

            if (result == MessageBoxResult.OK)
            {
                if (RootFrame.CanGoBack == true)
                {
                    RootFrame.GoBack();
                }
                else
                {
                    Application.Current.Terminate();
                }
            }
        }

        protected override void ShowConnectivityMessageBox(string message)
        {
            var result = MessageBox.Show(message, I18N.DialogBoxErrorTitle, MessageBoxButton.OK);

            if (result == MessageBoxResult.OK)
            {
                if (RootFrame.CanGoBack == true)
                {
                    RootFrame.GoBack();
                }
            }
        }

    }

}
