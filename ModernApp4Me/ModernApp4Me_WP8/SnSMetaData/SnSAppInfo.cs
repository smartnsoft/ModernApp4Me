using System.Reflection;

namespace ModernApp4Me_WP8.SnSMetaData
{
    /// <summary>
    /// A class that provides information about the app (name, version, etc...).
    /// </summary>
    public static class SnSAppInfo
    {
        /*******************************************************/
        /** ATTRIBUTES.
        /*******************************************************/
        private static readonly AssemblyName NameHelper = new AssemblyName(Assembly.GetExecutingAssembly().FullName);


        /*******************************************************/
        /** PROPERTIES.
        /*******************************************************/
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
