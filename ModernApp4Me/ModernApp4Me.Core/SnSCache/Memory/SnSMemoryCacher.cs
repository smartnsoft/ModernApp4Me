using System;
using System.Collections.Generic;
using System.Threading;
using ModernApp4Me.Core.SnSLog;

namespace ModernApp4Me.Core.SnSCache.Memory
{

    /// <summary>
    /// A class which enables to cache the result of web service calls in RAM only.
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.24</since>
    public sealed class SnSMemoryCacher
    {

        private static volatile SnSMemoryCacher instance;

        private static readonly object InstanceLock = new Object();
        
        private readonly Mutex mutex;

        private readonly Dictionary<string, SnSMemoryCacherObject> memoryCacher;

        /// <summary>
        /// Private constructor.
        /// </summary>
        private SnSMemoryCacher()
        {
            memoryCacher = new Dictionary<string, SnSMemoryCacherObject>();
            mutex = new Mutex(false, "memory cache access mutex");
        }

        public static SnSMemoryCacher Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (InstanceLock)
                    {
                        if (instance == null)
                        {
                            instance = new SnSMemoryCacher();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Saves a value into the memory cacher.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>true in case of success, false otherwise</returns>
        public bool Add(string key, object value)
        {
            var isAdded = true;

            try
            {
                mutex.WaitOne();
                memoryCacher.Add(key, new SnSMemoryCacherObject {Date = DateTime.Now, Value = value});
            }
            catch (Exception exception)
            {
                isAdded = false;
                SnSLoggerWrapper.Instance.Logger.Warn("Cannot add the entry with key '" + key + "'", exception);
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            return isAdded;
        }

        /// <summary>
        /// Returns the memory cacher value according to the key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>the value or null</returns>
        public SnSMemoryCacherObject Get(string key)
        {
            SnSMemoryCacherObject returnValue = null;

            try
            {
                mutex.WaitOne();
                returnValue = memoryCacher[key];
            }
            catch (Exception exception)
            {
                SnSLoggerWrapper.Instance.Logger.Warn("Cannot get the entry with key '" + key + "'", exception);
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            return returnValue;
        }

        /// <summary>
        /// Deletes the memory cacher value according to the key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>true in case of success, false otherwise</returns>
        public bool Remove(string key)
        {
            var isRemoved = true;

            try
            {
                mutex.WaitOne();
                memoryCacher.Remove(key);
            }
            catch (Exception exception)
            {
                isRemoved = false;
                SnSLoggerWrapper.Instance.Logger.Warn("Cannot remove the entry with key '" + key + "'", exception);
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            return isRemoved;
        }

        /// <summary>
        /// Updates the memory cacher value according to the key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>true in case of success, false otherwise</returns>
        public bool Update(string key, object value)
        {
            var isUpdated = true;

            try
            {
                mutex.WaitOne();
                memoryCacher[key] = new SnSMemoryCacherObject {Date = DateTime.Now, Value = value};
            }
            catch (Exception exception)
            {
                isUpdated = false;
                SnSLoggerWrapper.Instance.Logger.Warn("Cannot update the entry with key '" + key + "'", exception);
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            return isUpdated;
        }

        /// <summary>
        /// Checks if the key is already used in the memory cacher.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>true is the key exists.</returns>
        public bool IsKeyExists(string key)
        {
            return memoryCacher.ContainsKey(key);
        }
    }
}
