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
