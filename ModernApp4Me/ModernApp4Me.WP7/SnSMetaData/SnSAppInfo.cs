using System.Reflection;

namespace ModernApp4Me.WP7.SnSMetaData
{

    /// <summary>
    /// A class that provides information about the app (name, version, etc...).
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.24</since>
    public static class SnSAppInfo
    {

        private static readonly AssemblyName NameHelper = new AssemblyName(Assembly.GetExecutingAssembly().FullName);

        public static string FullName
        {
            get { return NameHelper.FullName; }
        }

        public static string Name
        {
            get { return NameHelper.Name; }
        }

        public static string Version
        {
            get { return NameHelper.Version.ToString(); }
        }

    }

}
