using System;

namespace ModernApp4Me.Core.SnSApp
{

    /// <summary>
    /// This exception should be triggered when the user do not have an internet connection.
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.24</since>
    public sealed class SnSConnectivityException : Exception
    {
        /// <summary>
        /// The error code associated with the exception. Is equal to 0 by default.
        /// </summary>
        public int Code { get; set; }

        public SnSConnectivityException() : this(0)
        {
        }

        public SnSConnectivityException(int code) : this(null, null, 0)
        {
        }

        public SnSConnectivityException(String message, Exception cause) : this(message, cause, 0)
        {
        }

        public SnSConnectivityException(String message) : this(message, 0)
        {
        }

        public SnSConnectivityException(String message, int code) : this(message, null, code)
        {
        }

        public SnSConnectivityException(Exception cause) : this(cause, 0)
        {
        }

        public SnSConnectivityException(Exception cause, int code) : this(null, cause, code)
        {
        }

        public SnSConnectivityException(String message, Exception cause, int code) : base(message, cause)
        {
            Code = code;
        }
    }
}
