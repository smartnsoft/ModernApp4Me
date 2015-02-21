using ModernApp4Me.Core.Log;

namespace ModernApp4Me.WP8.Template
{

    /// <summary>
    /// Gathers in one place the constants of the application.
    /// </summary>
    public abstract class Constants
    {

        /// <summary>
        /// Indicates the application development status.
        /// </summary>
        public static readonly bool DEVELOPMENT_MODE = "".Equals("");

        /// <summary>
        /// The logging level of the application.
        /// </summary>
        public static readonly M4MLogLevel LOG_LEVEL = Constants.DEVELOPMENT_MODE == true ? M4MLogLevel.Debug : M4MLogLevel.Warn;

        /// <summary>
        /// Indicates whether the analytics are enabled.
        /// </summary>
        public static readonly bool ANALYTICS_ENABLED = !Constants.DEVELOPMENT_MODE;

        /// <summary>
        /// Indicates whether the tracker is enabled.
        /// </summary>
        public static readonly bool BUG_TRACKER_ENABLED = !Constants.DEVELOPMENT_MODE;

        /// <summary>
        /// Indicates whether the base URL for the default services caller.
        /// </summary>
        public const string SERVICES_BASE_URL = "";

    }
}
