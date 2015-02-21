using System;
using System.IO.IsolatedStorage;
using System.Threading;
using ModernApp4Me.Core.Log;

namespace ModernApp4Me.WP8.SnSCache.Settings
{

    /// <summary>
    /// Enables to store some contents into on the <see cref="IsolatedStorageSettings.ApplicationSettings"/>.
    /// The classe implements the singleton pattern and is thread safe !
    /// </summary>
    /// 
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.24</since>
    public sealed class SnSSettings
    {

        private static volatile SnSSettings instance;

        private static readonly object InstanceLock = new Object();

        private readonly Mutex mutex;

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
                M4MLoggerWrapper.Instance.Logger.Warn("Cannot add the settings with the key '"+ key + "'", exception);
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            return isAdded;
        }

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
                M4MLoggerWrapper.Instance.Logger.Warn("Cannot update the settings with the key '" + key + "'", exception);
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            return isUpdated;
        }

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
                M4MLoggerWrapper.Instance.Logger.Warn("Cannot add the settings with the key '" + key + "'", exception);
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            return value;
        }

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
                M4MLoggerWrapper.Instance.Logger.Warn("Cannot remove the settings with the key '" + key + "'", exception);
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            return isRemoved;
        }
    }
}
