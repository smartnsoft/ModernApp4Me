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

using Windows.ApplicationModel.Resources;

namespace ModernApp4Me.Universal.App
{
  /// <summary>
  /// Helper class to make easier the use or resources strings in an application
  /// </summary>
  /// <author>Guillaume Bonnin</author>
  /// <since>2015.02.15</since>
  public sealed class M4MStrings
  {
    private static ResourceLoader resourceLoader;

    private static string resourcesName;

    /// <summary>
    /// Name of the resources files used to get strings, by default name is "Strings", but it can be overrided in applications by setting a new ResourcesName
    /// </summary>
    public static string ResourcesName
    {
      get {
        return resourcesName;
      }
      set
      {
        if (resourcesName != value && string.IsNullOrEmpty(value) == false)
        {
          resourcesName = value;
          try
          {
            resourceLoader = ResourceLoader.GetForViewIndependentUse(value);
          }
          catch { }
        }
      }
    }

    static M4MStrings()
    {
      try
      {
        ResourcesName = "Strings";
        resourceLoader = ResourceLoader.GetForViewIndependentUse(ResourcesName);
      }
      catch { }
    }

    public static string Get(string key)
    {
      if (resourceLoader == null)
      {
        return null;
      }
      return resourceLoader.GetString(key.ToString());
    }

    public string this[string key]
    {
      get
      {
        return Get(key);
      }
    }
  }
}
