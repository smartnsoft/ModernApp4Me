using Microsoft.Phone.Net.NetworkInformation;
using wp4me.SnsIsolatedStorageUtils;

namespace wp4me.SnSNetworkUtils
{
    /// <summary>
    /// Class that provides functions to work with network information.
    /// </summary>
    public sealed class SnsNetworkUtils
    {
        /// <summary>
        /// Methods that indicates if the phone has a network connection (edge, 3G, wifi, etc.).
        /// </summary>
        /// <returns>true or false depending on the phone's network connection</returns>
        public static bool HasNetworkConnection()
        {
            return NetworkInterface.NetworkInterfaceType == NetworkInterfaceType.None;
        }
    }
}
