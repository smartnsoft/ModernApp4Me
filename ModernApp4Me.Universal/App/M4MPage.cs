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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using ModernApp4Me.Core.ViewModel;
using ModernApp4Me.Universal.LifeCycle;

namespace ModernApp4Me.Universal.App
{

  /// <author>Ludovic ROLAND</author>
  /// <since>2015.04.01</since>
  /// <summary>
  /// The basis class for all Pages available in the framework.
  /// </summary>
  public abstract class M4MPage<SuspensionManagerClass> : Page, M4MLifeCycle, M4MLoadingAndErrorManager
    where SuspensionManagerClass : M4MSuspensionManager<SuspensionManagerClass>, new()
  {

    public M4MBaseViewModel ViewModel
    {
      get { return modern4mizer.ViewModel; }
      set { modern4mizer.ViewModel = value; }
    }

    public bool IsMVVMUsed
    {
      get { return modern4mizer.IsMVVMUsed; }
      set { modern4mizer.IsMVVMUsed = value; }
    }

    private readonly M4Mizer<SuspensionManagerClass> modern4mizer;

    protected M4MPage()
    {
      modern4mizer = new M4Mizer<SuspensionManagerClass>(this, this, this);
    }

    #region overring methods from Page
    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
      await modern4mizer.OnNavigatedTo(e);
      base.OnNavigatedTo(e);
    }

    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
      modern4mizer.OnNavigatedFrom();
    }
    #endregion

    #region abstract implementation of the life cycle interface
    public abstract void ComputeOnBackPressed();

    public abstract void RemoveOnBackPressed();
    #endregion

    #region default implementation of the life cycle interface
    public virtual async Task RefreshBusinessObjects()
    {
      await modern4mizer.RefreshBusinessObjects();
    }

    public virtual void LoadQueryString(object navigationParameter)
    {
    }

    public virtual Task<M4MBaseViewModel> ComputeViewModel()
    {
      return null;
    }

    public virtual Task RefreshViewModel()
    {
      return null;
    }

    public virtual void OnFulfillDisplayObjects()
    {
    }

    #endregion

    #region abstract implementation of the loading and error listener interface
    public virtual Panel RetrieveMainPanel()
    {
      return null;
    }

    public virtual Panel RetrieveErrorAndRetryPanel()
    {
      return null;
    }

    public virtual Panel RetrieveLoadingPanel()
    {
      return null;
    }

    public virtual void OnDisplayErrorAndRetryPanel(Exception exception)
    {
      RetrieveMainPanel().Visibility = Visibility.Collapsed;
      RetrieveLoadingPanel().Visibility = Visibility.Collapsed;
      RetrieveErrorAndRetryPanel().Visibility = Visibility.Visible;
    }
    #endregion

  }

}

