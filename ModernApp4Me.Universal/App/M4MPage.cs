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
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using ModernApp4Me.Core.LifeCycle;
using ModernApp4Me.Core.ViewModel;
using ModernApp4Me.Universal.LifeCycle;

namespace ModernApp4Me.Universal.App
{

  /// <author>Ludovic ROLAND</author>
  /// <since>2015.04.01</since>
  /// <summary>
  /// The basis class for all Pages available in the framework.
  /// </summary>
  public abstract class M4MPage : Page
  {

    private const string PAGE_KEY_SUFFIX = "Page-";

    private const string VIEW_MODEL_KEY = "viewModelKey";

    protected readonly M4MNavigationHelper navigationHelper;

    protected M4MBaseViewModel viewModel;

    protected ProgressRing progressRing;

    protected bool isMVVMUsed = true;

    protected Panel mainContener;

    protected Panel connectivityContainer;

    protected M4MPage()
    {
      navigationHelper = new M4MNavigationHelper(this);
    }

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
      progressRing = RetrieveProgressRing();
      mainContener = RetrieveMainContainer();
      connectivityContainer = RetrieveConnectivityContainer();

      mainContener.Visibility = Visibility.Collapsed;

      if (connectivityContainer != null)
      {
        connectivityContainer.Visibility = Visibility.Collapsed;
      }

      if (progressRing != null)
      {
        progressRing.IsActive = true;
      }

      SendPage();

      LoadQueryString(e.Parameter);

      try
      {
        var frameState = M4MSuspensionManager.SessionStateForFrame(Frame);
        navigationHelper.PageKey = M4MPage.PAGE_KEY_SUFFIX + Frame.BackStackDepth;

        if (e.NavigationMode == NavigationMode.New)
        {
          // Clear existing state for forward navigation when adding a new page to the
          // navigation stack
          var nextPageKey = navigationHelper.PageKey;
          var nextPageIndex = Frame.BackStackDepth;

          while (frameState.Remove(nextPageKey))
          {
            nextPageIndex++;
            nextPageKey = M4MPage.PAGE_KEY_SUFFIX + nextPageIndex;
          }

          if (isMVVMUsed == true)
          {
            viewModel = await ComputeViewModel();
          }
        }
        else
        {
          // Pass the navigation parameter and preserved page state to the page, using
          // the same strategy for loading suspended state and recreating pages discarded
          // from cache
          viewModel =
            ((Dictionary<String, Object>) frameState[navigationHelper.PageKey])[M4MPage.VIEW_MODEL_KEY] as
              M4MBaseViewModel;
        }
      }
      catch (M4MConnectivityException exception)
      {
        OnDisplayConnectivityLayout();
        throw new M4MConnectivityException(exception);
      }

      OnFullfillDisplayObjects();

      if (connectivityContainer != null)
      {
        connectivityContainer.Visibility = Visibility.Collapsed;
      }

      mainContener.Visibility = Visibility.Visible;

      if (progressRing != null)
      {
        progressRing.IsActive = false;
      }

      base.OnNavigatedTo(e);
    }

    /// <summary>
    /// Override this in order to send the PhoneApplicationPage when it is displayed.
    /// </summary>
    protected virtual void SendPage()
    {
    }

    /// <summary>
    /// Override this in order to personnalize the user experience when a <see cref="M4MConnectivityException"/> occurs.
    /// </summary>
    protected virtual void OnDisplayConnectivityLayout()
    {
      mainContener.Visibility = Visibility.Collapsed;

      if (progressRing != null)
      {
        progressRing.IsActive = false;
      }

      if (connectivityContainer != null)
      {
        connectivityContainer.Visibility = Visibility.Visible;
      }
    }

    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
      var frameState = M4MSuspensionManager.SessionStateForFrame(Frame);
      var pageState = new Dictionary<String, Object> {{M4MPage.VIEW_MODEL_KEY, viewModel}};
      frameState[navigationHelper.PageKey] = pageState;
    }

    public abstract void ComputeOnBackPressed();

    public abstract void RemoveOnBackPressed();

    /// <summary>
    /// This is the place where to load the business objects, from memory, local persistence, via web services, necessary for the entity processing.
    /// This callback is invoked from a background thread, and not the UI thread.
    /// This method is invoked only once during the <see cref="M4MPage"/> life cycle.
    /// Never invoke this method, only the framework should, because this is a callback!
    /// </summary>
    /// <returns>a class that implements <see cref="M4MBaseViewModel"/> or null if you set <see cref="M4MPage.isMVVMUsed"/> to false</returns>
    protected abstract Task<M4MBaseViewModel> ComputeViewModel();

    /// <summary>
    /// This is the place where the implementing class can initialize the previously retrieved graphical objects.
    /// This method is invoked only once during the <see cref="M4MPage"/> life cycle.
    /// It is ensured that this method will be invoked from the UI thread!
    /// Never invoke this method, only the framework should, because this is a callback!
    /// </summary>
    protected abstract void OnFullfillDisplayObjects();

    /// <summary>
    /// Returns the <see cref="Panel"/> in which data are displayed.
    /// This <see cref="Panel"/> will be displayed or hidden automatically by the <see cref="M4MPage"/> during the life cycle.
    /// </summary>
    /// <returns>the <see cref="Panel"/> in which data are displayed</returns>
    protected abstract Panel RetrieveMainContainer();

    /// <summary>
    /// Override this in order to return the <see cref="Panel"/> which displays a particular message when a <see cref="M4MConnectivityException"/> is thrown.
    /// This <see cref="Panel"/> will be displayed or hidden automatically by the <see cref="M4MPage"/> during the life cycle.
    /// </summary>
    /// <returns>the <see cref="Panel"/> which display the message or null</returns>
    protected virtual Panel RetrieveConnectivityContainer()
    {
      return null;
    }

    /// <summary>
    /// Returns the <see cref="ProgressRing"/> of the <see cref="M4MPage"/>.
    /// </summary>
    /// <returns>a <see cref="ProgressRing"/> or null</returns>
    protected virtual ProgressRing RetrieveProgressRing()
    {
      return null;
    }

    /// <summary>
    /// This is the place where to retrieve data from the <see cref="NavigationEventArgs.Parameter"/>.
    /// This method is invoked before the <see cref="M4MPage.ComputeViewModel()"/>.
    /// </summary>
    /// <param name="navigationParameter">The parameter value passed to <see cref="Frame.Navigate(Type, Object)"/> </param>
    protected virtual void LoadQueryString(Object navigationParameter)
    {
    }

    /// <summary>
    /// This is the place where you can update the <see cref="M4MPage.viewModel"/> after the invokation of the method <see cref="M4MPage.Refresh()"/>.
    /// This callback is invoked from a background thread, and not the UI thread.
    /// Never invoke this method, only the framework should, because this is a callback!
    /// </summary>
    protected virtual Task RefreshViewModel()
    {
      return null;
    }

    /// <summary>
    /// Invokes this method when you want to reload the business objects, from memory, local persistence, via web services, necessary for the entity processing.
    /// </summary>
    /// <returns></returns>
    protected async Task Refresh()
    {
      mainContener.Visibility = Visibility.Collapsed;

      if (connectivityContainer != null)
      {
        connectivityContainer.Visibility = Visibility.Collapsed;
      }

      if (progressRing != null)
      {
        progressRing.IsActive = true;
      }

      if (viewModel == null)
      {
        try
        {
          viewModel = await ComputeViewModel();
        }
        catch (M4MConnectivityException exception)
        {
          OnDisplayConnectivityLayout();
          throw new M4MConnectivityException(exception);
        }

        OnFullfillDisplayObjects();

        if (connectivityContainer != null)
        {
          connectivityContainer.Visibility = Visibility.Collapsed;
        }

        mainContener.Visibility = Visibility.Visible;

        if (progressRing != null)
        {
          progressRing.IsActive = false;
        }
      }
      else
      {
        try
        {
          await RefreshViewModel();
        }
        catch (M4MConnectivityException exception)
        {
          if (progressRing != null)
          {
            progressRing.IsActive = false;
          }

          throw new M4MConnectivityException(exception);
        }
      }

      if (progressRing != null)
      {
        progressRing.IsActive = false;
      }
    }

  }

}

