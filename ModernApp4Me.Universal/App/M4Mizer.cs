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
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
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

    private readonly M4MLoadingAndErrorManager loadingAndErrorManager;

    private readonly Page page;

    private Panel mainPanel;
    
    private Panel loadingPanel;

    private Panel errorAndRetryPanel;

    public M4Mizer(Page page, M4MLifeCycle lifeCycle, M4MLoadingAndErrorManager loadingAndErrorManager)
    {
      if (lifeCycle == null)
      {
        throw new ArgumentException("The M4MLifeCycle interface cannot be null");
      }

      if (loadingAndErrorManager == null)
      {
        throw new ArgumentException("The M4MLoadingAndErrorListener interface cannot be null");
      }

      IsMVVMUsed = true;
      navigationHelper = new M4MNavigationHelper(lifeCycle, page);
      this.page = page;
      this.lifeCycle = lifeCycle;
      this.loadingAndErrorManager = loadingAndErrorManager;
    }

    public async Task OnNavigatedTo(NavigationEventArgs e)
    {
      loadingPanel = loadingAndErrorManager.RetrieveLoadingPanel();
      errorAndRetryPanel = loadingAndErrorManager.RetrieveErrorAndRetryPanel();
      mainPanel = loadingAndErrorManager.RetrieveMainPanel();

      DisplayLoadingPanel();

      lifeCycle.LoadQueryString(e.Parameter);

      var frameState = M4MSuspensionManager<SuspensionManagerClass>.Instance.SessionStateForFrame(page.Frame);
      navigationHelper.PageKey = M4Mizer<SuspensionManagerClass>.PAGE_KEY_SUFFIX + page.Frame.BackStackDepth;

      if (e.NavigationMode == NavigationMode.New)
      {
        // Clear existing state for forward navigation when adding a new page to the
        // navigation stack
        var nextPageKey = navigationHelper.PageKey;
        var nextPageIndex = page.Frame.BackStackDepth;

        while (frameState.Remove(nextPageKey) == true)
        {
          nextPageIndex++;
          nextPageKey = M4Mizer<SuspensionManagerClass>.PAGE_KEY_SUFFIX + nextPageIndex;
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
          ((Dictionary<String, Object>) frameState[navigationHelper.PageKey])[
            M4Mizer<SuspensionManagerClass>.VIEW_MODEL_KEY] as M4MBaseViewModel;
      }

      lifeCycle.OnFulfillDisplayObjects();

      DisplayMainPanel();
    }

    private void DisplayMainPanel()
    {
      if (loadingPanel != null)
      {
        loadingPanel.Visibility = Visibility.Collapsed;
      }

      if (errorAndRetryPanel != null)
      {
        errorAndRetryPanel.Visibility = Visibility.Collapsed;
      }

      if (mainPanel != null)
      {
        mainPanel.Visibility = Visibility.Visible;
      }
    }

    private void DisplayLoadingPanel()
    {
      if (mainPanel != null)
      {
        mainPanel.Visibility = Visibility.Collapsed;
      }

      if (errorAndRetryPanel != null)
      {
        errorAndRetryPanel.Visibility = Visibility.Collapsed;
      }

      if (loadingPanel != null)
      {
        loadingPanel.Visibility = Visibility.Visible;
      }
    }

    public void OnNavigatedFrom()
    {
      var frameState = M4MSuspensionManager<SuspensionManagerClass>.Instance.SessionStateForFrame(page.Frame);
      var pageState = new Dictionary<String, Object> { { M4Mizer<SuspensionManagerClass>.VIEW_MODEL_KEY, ViewModel } };
      frameState[navigationHelper.PageKey] = pageState;
    }

    public async Task RefreshBusinessObjects()
    {
      DisplayLoadingPanel();

      if (ViewModel == null)
      {
        ViewModel = await lifeCycle.ComputeViewModel();
        lifeCycle.OnFulfillDisplayObjects();
      }
      else
      {
        await lifeCycle.RefreshViewModel();
      }

      DisplayMainPanel();
    }

  }

}
