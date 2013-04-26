using System;
using System.Data.Linq;
using ModernApp4Me_WP8.SnSFile;
using ModernApp4Me_Core.SnSLog;

namespace ModernApp4Me_WP8.SnSCache.File
{
    /// <summary>
    /// Represents the file database context.
    /// Implements the singleton pattern.
    /// </summary>
    class SnSPeristenceFileContext : DataContext
    {
        /*******************************************************/
        /** PRIVATES ATTRIBUTES.
        /*******************************************************/
        private const string StringConnection = "Data Source=isostore:/filesdb.sdf";
        private static volatile SnSPeristenceFileContext _instance;
        private static readonly object InstanceLock = new Object();


        /*******************************************************/
        /** PUBLIC TABLES
        /*******************************************************/
        public Table<SnSFileModel> Files;


        /*******************************************************/
        /** METHODS.
        /*******************************************************/
        /// <summary>
        /// Private constructor according to the singleton pattern.
        /// </summary>
        private SnSPeristenceFileContext() : base(StringConnection) { }

        /// <summary>
        /// Returns the current instance.
        /// </summary>
        /// <returns></returns>
        public static SnSPeristenceFileContext GetInstance()
        {
            if (_instance == null)
            {
                lock (InstanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new SnSPeristenceFileContext();
                        CreateFilesDb();
                    }
                }
            }

            return _instance;
        }

        /// <summary>
        /// Creates the database.
        /// </summary>
        /// <returns></returns>
        private static void CreateFilesDb()
        {
            try
            {
                if (!_instance.DatabaseExists())
                {
                    _instance.CreateDatabase();
                }
            }
            catch (Exception e)
            {
                SnSLogger.Warn(e.StackTrace, "SnSPeristenceFileContext", "CreateFilesDb");
            }
        }
    }
}
