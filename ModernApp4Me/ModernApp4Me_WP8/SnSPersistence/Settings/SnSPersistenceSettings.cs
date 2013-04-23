using System.Collections.Generic;
using System.IO.IsolatedStorage;
using ModernApp4Me_Core.SnSLog;

namespace ModernApp4Me_WP8.SnSPersistence.Settings
{
    /// <summary>
    /// Provides functions to manipulate the IsolatedStorageSettings.
    /// </summary>
    public static class SnSPersistenceSettings
    {
        /*******************************************************/
        /** METHODS AND FUNCTIONS.
        /*******************************************************/
        /// <summary>
        /// Saves a value into IsolatedStorageSettings.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetSetting(string key, object value)
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            settings[key] = value;
            settings.Save();
        }

        /// <summary>
        /// Returns the IsolatedStorageSettings' value according to the key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetSetting(string key)
        {
            try
            {
                return IsolatedStorageSettings.ApplicationSettings[key];
            }
            catch (KeyNotFoundException e)
            {
                SnSLogger.Warn(e.StackTrace, "SnSPersistenceSettings", "GetApplicationSettings");
                return null;
            }
        }

        /// <summary>
        /// Deletes the IsolatedStorageSettings' value according to the key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void DeleteSetting(string key)
        {
            IsolatedStorageSettings.ApplicationSettings.Remove(key);
        }
    }
}
