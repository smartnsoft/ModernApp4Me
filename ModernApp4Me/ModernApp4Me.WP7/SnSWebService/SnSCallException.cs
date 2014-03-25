using System;

namespace ModernApp4Me.WP7.SnSWebService
{

    /// <summary>
    /// This exception should be triggered when the result code of a call to a web method is not OK (not 20X).
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.25</since>
    public sealed class SnSCallException : Exception
    {
        /// <summary>
        /// The error code associated with the exception. Is equal to 0 by default.
        /// </summary>
        public int Code { get; set; }

        public SnSCallException() : this(0)
        {
        }

        public SnSCallException(int code) : this(null, null, 0)
        {
        }

        public SnSCallException(String message, Exception cause) : this(message, cause, 0)
        {
        }

        public SnSCallException(String message) : this(message, 0)
        {
        }

        public SnSCallException(String message, int code) : this(message, null, code)
        {
        }

        public SnSCallException(Exception cause) : this(cause, 0)
        {
        }

        public SnSCallException(Exception cause, int code) : this(null, cause, code)
        {
        }

        public SnSCallException(String message, Exception cause, int code) : base(message, cause)
        {
            Code = code;
        }
    }
}
