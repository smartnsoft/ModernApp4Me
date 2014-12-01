using System;

namespace ModernApp4Me.Core.LifeCycle
{

    /// <summary>
    /// This exception should be triggered on the framework methods which allow to throw it, when a business object is not accessible.
    /// This exception should be triggered on the framework methods which allow to throw it, when the user do not have an internet connection.
    /// </summary>
    /// 
    /// <author>Ludovic Roland</author>
    /// <since>2014.03.24</since>
    public sealed class M4MConnectivityException : Exception
    {

        /// <summary>
        /// The error code associated with the exception. Is equal to 0 by default.
        /// </summary>
        public int Code { get; set; }

        public M4MConnectivityException() : this(0)
        {
        }

        public M4MConnectivityException(int code) : this(null, null, 0)
        {
        }

        public M4MConnectivityException(String message, Exception cause) : this(message, cause, 0)
        {
        }

        public M4MConnectivityException(String message) : this(message, 0)
        {
        }

        public M4MConnectivityException(String message, int code) : this(message, null, code)
        {
        }

        public M4MConnectivityException(Exception cause) : this(cause, 0)
        {
        }

        public M4MConnectivityException(Exception cause, int code) : this(null, cause, code)
        {
        }

        public M4MConnectivityException(String message, Exception cause, int code) : base(message, cause)
        {
            Code = code;
        }

    }

}
