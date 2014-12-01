using System;
using System.IO.IsolatedStorage;
using System.Threading;
using ModernApp4Me.Core.SnSLog;

namespace ModernApp4Me.WP8.SnSCache.Settings
{

    /// <summary>
    /// Provides functions to manipulate the IsolatedStorageSettings.
    /// Implement the singleton pattern.
    /// Thread Safety because of the mutex.
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.24</since>
    public sealed class SnSSettings
    {

        private static volatile SnSSettings instance;

        private static readonly object InstanceLock = new Object();

        private readonly Mutex mutex;

        /// <summary>
        /// Private constructor.
        /// </summary>
        private SnSSettings()
        {
            mutex = new Mutex(false, "settings access mutex");
        }

        public static SnSSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (InstanceLock)
                    {
                        if (instance == null)
                        {
                            instance = new SnSSettings();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Saves a value into IsolatedStorageSettings.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>true in case of success, false otherwise</returns>
        public bool AddSetting(string key, object value)
        {
            var isAdded = true;

            try
            {
                mutex.WaitOne();

                var settings = IsolatedStorageSettings.ApplicationSettings;
                settings.Add(key, value);
                settings.Save();
            }
            catch (Exception exception)
            {
                isAdded = false;
                SnSLoggerWrapper.Instance.Logger.Warn("Cannot add the settings with the key '"+ key + "'", exception);
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            return isAdded;
        }

        /// <summary>
        /// Updates a value into IsolatedStorageSettings.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>true in case of success, false otherwise</returns>
        public bool UpdateSetting(string key, object value)
        {
            var isUpdated = true;

            try
            {
                mutex.WaitOne();

                var settings = IsolatedStorageSettings.ApplicationSettings;
                settings[key] = value;
                settings.Save();
            }
            catch (Exception exception)
            {
                isUpdated = false;
                SnSLoggerWrapper.Instance.Logger.Warn("Cannot update the settings with the key '" + key + "'", exception);
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            return isUpdated;
        }

        /// <summary>
        /// Returns the IsolatedStorageSettings' value according to the key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetSetting(string key)
        {
            object value = null;

            try
            {
                mutex.WaitOne();
                value = IsolatedStorageSettings.ApplicationSettings[key];
            }
            catch (Exception exception)
            {
                SnSLoggerWrapper.Instance.Logger.Warn("Cannot add the settings with the key '" + key + "'", exception);
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            return value;
        }

        /// <summary>
        /// Deletes the IsolatedStorageSettings' value according to the key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>true in case of success, false otherwise</returns>
        public bool RemoveSetting(string key)
        {
            var isRemoved = true;

            try
            {
                mutex.WaitOne();

                IsolatedStorageSettings.ApplicationSettings.Remove(key);
            }
            catch (Exception exception)
            {
                isRemoved = false;
                SnSLoggerWrapper.Instance.Logger.Warn("Cannot remove the settings with the key '" + key + "'", exception);
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            return isRemoved;
        }
    }
}
