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
using ModernApp4Me.Core.LifeCycle;

namespace ModernApp4Me.Core.App
{

  /// <summary>
  /// A class base that should be extend in order to implement an exception handler which pops-up error dialog boxes.
  /// </summary>
  /// 
  /// <author>Ludovic Roland</author>
  /// <since>2014.03.24</since>
  public abstract class M4MExceptionHandlers
  {

    public M4Mi18N I18N { get; set; }

    public virtual void AnalyseException(Exception exception)
    {
      if (exception is M4MBusinessObjectUnavailableException)
      {
        ShowMessageBox(I18N.BusinessObjectUnavailableHint);
      }
      else if (exception is M4MConnectivityException)
      {
        ShowConnectivityMessageBox(I18N.ConnectivityProblemHint);
      }
      else
      {
        ShowMessageBox(I18N.InhandledProblemHint);
      }
    }

    public virtual string GetDefaultErrorMessages(Exception exception)
    {
      if (exception is M4MBusinessObjectUnavailableException)
      {
        return I18N.BusinessObjectUnavailableHint;
      }
      
      if (exception is M4MConnectivityException)
      {
        return I18N.ConnectivityProblemHint;
      }
      
      return I18N.InhandledProblemHint;
    }

    protected abstract void ShowMessageBox(string message);

    protected abstract void ShowConnectivityMessageBox(string message);

  }

}
