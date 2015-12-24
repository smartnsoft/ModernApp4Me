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
using Windows.UI.Xaml.Controls;
using ModernApp4Me.Universal.App;

namespace ModernApp4Me.Universal.LifeCycle
{

  /// <summary>
  /// An listener which is responsible for handling two things, very common in applications:
  /// * The graphical indicators while an entity is being loaded: on that purpose, the entity needs to implementthis interface so that loading events are triggered;
  /// * The display of errors.
  /// </summary>
  /// <author>Ludovic Roland</author>
  /// <since>2015.12.24</since>
  public interface M4MLoadingAndErrorManager
  {

    /// <summary>
    /// Returns the <see cref="Panel"/> in which data are displayed.
    /// This <see cref="Panel"/> will be displayed or hidden automatically by the <see cref="M4MPage{SuspensionManagerClass}"/> during the life cycle.
    /// </summary>
    /// <returns>the <see cref="Panel"/> in which data are displayed</returns>
    Panel RetrieveMainPanel();

    /// <summary>
    /// Override this in order to return the <see cref="Panel"/> which displays a particular message when a <see cref="Exception"/> is thrown.
    /// This <see cref="Panel"/> will be displayed or hidden automatically by the <see cref="M4MPage"/> during the life cycle.
    /// </summary>
    /// <returns>the <see cref="Panel"/> which display the message or null</returns>
    Panel RetrieveErrorAndRetryPanel();

    /// <summary>
    /// Returns the <see cref="Panel"/> in which a loading experience is displayed.
    /// This <see cref="Panel"/> will be displayed or hidden automatically by the <see cref="M4MPage"/> during the life cycle.
    /// </summary>
    /// <returns>the <see cref="Panel"/> in which data are displayed</returns>
    Panel RetrieveLoadingPanel();

    /// <summary>
    /// Implement this method in order to personnalize to display the error experience when an <see cref="Exception"/> occurs.
    /// </summary
    /// <param name="exception">The exception that occured</param>
    void OnDisplayErrorAndRetryPanel(Exception exception);
  }

}
