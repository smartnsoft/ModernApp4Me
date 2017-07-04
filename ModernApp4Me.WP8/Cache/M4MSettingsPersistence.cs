// The MIT License (MIT)
//
// Copyright (c) 2017 Smart&Soft
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

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
