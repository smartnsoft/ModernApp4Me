using System.Reflection;
using Windows.ApplicationModel;

namespace ModernApp4Me_WinRT.SnSMetaData
{
    /// <summary>
    /// A class that provides information about the app (name, version, etc...).
    /// </summary>
    public static class SnSAppInfo
    {
        /*******************************************************/
        /** PROPERTIES.
        /*******************************************************/
        public static string FullName
        {
            get { return Package.Current.Id.FullName; }
        }

        public static string Name
        {
            get { return Package.Current.Id.Name; }
        }

        public static string Version
        {
            get { return Package.Current.Id.Version.ToString(); }
        }
    }
}
