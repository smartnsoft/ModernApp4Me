using Windows.Security.ExchangeActiveSyncProvisioning;

namespace ModernApp4Me_WinRT.SnSMetaData
{
    /// <summary>
    /// A class that provides information about the device (name, constructor, etc...).
    /// </summary>
    public static class SnSDeviceInfo
    {
        /*******************************************************/
        /** ATTRIBUTES.
        /*******************************************************/
        private static readonly EasClientDeviceInformation DeviceStatus = new EasClientDeviceInformation();


        /*******************************************************/
        /** PROPERTIES.
        /*******************************************************/
        public static string FirmwareVersion
        {
            get
            {
                return DeviceStatus.OperatingSystem;
            }
        }

        public static string Manufacturer
        {
            get
            {
                return DeviceStatus.SystemManufacturer;
            }
        }

        public static string Name
        {
            get
            {
                return DeviceStatus.SystemProductName;
            }
        }
    }
}
