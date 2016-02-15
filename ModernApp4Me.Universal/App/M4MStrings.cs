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
    private readonly static ResourceLoader resourceLoader;

    static M4MStrings()
    {
      resourceLoader = new ResourceLoader();
    }

    public static string Get(string key)
    {
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
