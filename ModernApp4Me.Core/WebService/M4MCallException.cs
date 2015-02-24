using System;

namespace ModernApp4Me.Core.WebService
{

    /// <summary>
    /// The exception that will be thrown if any problem occurs during a web service call.
    /// </summary>
    /// 
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.25</since>
    public sealed class M4MCallException : Exception
    {

        public int Code { get; set; }

        public M4MCallException() : this(0)
        {
        }

        public M4MCallException(int code) : this(null, null, 0)
        {
        }

        public M4MCallException(String message, Exception cause) : this(message, cause, 0)
        {
        }

        public M4MCallException(String message) : this(message, 0)
        {
        }

        public M4MCallException(String message, int code) : this(message, null, code)
        {
        }

        public M4MCallException(Exception cause) : this(cause, 0)
        {
        }

        public M4MCallException(Exception cause, int code) : this(null, cause, code)
        {
        }

        public M4MCallException(String message, Exception cause, int code)
            : base(message, cause)
        {
            Code = code;
        }
    }
}
