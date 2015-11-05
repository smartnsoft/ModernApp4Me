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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using ModernApp4Me.Core.LifeCycle;
using ModernApp4Me.Core.ViewModel;
using ModernApp4Me.Universal.LifeCycle;
using System.Threading.Tasks;

namespace ModernApp4Me.Universal.App
{

  /// <author>Ludovic ROLAND</author>
  /// <since>2015.10.28</since>
  /// <summary>
  /// The class that should be used when extending a legacy class to support the whole Modern4Me framework features.
  /// </summary>
  public sealed class M4Mizer<SuspensionManagerClass>
    where SuspensionManagerClass : M4MSuspensionManager<SuspensionManagerClass>, new()
  {

    private const string PAGE_KEY_SUFFIX = "Page-";

    private const string VIEW_MODEL_KEY = "viewModelKey";

    public M4MBaseViewModel ViewModel { get; set; }

    public bool IsMVVMUsed { get; set; }

    private readonly M4MNavigationHelper navigationHelper;

    private readonly M4MLifeCycle lifeCycle;

    private readonly Page page;

    private ProgressRing progressRing;

    private Panel mainPanel;

    private Panel connectivityPanel;

    public M4Mizer(Page page, M4MLifeCycle lifeCycle)
    {
      IsMVVMUsed = true;
      navigationHelper = new M4MNavigationHelper(lifeCycle, page);
      this.page = page;
      this.lifeCycle = lifeCycle;
    }

    public async Task OnNavigatedTo(NavigationEventArgs e)
    {
      progressRing = lifeCycle.RetrieveProgressRing();
      mainPanel = lifeCycle.RetrieveMainContainer();
      connectivityPanel = lifeCycle.RetrieveConnectivityContainer();

      if (mainPanel != null)
      {
        mainPanel.Visibility = Visibility.Collapsed;
      }

      if (connectivityPanel != null)
      {
        connectivityPanel.Visibility = Visibility.Collapsed;
      }

      if (progressRing != null)
      {
        progressRing.IsActive = true;
      }

      lifeCycle.LoadQueryString(e.Parameter);

      try
      {
        var frameState = M4MSuspensionManager<SuspensionManagerClass>.Instance.SessionStateForFrame(page.Frame);
        navigationHelper.PageKey = PAGE_KEY_SUFFIX + page.Frame.BackStackDepth;

        if (e.NavigationMode == NavigationMode.New)
        {
          // Clear existing state for forward navigation when adding a new page to the
          // navigation stack
          var nextPageKey = navigationHelper.PageKey;
          var nextPageIndex = page.Frame.BackStackDepth;

          while (frameState.Remove(nextPageKey) == true)
          {
            nextPageIndex++;
            nextPageKey = PAGE_KEY_SUFFIX + nextPageIndex;
          }

          if (IsMVVMUsed == true)
          {
            ViewModel = await lifeCycle.ComputeViewModel();
          }
        }
        else
        {
          // Pass the navigation parameter and preserved page state to the page, using
          // the same strategy for loading suspended state and recreating pages discarded
          // from cache
          ViewModel =
            ((Dictionary<String, Object>)frameState[navigationHelper.PageKey])[VIEW_MODEL_KEY] as
              M4MBaseViewModel;
        }
      }
      catch (M4MConnectivityException exception)
      {
        lifeCycle.OnDisplayConnectivityLayout();
        throw new M4MConnectivityException(exception);
      }

      lifeCycle.OnFulfillDisplayObjects();

      if (connectivityPanel != null)
      {
        connectivityPanel.Visibility = Visibility.Collapsed;
      }

      if (mainPanel != null)
      {
        mainPanel.Visibility = Visibility.Visible;
      }

      if (progressRing != null)
      {
        progressRing.IsActive = false;
      }
    }

    public void OnDisplayConnectivityLayout()
    {
      if (mainPanel != null)
      {
        mainPanel.Visibility = Visibility.Collapsed;
      }

      if (progressRing != null)
      {
        progressRing.IsActive = false;
      }

      if (connectivityPanel != null)
      {
        connectivityPanel.Visibility = Visibility.Visible;
      }
    }

    public void OnNavigatedFrom()
    {
      var frameState = M4MSuspensionManager<SuspensionManagerClass>.Instance.SessionStateForFrame(page.Frame);
      var pageState = new Dictionary<String, Object> { { VIEW_MODEL_KEY, ViewModel } };
      frameState[navigationHelper.PageKey] = pageState;
    }

    public async Task RefreshBusinessObjects()
    {
      if (mainPanel != null)
      {
        mainPanel.Visibility = Visibility.Collapsed;
      }

      if (connectivityPanel != null)
      {
        connectivityPanel.Visibility = Visibility.Collapsed;
      }

      if (progressRing != null)
      {
        progressRing.IsActive = true;
      }

      if (ViewModel == null)
      {
        try
        {
          ViewModel = await lifeCycle.ComputeViewModel();
        }
        catch (M4MConnectivityException exception)
        {
          lifeCycle.OnDisplayConnectivityLayout();
          throw new M4MConnectivityException(exception);
        }

        lifeCycle.OnFulfillDisplayObjects();

        if (connectivityPanel != null)
        {
          connectivityPanel.Visibility = Visibility.Collapsed;
        }

        if (mainPanel != null)
        {
          mainPanel.Visibility = Visibility.Visible;
        }

        if (progressRing != null)
        {
          progressRing.IsActive = false;
        }
      }
      else
      {
        try
        {
          await lifeCycle.RefreshViewModel();
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
