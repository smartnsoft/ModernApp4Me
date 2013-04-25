using System;
using System.Collections.Generic;
using System.Threading;
using ModernApp4Me_Core.SnSLog;

namespace ModernApp4Me_Core.SnSCache
{
    /// <summary>
    /// A class which enables to cache the result of web service calls in RAM only.
    /// </summary>
    public sealed class SnSMemoryCacher
    {
        /*******************************************************/
        /** ATTRIBUTES.
        /*******************************************************/
        private static volatile SnSMemoryCacher _instance;
        private static readonly object InstanceLock = new Object();
        
        private readonly Mutex _mutex;
        private readonly Dictionary<string, SnSMemoryCacherObject> _memoryCacher;


        /*******************************************************/
        /** METHODS.
        /*******************************************************/
        /// <summary>
        /// Private constructor.
        /// </summary>
        private SnSMemoryCacher()
        {
            _memoryCacher = new Dictionary<string, SnSMemoryCacherObject>();
            _mutex = new Mutex(true, "memory cache access mutex");
        }

        /// <summary>
        /// Returns the current instance.
        /// </summary>
        /// <returns></returns>
        public SnSMemoryCacher GetInstance()
        {
            if (_instance == null)
            {
                lock (InstanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new SnSMemoryCacher();
                    }
                }
            }

            return _instance;
        }

        /// <summary>
        /// Saves a value into the memory cacher.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, object value)
        {
            try
            {
                _mutex.WaitOne();
                _memoryCacher.Add(key, new SnSMemoryCacherObject {Date = DateTime.Now, Value = value});
            }
            catch (Exception e)
            {
                SnSLogger.Warn(e.StackTrace, "SnSMemoryCacher", "Add");
            }
            finally
            {
                _mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Returns the memory cacher value according to the key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object Get(string key)
        {
            object returnValue;

            try
            {
                _mutex.WaitOne();
                returnValue = _memoryCacher[key];
            }
            catch (Exception e)
            {
                SnSLogger.Warn(e.StackTrace, "SnSMemoryCacher", "Get");
                returnValue = null;
            }
            finally
            {
                _mutex.ReleaseMutex();
            }

            return returnValue;
        }

        /// <summary>
        /// Deletes the memory cacher value according to the key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public void Remove(string key)
        {
            try
            {
                _mutex.WaitOne();
                _memoryCacher.Remove(key);
            }
            catch (Exception e)
            {
                SnSLogger.Warn(e.StackTrace, "SnSMemoryCacher", "Remove");
            }
            finally
            {
                _mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Updates the memory cacher value according to the key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public void Update(string key, object value)
        {
            try
            {
                _mutex.WaitOne();
                _memoryCacher[key] = new SnSMemoryCacherObject {Date = DateTime.Now, Value = value};
            }
            catch (Exception e)
            {
                SnSLogger.Warn(e.StackTrace, "SnSMemoryCacher", "Update");
            }
            finally
            {
                _mutex.ReleaseMutex();
            }
        }
    }
}
