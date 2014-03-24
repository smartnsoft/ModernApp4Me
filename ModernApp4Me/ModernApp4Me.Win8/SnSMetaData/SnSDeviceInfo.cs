using Windows.Security.ExchangeActiveSyncProvisioning;

namespace ModernApp4Me.Win8.SnSMetaData
{
    /// <summary>
    /// A class that provides information about the device (name, constructor, etc...).
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.24</since>
    public static class SnSDeviceInfo
    {

        private static readonly EasClientDeviceInformation DeviceStatus = new EasClientDeviceInformation();

        public static string FirmwareVersion
        {
            get { return DeviceStatus.OperatingSystem; } 
        }

        public static string Manufacturer
        {
            get { return DeviceStatus.SystemManufacturer; }
        }

        public static string Name
        {
            get { return DeviceStatus.SystemProductName; }
        }

    }

}
