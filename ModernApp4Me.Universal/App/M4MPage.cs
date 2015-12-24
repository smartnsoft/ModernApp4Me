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

