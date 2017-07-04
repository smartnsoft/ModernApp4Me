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
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ModernApp4Me.Core.App;

namespace ModernApp4Me.Universal.App
{

  /// <author>Ludovic Roland</author>
  /// <since>2014.03.24</since>
  /// <summary>
  /// A extension implementation which displays MessageBoxes.
  /// </summary>
  public class M4MDefaultExceptionHandlers : M4MExceptionHandlers
  {

    public Frame Frame { get; set; }

    protected async override void ShowMessageBox(string message)
    {
      var dialog = new MessageDialog(message, I18N.DialogBoxErrorTitle);
      dialog.Commands.Clear();
      dialog.Commands.Add(new UICommand(){ Label = "OK", Id = 0});

      await dialog.ShowAsync();

      if (Frame.CanGoBack == true)
      {
        Frame.GoBack();
      }
      else
      {
        Application.Current.Exit();
      }
    }

    protected async override void ShowConnectivityMessageBox(string message)
    {
      var dialog = new MessageDialog(message, I18N.DialogBoxErrorTitle);
      dialog.Commands.Clear();
      dialog.Commands.Add(new UICommand() { Label = "OK", Id = 0 });

      await dialog.ShowAsync();
    }

  }

}
