using System;
using Microsoft.Phone.Info;

namespace ModernApp4Me.WP8.SnSMetaData
{

    /// <summary>
    /// A class that provides information about the device (name, constructor, etc...).
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.24</since>
    public static class SnSDeviceInfo
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
