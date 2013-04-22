using Microsoft.Phone.Info;

namespace ModernApp4Me_WP7.SnSMetaData
{
    /// <summary>
    /// A class that provides information about the device (name, constructor, etc...).
    /// </summary>
    public static class SnSDeviceInfo
    {
        /*******************************************************/
        /** PROPERTIES.
        /*******************************************************/
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
    }
}
