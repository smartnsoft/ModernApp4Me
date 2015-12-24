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
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using ModernApp4Me.Core.ViewModel;
using ModernApp4Me.Universal.App;

namespace ModernApp4Me.Universal.LifeCycle
{

  /// <author>Ludovic ROLAND</author>
  /// <since>2015.10.28</since>
  /// <summary>
  /// Identifies a typical life cycle work-flow for a <see cref="Page" /> of the framework. 
  /// The framework captures those entities work-flow and divides it into typical actions:
  /// 1. extracts <see cref="Windows.UI.Xaml.Controls" />
  /// 2. retrieves the business objects that are represented on the entity
  /// 3. bind the <see cref="Windows.UI.Xaml.Controls" /> to the previously extracted business objects.
  /// 
  /// When deriving from this interface, just implement this interface method. 
  /// </summary>
  public interface M4MLifeCycle
  {

    /// <summary>
    /// This is the place where to load the business objects, from memory, local persistence, via web services, necessary for the entity processing.
    /// This callback is invoked from a background thread, and not the UI thread.
    /// This method is invoked only once during the <see cref="M4MPage"/> life cycle.
    /// Never invoke this method, only the framework should, because this is a callback!
    /// </summary>
    /// <returns>a class that implements <see cref="M4MBaseViewModel"/> or null if you set <see cref="M4Mizer{SuspensionManagerClass}.IsMVVMUsed"/> to false</returns>
    Task<M4MBaseViewModel> ComputeViewModel();

    /// <summary>
    /// This is the place where the implementing class can initialize the previously retrieved graphical objects.
    /// This method is invoked only once during the <see cref="M4MPage"/> life cycle.
    /// It is ensured that this method will be invoked from the UI thread!
    /// Never invoke this method, only the framework should, because this is a callback!
    /// </summary>
    void OnFulfillDisplayObjects();

    /// <summary>
    /// This is the place where to retrieve data from the <see cref="NavigationEventArgs.Parameter"/>.
    /// This method is invoked before the <see cref="ComputeViewModel()"/>.
    /// </summary>
    /// <param name="navigationParameter">The parameter value passed to <see cref="Frame.Navigate(Type, object)"/> </param>
    void LoadQueryString(Object navigationParameter);

    /// <summary>
    /// This is the place where you can update the <see cref="M4Mizer.ViewModel"/> after the invokation of the method <see cref="RefreshBusinessObjects()"/>.
    /// This callback is invoked from a background thread, and not the UI thread.
    /// Never invoke this method, only the framework should, because this is a callback!
    /// </summary>
    Task RefreshViewModel();

    /// <summary>
    /// Invokes this method when you want to reload the business objects, from memory, local persistence, via web services, necessary for the entity processing.
    /// </summary>
    Task RefreshBusinessObjects();

    void ComputeOnBackPressed();

    void RemoveOnBackPressed();

  }

}
