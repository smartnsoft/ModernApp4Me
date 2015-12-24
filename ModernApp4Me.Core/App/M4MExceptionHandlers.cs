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
