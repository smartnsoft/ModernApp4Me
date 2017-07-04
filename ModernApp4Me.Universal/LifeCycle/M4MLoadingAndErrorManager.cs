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
