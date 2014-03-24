using Windows.ApplicationModel;

namespace ModernApp4Me.Win8.SnSMetaData
{

    /// <summary>
    /// A class that provides information about the app (name, version, etc...).
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.24</since>
    public static class SnSAppInfo
    {

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
