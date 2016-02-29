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
