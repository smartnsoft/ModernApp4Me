using System;
using System.Collections.Generic;
using ModernApp4Me.WP8.Log;

namespace ModernApp4Me.WP8.Cache
{

    /// <summary>
    /// A class which enables to cache data in RAM only.
    /// This class implements the singleton pattern and is thread safe !
    /// </summary>
    /// 
    /// <author>Ludovic Roland</author>
    /// <since>2014.03.24</since>
    // TODO : add logs
    public class M4MMemoryCacher
    {

        /// <summary>
        /// A class which reprensents an object stored into the <see cref="M4MMemoryCacher"/>.
        /// </summary>
        /// 
        /// <author>Ludovic Roland</author>
        /// <since>2014.03.24</since>
        public sealed class M4MMemoryCacherObject
        {

            public DateTime Date { get; set; }

            public object Value { get; set; }

        }

        private static volatile M4MMemoryCacher instance;

        private static readonly object InstanceLock = new Object();
        
        private readonly Dictionary<string, M4MMemoryCacherObject> memoryCacher;

        private M4MMemoryCacher()
        {
            memoryCacher = new Dictionary<string, M4MMemoryCacherObject>();
        }

        public static M4MMemoryCacher Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (InstanceLock)
                    {
                        if (instance == null)
                        {
                            instance = new M4MMemoryCacher();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Adds the business object corresponding to the provided parameter into the <see cref="M4MMemoryCacher"/> by specifying the key.
        /// </summary>
        /// <param name="key">the key corresponding to the business object</param>
        /// <param name="value">the business object</param>
        /// <returns>true in case of success, false otherwise</returns>
        public bool Add(string key, object value)
        {
            lock (Instance)
            {
                if (M4MModernLogger.Instance.IsDebugEnabled() == true)
                {
                    M4MModernLogger.Instance.Debug("Adding an object with the key '" + key + "' into the Memory Cacher");
                }

                var isAdded = true;

                try
                {
                    memoryCacher.Add(key, new M4MMemoryCacherObject { Date = DateTime.Now, Value = value });
                }
                catch (Exception exception)
                {
                    if (M4MModernLogger.Instance.IsErrorEnabled() == true)
                    {
                        M4MModernLogger.Instance.Error("An error occurs while adding an object with the key : '" + key + "' into the Memory Cacher", exception);
                    }

                    isAdded = false;
                }

                return isAdded;
            }
        }

        /// <summary>
        /// Retrieves the business object stores into the <see cref="M4MMemoryCacher"/> and corresponding to the specifying key.
        /// </summary>
        /// <param name="key">the key corresponding to the business object</param>
        /// <returns>a <see cref="M4MMemoryCacherObject"/> which can be null</returns>
        public M4MMemoryCacherObject Get(string key)
        {
            lock (Instance)
            {
                if (M4MModernLogger.Instance.IsDebugEnabled() == true)
                {
                    M4MModernLogger.Instance.Debug("Getting the object with the key '" + key + "' from the Memory Cacher");
                }

                M4MMemoryCacherObject returnValue = null;

                try
                {
                    returnValue = memoryCacher[key];
                }
                catch (Exception exception)
                {
                    if (M4MModernLogger.Instance.IsErrorEnabled() == true)
                    {
                        M4MModernLogger.Instance.Error("An error occurs while reading the object with the key : '" + key + "' from the Memory Cacher", exception);
                    }

                    returnValue = null;
                }

                return returnValue;
            }
        }

        /// <summary>
        /// Deletes the business object stores into the <see cref="M4MMemoryCacher"/> and corresponding to the specifying key.
        /// </summary>
        /// <param name="key">the key corresponding to the business object</param>
        /// <returns>true in case of success, false otherwise</returns>
        public bool Remove(string key)
        {
            lock (Instance)
            {
                if (M4MModernLogger.Instance.IsDebugEnabled() == true)
                {
                    M4MModernLogger.Instance.Debug("Removing the object with the key '" + key + "' from the Memory Cacher");
                }

                var isRemoved = true;

                try
                {
                    memoryCacher.Remove(key);
                }
                catch (Exception)
                {
                    if (M4MModernLogger.Instance.IsErrorEnabled() == true)
                    {
                        M4MModernLogger.Instance.Error("An error occurs while removing the object with the key : '" + key + "' from the Memory Cacher", exception);
                    }

                    isRemoved = false;
                }

                return isRemoved;
            }
        }

        /// <summary>
        /// Updates the business object stores into the <see cref="M4MMemoryCacher"/> and corresponding to the specifying key.
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
                    M4MModernLogger.Instance.Debug("Updating the object with the key '" + key + "' into the Memory Cacher");
                }

                var isUpdated = true;

                try
                {
                    memoryCacher[key] = new M4MMemoryCacherObject { Date = DateTime.Now, Value = value };
                }
                catch (Exception exception)
                {
                    if (M4MModernLogger.Instance.IsErrorEnabled() == true)
                    {
                        M4MModernLogger.Instance.Error("An error occurs while updating the object with the key : '" + key + "' intro the Memory Cacher", exception);
                    }

                    isUpdated = false;
                }

                return isUpdated;
            }
        }

        /// <summary>
        /// Checks if the specifying key exists in into the <see cref="M4MMemoryCacher"/>.
        /// </summary>
        /// <param name="key">the key to test</param>
        /// <returns>true is the key exists, false otherwise</returns>
        public bool IsKeyExists(string key)
        {
            lock (Instance)
            {
                return memoryCacher.ContainsKey(key);
            }
        }

    }

}
