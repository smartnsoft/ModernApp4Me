using System;

namespace ModernApp4Me.WP7.SnSApp
{

    /// <summary>
    /// This exception should be triggered on the framework methods which allow to throw it, when a business object is not accessible.
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.24</since>
    public sealed class SnSBusinessObjectUnavailableException : Exception
    {
        /// <summary>
        /// The error code associated with the exception. Is equal to 0 by default.
        /// </summary>
        public int Code { get; set; }

        public SnSBusinessObjectUnavailableException() : this(0)
        {
        }

        public SnSBusinessObjectUnavailableException(int code) : this(null, null, 0)
        {
        }

        public SnSBusinessObjectUnavailableException(String message, Exception cause) : this(message, cause, 0)
        {
        }

        public SnSBusinessObjectUnavailableException(String message) : this(message, 0)
        {
        }

        public SnSBusinessObjectUnavailableException(String message, int code) : this(message, null, code)
        {
        }

        public SnSBusinessObjectUnavailableException(Exception cause) : this(cause, 0)
        {
        }

        public SnSBusinessObjectUnavailableException(Exception cause, int code) : this(null, cause, code)
        {
        }

        public SnSBusinessObjectUnavailableException(String message, Exception cause, int code) : base(message, cause)
        {
            Code = code;
        }
    }
}
