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
