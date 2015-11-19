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
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.UI.Xaml;

namespace ModernApp4Me.Universal.Metadata
{

  /// <author>Ludovic Roland</author>
  /// <since>2015.11.19</since>
  /// <summary>
  /// Provides information about the device.
  /// </summary>
  public sealed class M4MDeviceInformation
  {

    private static volatile M4MDeviceInformation instance;

    private static readonly object InstanceLock = new Object();

    public static M4MDeviceInformation Instance
    {
      get
      {
        if (instance == null)
        {
          lock (InstanceLock)
          {
            if (instance == null)
            {
              instance = new M4MDeviceInformation();
            }
          }
        }

        return instance;
      }
    }

    private readonly EasClientDeviceInformation deviceInformation;

    private M4MDeviceInformation()
    {
      deviceInformation = new EasClientDeviceInformation();
    }

    public string FriendlyName
    {
      get
      {
        return deviceInformation.FriendlyName;
      }
    }

    public string OperatingSystem
    {
      get
      {
        return deviceInformation.OperatingSystem;
      }
    }

    public string SystemManufacturer
    {
      get
      {
        return deviceInformation.SystemManufacturer;
      }
    }

    public string SystemProductName
    {
      get
      {
        return deviceInformation.SystemProductName;
      }
    }

    public string SystemSku
    {
      get
      {
        return deviceInformation.SystemSku;
      }
    }

    public Guid Id
    {
      get
      {
        return deviceInformation.Id;
      }
    }

    public double ScreenWidth
    {
      get
      {
        return Window.Current.Bounds.Width;
      }
    }

    public double ScreenHeight
    {
      get
      {
        return Window.Current.Bounds.Height;
      }
    }

  }

}
