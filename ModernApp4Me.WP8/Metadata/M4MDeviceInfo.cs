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
