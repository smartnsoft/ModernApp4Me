using System.Collections.Generic;
using System.IO.IsolatedStorage;
using wp4me.SnSDebugUtils;

namespace wp4me.SnSIsolatedStorageUtils
{
    /// <summary>
    /// Class that provides functions to work with the IsolatedStorageSettings.
    /// </summary>
    public sealed class SnsIsolatedStorageSettings
    {
        /*******************************************************/
        /** METHODS AND FUNCTIONS.
        /*******************************************************/
        /// <summary>
        /// Method that saves a value into ApplicationSettings.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetApplicationSettings(string key, object value)
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            settings[key] = value;
            settings.Save();
        }

        /// <summary>
        /// Method that returns the ApplicationSettings' value according to the key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>value</returns>
        public static object GetApplicationSettings(string key)
        {
            try
            {
                return IsolatedStorageSettings.ApplicationSettings[key];
            }
            catch (KeyNotFoundException e)
            {
                SnSDebug.ConsoleWriteLine(e.StackTrace);
                return null;
            }
        }
    }
}
