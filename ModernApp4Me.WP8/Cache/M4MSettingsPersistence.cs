using System;
using System.IO.IsolatedStorage;
using ModernApp4Me.Core.Log;
using ModernApp4Me.WP8.Log;

namespace ModernApp4Me.WP8.Cache
{

    /// <summary>
    /// Enables to store some contents into on the <see cref="IsolatedStorageSettings.ApplicationSettings"/>.
    /// The classe implements the singleton pattern and is thread safe !
    /// </summary>
    /// 
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.24</since>
    public sealed class M4MSettingsPersistence
    {

        private static volatile M4MSettingsPersistence instance;

        private static readonly object InstanceLock = new Object();

        private M4MSettingsPersistence()
        {
        }

        public static M4MSettingsPersistence Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (InstanceLock)
                    {
                        if (instance == null)
                        {
                            instance = new M4MSettingsPersistence();
                        }
                    }
                }

                return instance;
            }
        }

        public bool AddSetting(string key, object value)
        {
            lock (InstanceLock)
            {
                M4MModernLogger.Instance.Debug("Adding the value : '" + value.ToString() + "'");
                var isAdded = true;

                try
                {
                    var settings = IsolatedStorageSettings.ApplicationSettings;
                    settings.Add(key, value);
                    settings.Save();
                }
                catch (Exception)
                {
                    isAdded = false;
                }

                return isAdded;
            }            
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
            catch (Exception)
            {
                isUpdated = false;
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
            catch (Exception)
            {
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
            catch (Exception)
            {
                isRemoved = false;
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            return isRemoved;
        }
    }
}
