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
