using System;
using System.IO.IsolatedStorage;
using ModernApp4Me_Core.SnSLog;

namespace ModernApp4Me_WP8.SnSCache.Settings
{
    /// <summary>
    /// Provides functions to manipulate the IsolatedStorageSettings.
    /// Implement the singleton pattern.
    /// Thread Safety because of the mutex.
    /// </summary>
    public sealed class SnSPersistenceSettings
    {
        /*******************************************************/
        /** ATTRIBUTES.
        /*******************************************************/
        private static volatile SnSPersistenceSettings _instance;
        private static readonly object InstanceLock = new Object();


        /*******************************************************/
        /** METHODS.
        /*******************************************************/
        /// <summary>
        /// Private constructor.
        /// </summary>
        private SnSPersistenceSettings() { }

        /// <summary>
        /// Returns the current instance.
        /// </summary>
        /// <returns></returns>
        public static SnSPersistenceSettings GetInstance()
        {
            if (_instance == null)
            {
                lock (InstanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new SnSPersistenceSettings();
                    }
                }
            }

            return _instance;
        }

        /// <summary>
        /// Saves a value into IsolatedStorageSettings.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddSetting(string key, object value)
        {
            try
            {
                var settings = IsolatedStorageSettings.ApplicationSettings;
                settings.Add(key, value);
                settings.Save();
            }
            catch (Exception e)
            {
                SnSLogger.Warn(e.StackTrace, "SnSPersistenceSettings", "AddSetting");
            }
        }

        /// <summary>
        /// Updates a value into IsolatedStorageSettings.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void UpdateSetting(string key, object value)
        {
            try
            {
                var settings = IsolatedStorageSettings.ApplicationSettings;
                settings[key] = value;
                settings.Save();
            }
            catch (Exception e)
            {
                SnSLogger.Warn(e.StackTrace, "SnSPersistenceSettings", "UpdateSetting");
            }
        }

        /// <summary>
        /// Returns the IsolatedStorageSettings' value according to the key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetSetting(string key)
        {
            object returnValue;

            try
            {
                returnValue = IsolatedStorageSettings.ApplicationSettings[key];
            }
            catch (Exception e)
            {
                SnSLogger.Warn(e.StackTrace, "SnSPersistenceSettings", "GetSetting");
                returnValue = null;
            }

            return returnValue;
        }

        /// <summary>
        /// Deletes the IsolatedStorageSettings' value according to the key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public void RemoveSetting(string key)
        {
            try
            {
                IsolatedStorageSettings.ApplicationSettings.Remove(key);
            }
            catch (Exception e)
            {
                SnSLogger.Warn(e.StackTrace, "SnSPersistenceSettings", "RemoveSetting");
            }
        }
    }
}
