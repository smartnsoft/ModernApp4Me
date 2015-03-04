// Copyright (C) 2015 Smart&Soft SAS (http://www.smartnsoft.com/) and contributors
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 3 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details.
//
// Contributors:
//   Smart&Soft - initial API and implementation

using System;
using System.IO.IsolatedStorage;

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

        /// <summary>
        /// Retrieves the business object stores into the Settings and corresponding to the specifying key.
        /// </summary>
        /// <param name="key">the key corresponding to the business object</param>
        /// <returns>an <see cref="object"/> which can be null</returns>
        public object Get(string key)
        {
            lock (Instance)
            {
                object returnValue = null;

                try
                {
                    returnValue = IsolatedStorageSettings.ApplicationSettings[key];
                }
                catch (Exception)
                {
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
                var isRemoved = true;

                try
                {
                    IsolatedStorageSettings.ApplicationSettings.Remove(key);
                }
                catch (Exception)
                {
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
                var isUpdated = true;

                try
                {
                    var settings = IsolatedStorageSettings.ApplicationSettings;
                    settings[key] = value;
                    settings.Save();
                }
                catch (Exception)
                {
                    isUpdated = false;
                }

                return isUpdated;
            }
        }
        
    }
}
