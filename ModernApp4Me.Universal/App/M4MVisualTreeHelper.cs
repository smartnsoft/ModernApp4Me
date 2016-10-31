// Copyright (C) 2016 Smart&Soft SAS (http://www.smartnsoft.com/) and contributors
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

using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace ModernApp4Me.Universal.App
{

  /// <author>Guillaume Bonnin</author>
  /// <since>2016.10.31</since>
  /// <summary>
  /// Offers utils method to access at visual tree in the application
  /// </summary>
  public sealed class M4MVisualTreeHelper
  {

    public static DependencyObject GetFirstParent<T>(DependencyObject reference)
    {
      var parent = reference;
      while (parent != null)
      {
        parent = VisualTreeHelper.GetParent(parent);
        if (parent is T)
        {
          return parent;
        }
      }
      return null;
    }

    public static DependencyObject GetFirstChild<T>(DependencyObject reference)
    {
      var childrensNumber = VisualTreeHelper.GetChildrenCount(reference);
      for (var i = 0; i < childrensNumber; i++)
      {
        var child = VisualTreeHelper.GetChild(reference, i);
        if (child is T)
        {
          return child;
        }
        child = GetFirstChild<T>(child);
        if (child is T)
        {
          return child;
        }
      }

      return null;
    }

  }
}
