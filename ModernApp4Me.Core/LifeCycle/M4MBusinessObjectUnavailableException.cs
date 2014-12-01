using System;

namespace ModernApp4Me.Core.LifeCycle
{

    /// <summary>
    /// This exception should be triggered on the framework methods which allow to throw it, when a business object is not accessible.
    /// </summary>
    /// 
    /// <author>Ludovic Roland</author>
    /// <since>2014.03.24</since>
    public sealed class M4MBusinessObjectUnavailableException : Exception
    {

        /// <summary>
        /// The error code associated with the exception. Is equal to 0 by default.
        /// </summary>
        public int Code { get; set; }

        public M4MBusinessObjectUnavailableException() : this(0)
        {
        }

        public M4MBusinessObjectUnavailableException(int code) : this(null, null, 0)
        {
        }

        public M4MBusinessObjectUnavailableException(String message, Exception cause) : this(message, cause, 0)
        {
        }

        public M4MBusinessObjectUnavailableException(String message) : this(message, 0)
        {
        }

        public M4MBusinessObjectUnavailableException(String message, int code) : this(message, null, code)
        {
        }

        public M4MBusinessObjectUnavailableException(Exception cause) : this(cause, 0)
        {
        }

        public M4MBusinessObjectUnavailableException(Exception cause, int code) : this(null, cause, code)
        {
        }

        public M4MBusinessObjectUnavailableException(String message, Exception cause, int code) : base(message, cause)
        {
            Code = code;
        }

    }

}
