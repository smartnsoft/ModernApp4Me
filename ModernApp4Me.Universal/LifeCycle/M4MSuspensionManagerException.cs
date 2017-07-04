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
// SOFTWARE.ion

using System;

namespace ModernApp4Me.Universal.LifeCycle
{

  /// <author>Ludovic Roland</author>
  /// <since>2015.11.05</since>
  /// <summary>
  /// This exception should be triggered in the <see cref="M4MSuspensionManager{SuspensionManagerClass}"/> class, when an issue appears
  /// into the <see cref="M4MSuspensionManager{SuspensionManagerClass}.SaveAsync"/> 
  /// and <see cref="M4MSuspensionManager{SuspensionManagerClass}.RestoreAsync"/> methods.
  /// </summary>
  public sealed class M4MSuspensionManagerException : Exception
  {

    public enum SuspensionManagerOperation
    {
      Restore, Save
    }

    public M4MSuspensionManagerException()
    {
    }

    public M4MSuspensionManagerException(SuspensionManagerOperation operation, Exception exception)
      : base("The Suspension Manager failed the " + operation + " operation", exception)
    {
    }

  }

}
