// Copyright (C) 2015 Smart&Soft SAS (http://www.smartnsoft.com/) and contributors
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details.
//
// Contributors:
//   Smart&Soft - initial API and implementation

using System;

namespace ModernApp4Me.Core.LifeCycle
{

    /// <summary>
    /// This exception should be triggered on the framework methods which allow to throw it, when a business object is not accessible.
    /// </summary>
    /// 
    /// <author>Ludovic Roland</author>
    /// <since>2014.03.24</since>
    public class M4MBusinessObjectUnavailableException : Exception
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
