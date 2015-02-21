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
