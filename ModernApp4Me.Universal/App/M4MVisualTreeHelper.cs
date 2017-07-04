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

      for (var index = 0; index < childrensNumber; index++)
      {
        var child = VisualTreeHelper.GetChild(reference, index);

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
