using System;
using System.IO.IsolatedStorage;
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

        /// <summary>
        /// Adds the business object corresponding to the provided parameter into the Settings by specifying the key.
        /// </summary>
        /// <param name="key">the key corresponding to the business object</param>
        /// <param name="value">the business object</param>
        /// <returns>true in case of success, false otherwise</returns>
        public bool Add(string key, object value)
        {
            lock (InstanceLock)
            {
                if (M4MModernLogger.Instance.IsDebugEnabled() == true)
                {
                    M4MModernLogger.Instance.Debug("Adding an object with the key '" + key + "' into the Settings");
                }

                var isAdded = true;

                try
                {
                    var settings = IsolatedStorageSettings.ApplicationSettings;
                    settings.Add(key, value);
                    settings.Save();
                }
                catch (Exception exception)
                {
                    if (M4MModernLogger.Instance.IsErrorEnabled() == true)
                    {
                        M4MModernLogger.Instance.Error("An error occurs while adding an object with the key : '" + key + "' into the Settings", exception);
                    }

                    isAdded = false;
                }

                return isAdded;
            }
        }

        /// <summary>
        /// Retrieves the business object stores into the Settings and corresponding to the specifying key.
        /// </summary>
        /// <param name="key">the key corresponding to the business object</param>
        /// <returns>an <see cref="object"/> which can be null</returns>
        public object Get(string key)
        {
            lock (Instance)
            {
                if (M4MModernLogger.Instance.IsDebugEnabled() == true)
                {
                    M4MModernLogger.Instance.Debug("Getting the object with the key '" + key + "' from the Settings");
                }

                object returnValue = null;

                try
                {
                    returnValue = IsolatedStorageSettings.ApplicationSettings[key];
                }
                catch (Exception exception)
                {
                    if (M4MModernLogger.Instance.IsErrorEnabled() == true)
                    {
                        M4MModernLogger.Instance.Error("An error occurs while reading the object with the key : '" + key + "' from the Settings", exception);
                    }

                    returnValue = null;
                }

                return returnValue;
            }
        }

        /// <summary>
        /// Deletes the business object stores into the Settings and corresponding to the specifying key.
        /// </summary>
        /// <param name="key">the key corresponding to the business object</param>
        /// <returns>true in case of success, false otherwise</returns>
        public bool Remove(string key)
        {
            lock (Instance)
            {
                if (M4MModernLogger.Instance.IsDebugEnabled() == true)
                {
                    M4MModernLogger.Instance.Debug("Removing the object with the key '" + key + "' from the Settings");
                }

                var isRemoved = true;

                try
                {
                    IsolatedStorageSettings.ApplicationSettings.Remove(key);
                }
                catch (Exception exception)
                {
                    if (M4MModernLogger.Instance.IsErrorEnabled() == true)
                    {
                        M4MModernLogger.Instance.Error("An error occurs while removing the object with the key : '" + key + "' from the Settings", exception);
                    }

                    isRemoved = false;
                }

                return isRemoved;
            }
        }

        /// <summary>
        /// Updates the business object stores into the Settings and corresponding to the specifying key.
        /// </summary>
        /// <param name="key">the key corresponding to the business object</param>
        /// <param name="value">the business object</param>
        /// <returns>true in case of success, false otherwise</returns>
        public bool Update(string key, object value)
        {
            lock (Instance)
            {
                if (M4MModernLogger.Instance.IsDebugEnabled() == true)
                {
                    M4MModernLogger.Instance.Debug("Updating the object with the key '" + key + "' into the Settings");
                }

                var isUpdated = true;

                try
                {
                    var settings = IsolatedStorageSettings.ApplicationSettings;
                    settings[key] = value;
                    settings.Save();
                }
                catch (Exception exception)
                {
                    if (M4MModernLogger.Instance.IsErrorEnabled() == true)
                    {
                        M4MModernLogger.Instance.Error("An error occurs while updating the object with the key : '" + key + "' intro the Settings", exception);
                    }

                    isUpdated = false;
                }

                return isUpdated;
            }
        }
        
    }
}
