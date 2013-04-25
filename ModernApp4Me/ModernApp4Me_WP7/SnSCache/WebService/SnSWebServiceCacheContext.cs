using System;
using System.Data.Linq;
using ModernApp4Me_WP7.SnSLog;
using ModernApp4Me_WP7.SnSWebService;

namespace ModernApp4Me_WP7.SnSCache.WebService
{
    /// <summary>
    /// Represents the webservice database context.
    /// Implements the singleton pattern.
    /// </summary>
    class SnSWebServiceCacheContext : DataContext
    {
        /*******************************************************/
        /** PRIVATES ATTRIBUTES.
        /*******************************************************/
        private const string StringConnection = "Data Source=isostore:/webservicedb.sdf";
        private static volatile SnSWebServiceCacheContext _instance;
        private static readonly object InstanceLock = new Object();


        /*******************************************************/
        /** PUBLIC TABLES
        /*******************************************************/
        public Table<SnSWebServiceModel> WebServices;


        /*******************************************************/
        /** METHODS.
        /*******************************************************/
        /// <summary>
        /// Private constructor according to the singleton pattern.
        /// </summary>
        private SnSWebServiceCacheContext() : base(StringConnection) { }

        /// <summary>
        /// Returns the current instance.
        /// </summary>
        /// <returns></returns>
        public static SnSWebServiceCacheContext GetInstance()
        {
            if (_instance == null)
            {
                lock (InstanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new SnSWebServiceCacheContext();
                        CreateWebServicesDb();
                    }
                }
            }

            return _instance;
        }

        /// <summary>
        /// Creates the database.
        /// </summary>
        /// <returns></returns>
        private static void CreateWebServicesDb()
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
                SnSLogger.Warn(e.StackTrace, "SnSWebServiceCacheContext", "CreateDatabase");
            }
        }
    }
}
