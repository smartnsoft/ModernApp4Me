// Copyright (C) 2015 Smart&Soft SAS (http://www.smartnsoft.com/) and contributors
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 3 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details.
//
// Contributors:
//   Smart&Soft - initial API and implementation

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
