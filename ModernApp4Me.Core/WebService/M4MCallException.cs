// The MIT License (MIT)
//
// Copyright (c) 2017 Smart&Soft
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

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
