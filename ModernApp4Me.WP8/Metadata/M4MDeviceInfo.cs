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
using Microsoft.Phone.Info;

namespace ModernApp4Me.WP8.Metadata
{

    /// <summary>
    /// Provides information about the device.
    /// </summary>
    /// 
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.24</since>
    public static class M4MDeviceInfo
    {

        public static string FirmwareVersion
        {
            get { return DeviceStatus.DeviceFirmwareVersion; }
        }

        public static string HardwareVersion
        {
            get { return DeviceStatus.DeviceHardwareVersion; }
        }

        public static string Manufacturer
        {
            get { return DeviceStatus.DeviceManufacturer; }
        }

        public static string Name
        {
            get { return DeviceStatus.DeviceName; }
        }

        public static string Uuid
        {
            get
            {
                var deviceId = (byte[])DeviceExtendedProperties.GetValue("DeviceUniqueId");
                return Convert.ToBase64String(deviceId);
            }
        }

    }

}
