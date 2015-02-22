using System;
using System.Collections.Generic;
using System.Threading;
using ModernApp4Me.Core.Log;

namespace ModernApp4Me.Core.Cache
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
        
        private readonly Mutex mutex;

        private readonly Dictionary<string, M4MMemoryCacherObject> memoryCacher;

        private M4MMemoryCacher()
        {
            memoryCacher = new Dictionary<string, M4MMemoryCacherObject>();
            mutex = new Mutex(false, "memory cache access mutex");
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
            var isAdded = true;

            try
            {
                mutex.WaitOne();
                memoryCacher.Add(key, new M4MMemoryCacherObject {Date = DateTime.Now, Value = value});
            }
            catch (Exception)
            {
                isAdded = false;
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            return isAdded;
        }

        /// <summary>
        /// Retrieves the business object stores into the <see cref="M4MMemoryCacher"/> and corresponding to the specifying key.
        /// </summary>
        /// <param name="key">the key corresponding to the business object</param>
        /// <returns>a <see cref="M4MMemoryCacherObject"/> which can be null</returns>
        public M4MMemoryCacherObject Get(string key)
        {
            M4MMemoryCacherObject returnValue = null;

            try
            {
                mutex.WaitOne();
                returnValue = memoryCacher[key];
            }
            catch (Exception)
            {
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            return returnValue;
        }

        /// <summary>
        /// Deletes the business object stores into the <see cref="M4MMemoryCacher"/> and corresponding to the specifying key.
        /// </summary>
        /// <param name="key">the key corresponding to the business object</param>
        /// <returns>true in case of success, false otherwise</returns>
        public bool Remove(string key)
        {
            var isRemoved = true;

            try
            {
                mutex.WaitOne();
                memoryCacher.Remove(key);
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

        /// <summary>
        /// Updates the business object stores into the <see cref="M4MMemoryCacher"/> and corresponding to the specifying key.
        /// </summary>
        /// <param name="key">the key corresponding to the business object</param>
        /// <param name="value">the business object</param>
        /// <returns>true in case of success, false otherwise</returns>
        public bool Update(string key, object value)
        {
            var isUpdated = true;

            try
            {
                mutex.WaitOne();
                memoryCacher[key] = new M4MMemoryCacherObject {Date = DateTime.Now, Value = value};
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

        /// <summary>
        /// Checks if the specifying key exists in into the <see cref="M4MMemoryCacher"/>.
        /// </summary>
        /// <param name="key">the key to test</param>
        /// <returns>true is the key exists, false otherwise</returns>
        public bool IsKeyExists(string key)
        {
            return memoryCacher.ContainsKey(key);
        }

    }

}
