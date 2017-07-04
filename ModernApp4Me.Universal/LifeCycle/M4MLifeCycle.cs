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
using System.Threading.Tasks;
using ModernApp4Me.Core.ViewModel;

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
