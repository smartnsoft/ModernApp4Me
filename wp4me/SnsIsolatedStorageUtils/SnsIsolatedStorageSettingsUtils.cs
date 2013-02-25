using System.IO.IsolatedStorage;

namespace wp4me.SnsIsolatedStorageUtils
{
    /// <summary>
    /// Class that provides functions to work with the IsolatedStorageSettings.
    /// </summary>
    public sealed class SnsIsolatedStorageSettingsUtils
    {
        /// <summary>
        /// Method that saves a value into ApplicationSettings.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetApplicationSettings(string key, object value)
        {
            IsolatedStorageSettings.ApplicationSettings[key] = value;
        }

        /// <summary>
        /// Method that returns the ApplicationSettings' value according to the key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>value</returns>
        public static object GetApplicationSettings(string key)
        {
            return IsolatedStorageSettings.ApplicationSettings[key];
        }
    }
}
