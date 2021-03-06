﻿// The MIT License (MIT)
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

using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Shell;
using ModernApp4Me.Core.ViewModel;
using Microsoft.Phone.Controls;
using ModernApp4Me.Core.LifeCycle;

namespace ModernApp4Me.WP8.App
{

    /// <summary>
    /// The basis class for all PhoneApplicationPages available in the framework.
    /// </summary>
    /// 
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.09.08</since>
    public abstract class M4MPhoneApplicationPage : PhoneApplicationPage
    {

        private const string VIEW_MODEL_KEY = "viewModelKey";

        protected M4MBaseViewModel viewModel;

        protected ProgressIndicator progressIndicator;

        protected bool isMVVMUsed = true;

        protected bool displayLoaderPanelNextTime = true;

        protected bool isManagingProgressIndicatorItself = false;

        protected Panel mainContener;

        protected Panel connectivityContainer;

        protected Panel loadingPanel;

        //The State should be called before any await key word to avoid the system.InvalidOperationException: You can only use State between OnNavigatedTo and OnNavigatedFrom Exception
        //http://blog.ariankulp.com/2014/04/you-can-only-use-state-between.html
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            progressIndicator = RetrieveProgressIndicator();
            loadingPanel = RetrieveLoadingContainer();

            if (isManagingProgressIndicatorItself == false)
            {
                progressIndicator.IsVisible = true;

                if(loadingPanel != null && displayLoaderPanelNextTime == true)
                {
                  loadingPanel.Visibility = Visibility.Visible;
                }
            }

            SendPage();

            mainContener = RetrieveMainContainer();
            connectivityContainer = RetrieveConnectivityContainer();

            LoadQueryString();

            try
            {
                if (viewModel == null && isMVVMUsed == true)
                {
                    if (State.ContainsKey(M4MPhoneApplicationPage.VIEW_MODEL_KEY) == true)
                    {
                        viewModel = State[M4MPhoneApplicationPage.VIEW_MODEL_KEY] as M4MBaseViewModel;
                    }
                    else
                    {
                        viewModel = await ComputeViewModel();
                    }
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

            if (mainContener != null)
            {
                mainContener.Visibility = Visibility.Visible;
            }

            if (isManagingProgressIndicatorItself == false)
            {
                progressIndicator.IsVisible = false;

                if(loadingPanel != null)
                {
                  loadingPanel.Visibility = Visibility.Collapsed;
                }
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
            if (isManagingProgressIndicatorItself == false)
            {
                progressIndicator.IsVisible = false;

                if(loadingPanel != null)
                {
                  loadingPanel.Visibility = Visibility.Collapsed;
                }
            }

            if (connectivityContainer != null)
            {
                connectivityContainer.Visibility = Visibility.Visible;
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if (e.NavigationMode != NavigationMode.Back && viewModel != null)
            {
                State[M4MPhoneApplicationPage.VIEW_MODEL_KEY] = viewModel;
            }
        }

        /// <summary>
        /// This is the place where to load the business objects, from memory, local persistence, via web services, necessary for the entity processing.
        /// This callback is invoked from a background thread, and not the UI thread.
        /// This method is invoked only once during the <see cref="M4MPhoneApplicationPage"/> life cycle.
        /// Never invoke this method, only the framework should, because this is a callback!
        /// </summary>
        /// <returns>a class that implements <see cref="M4MBaseViewModel"/> or null if you set <see cref="M4MPhoneApplicationPage.IsMVVMUsed"/> to false</returns>
        protected abstract Task<M4MBaseViewModel> ComputeViewModel();

        /// <summary>
        /// This is the place where the implementing class can initialize the previously retrieved graphical objects.
        /// This method is invoked only once during the <see cref="M4MPhoneApplicationPage"/> life cycle.
        /// It is ensured that this method will be invoked from the UI thread!
        /// Never invoke this method, only the framework should, because this is a callback!
        /// </summary>
        protected abstract void OnFullfillDisplayObjects();

        /// <summary>
        /// Returns the <see cref="Panel"/> in which data are displayed.
        /// This <see cref="Panel"/> will be displayed or hidden automatically by the <see cref="M4MPhoneApplicationPage"/> during the life cycle.
        /// </summary>
        /// <returns>the <see cref="Panel"/> in which data are displayed</returns>
        protected abstract Panel RetrieveMainContainer();

        /// <summary>
        /// Override this in order to return the <see cref="Panel"/> which displays a particular message when a <see cref="M4MConnectivityException"/> is thrown.
        /// This <see cref="Panel"/> will be displayed or hidden automatically by the <see cref="M4MPhoneApplicationPage"/> during the life cycle.
        /// </summary>
        /// <returns>the <see cref="Panel"/> which display the message or null</returns>
        protected virtual Panel RetrieveConnectivityContainer()
        {
            return null;
        }

        /// <summary>
        /// Override this in order to return the <see cref="Panel"/> which displays a particular message when the app is loading.
        /// This <see cref="Panel"/> will be displayed or hidden automatically by the <see cref="M4MPhoneApplicationPage"/> during the life cycle.
        /// </summary>
        /// <returns>the <see cref="Panel"/> which display the message or null</returns>
        protected virtual Panel RetrieveLoadingContainer()
        {
          return null;
        }

        /// <summary>
        /// Returns the <see cref="ProgressIndicator"/> of the <see cref="M4MPhoneApplicationPage"/>.
        /// </summary>
        /// <returns>a <see cref="ProgressIndicator"/> or null if you set <see cref="M4MPhoneApplicationPage.IsManagingProgressIndicatorItself" /> to true</returns>
        protected abstract ProgressIndicator RetrieveProgressIndicator();

        /// <summary>
        /// This is the place where to retrieve data from the <see cref="NavigationContext.QueryString"/>.
        /// This method is invoked before the <see cref="M4MPhoneApplicationPage.ComputeViewModel()"/>.
        /// </summary>
        protected virtual void LoadQueryString()
        {
        }

        /// <summary>
        /// This is the place where you can update the <see cref="M4MPhoneApplicationPage.viewModel"/> after the invokation of the method <see cref="M4MPhoneApplicationPage.Refresh()"/>.
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
            if (isManagingProgressIndicatorItself == false)
            {
                progressIndicator.IsVisible = true;

                if(loadingPanel != null && displayLoaderPanelNextTime == true)
                {
                  if (connectivityContainer != null)
                  {
                    connectivityContainer.Visibility = Visibility.Collapsed;
                  }

                  mainContener.Visibility = Visibility.Collapsed;
                  loadingPanel.Visibility = Visibility.Visible;
                }
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

               if (isManagingProgressIndicatorItself == false)
                {
                    progressIndicator.IsVisible = false;

                    if(loadingPanel != null)
                    {
                      loadingPanel.Visibility = Visibility.Collapsed;
                    }
                }

                if (mainContener != null)
                {
                    mainContener.Visibility = Visibility.Visible;
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
                    if (isManagingProgressIndicatorItself == false)
                    {
                        progressIndicator.IsVisible = false;

                        if(displayLoaderPanelNextTime == true)
                        {
                          OnDisplayConnectivityLayout();
                        }
                    }


                    throw new M4MConnectivityException(exception);
                }

            }

            if (isManagingProgressIndicatorItself == false)
            {
                progressIndicator.IsVisible = false;

                if (loadingPanel != null)
                {
                  loadingPanel.Visibility = Visibility.Collapsed;
                }
            }

            if (mainContener != null)
            {
              mainContener.Visibility = Visibility.Visible;
            }
        }

    }

}
