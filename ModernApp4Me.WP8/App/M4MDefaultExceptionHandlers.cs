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
            MessageBox.Show(message, I18N.DialogBoxErrorTitle, MessageBoxButton.OK);
            
            if (RootFrame.CanGoBack == true)
            {
                RootFrame.GoBack();
            }
            else
            {
                Application.Current.Terminate();
            }
        }

        protected override void ShowConnectivityMessageBox(string message)
        {
            MessageBox.Show(message, I18N.DialogBoxErrorTitle, MessageBoxButton.OK);
        }

    }

}
