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

namespace ModernApp4Me.Universal.LifeCycle
{

  /// <author>Ludovic Roland</author>
  /// <since>2015.04.02</since>
  /// <summary>
  /// NavigationHelper aids in navigation between pages.  It provides commands used to 
  /// navigate back and forward as well as registers for standard mouse and keyboard 
  /// shortcuts used to go back and forward in Windows and the hardware back button in
  /// Windows Phone.  In addition it integrates SuspensionManger to handle process lifetime
  /// management and state management when navigating between pages.
  /// </summary>
  /// <example>
  /// To make use of NavigationHelper, follow these two steps or
  /// start with a BasicPage or any other Page item template other than BlankPage.
  /// 
  /// 1) Create an instance of the NavigationHelper somewhere such as in the 
  ///     constructor for the page and register a callback for the LoadState and 
  ///     SaveState events.
  /// <code>
  ///     public MyPage()
  ///     {
  ///         this.InitializeComponent();
  ///         var navigationHelper = new NavigationHelper(this);
  ///         this.navigationHelper.LoadState += navigationHelper_LoadState;
  ///         this.navigationHelper.SaveState += navigationHelper_SaveState;
  ///     }
  ///     
  ///     private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
  ///     { }
  ///     private async void navigationHelper_SaveState(object sender, LoadStateEventArgs e)
  ///     { }
  /// </code>
  /// 
  /// 2) Register the page to call into the NavigationHelper whenever the page participates 
  ///     in navigation by overriding the <see cref="Windows.UI.Xaml.Controls.Page.OnNavigatedTo"/> 
  ///     and <see cref="Windows.UI.Xaml.Controls.Page.OnNavigatedFrom"/> events.
  /// <code>
  ///     protected override void OnNavigatedTo(NavigationEventArgs e)
  ///     {
  ///         navigationHelper.OnNavigatedTo(e);
  ///     }
  ///     
  ///     protected override void OnNavigatedFrom(NavigationEventArgs e)
  ///     {
  ///         navigationHelper.OnNavigatedFrom(e);
  ///     }
  /// </code>
  /// </example>
  // Taken from the HubPage template
  [Windows.Foundation.Metadata.WebHostHidden]
  public class M4MNavigationHelper : DependencyObject
  {

    /// <summary>
    /// Class used to hold the event data required when a page attempts to load state.
    /// </summary>
    public sealed class LoadStateEventArgs : EventArgs
    {

      /// <summary>
      /// The parameter value passed to <see cref="Frame.Navigate(Type, Object)"/> 
      /// when this page was initially requested.
      /// </summary>
      public Object NavigationParameter { get; private set; }

      /// <summary>
      /// A dictionary of state preserved by this page during an earlier
      /// session.  This will be null the first time a page is visited.
      /// </summary>
      public Dictionary<string, Object> PageState { get; private set; }

      /// <summary>
      /// Initializes a new instance of the <see cref="LoadStateEventArgs"/> class.
      /// </summary>
      /// <param name="navigationParameter">
      /// The parameter value passed to <see cref="Frame.Navigate(Type, Object)"/> 
      /// when this page was initially requested.
      /// </param>
      /// <param name="pageState">
      /// A dictionary of state preserved by this page during an earlier
      /// session.  This will be null the first time a page is visited.
      /// </param>
      public LoadStateEventArgs(Object navigationParameter, Dictionary<string, Object> pageState)
        : base()
      {
        NavigationParameter = navigationParameter;
        PageState = pageState;
      }
    }

    /// <summary>
    /// Class used to hold the event data required when a page attempts to save state.
    /// </summary>
    public sealed class SaveStateEventArgs : EventArgs
    {

      /// <summary>
      /// An empty dictionary to be populated with serializable state.
      /// </summary>
      public Dictionary<string, Object> PageState { get; private set; }

      /// <summary>
      /// Initializes a new instance of the <see cref="SaveStateEventArgs"/> class.
      /// </summary>
      /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
      public SaveStateEventArgs(Dictionary<string, Object> pageState)
        : base()
      {
        PageState = pageState;
      }
    }

    private Page Page { get; set; }

    private M4MLifeCycle LifeCycle { get; set; }

    private Frame Frame { get { return Page.Frame; } }

    /// <summary>
    /// Initializes a new instance of the <see cref="M4MNavigationHelper"/> class.
    /// </summary>
    /// <param name="lifeCycle">A reference to the <see cref="M4MLifeCycle"/> used for navigation.</param>  
    /// <param name="page">A reference to the current <see cref="Page"/> used for navigation.
    /// This reference allows for frame manipulation and to ensure that keyboard 
    /// navigation requests only occur when the page is occupying the entire window.</param>
    public M4MNavigationHelper(M4MLifeCycle lifeCycle, Page page)
    {
      Page = page;
      LifeCycle = lifeCycle;

      // When this page is part of the visual tree make two changes:
      // 1) Map application view state to visual state for the page
      // 2) Handle hardware navigation requests
      Page.Loaded += (sender, e) =>
      {
        LifeCycle.ComputeOnBackPressed();
      };

      // Undo the same changes when the page is no longer visible
      Page.Unloaded += (sender, e) =>
      {
        LifeCycle.RemoveOnBackPressed();
      };
    }

    #region Navigation support

    M4MRelayCommand goBackCommand;

    M4MRelayCommand goForwardCommand;

    /// <summary>
    /// <see cref="M4MRelayCommand"/> used to bind to the back Button's Command property
    /// for navigating to the most recent item in back navigation history, if a Frame
    /// manages its own navigation history.
    /// 
    /// The <see cref="M4MRelayCommand"/> is set up to use the virtual method <see cref="GoBack"/>
    /// as the Execute Action and <see cref="CanGoBack"/> for CanExecute.
    /// </summary>
    public M4MRelayCommand GoBackCommand
    {
      get
      {
        if (goBackCommand == null)
        {
          goBackCommand = new M4MRelayCommand(
            () => GoBack(),
            () => CanGoBack());
        }

        return goBackCommand;
      }

      set
      {
        goBackCommand = value;
      }
    }

    /// <summary>
    /// <see cref="M4MRelayCommand"/> used for navigating to the most recent item in 
    /// the forward navigation history, if a Frame manages its own navigation history.
    /// 
    /// The <see cref="M4MRelayCommand"/> is set up to use the virtual method <see cref="GoForward"/>
    /// as the Execute Action and <see cref="CanGoForward"/> for CanExecute.
    /// </summary>
    public M4MRelayCommand GoForwardCommand
    {
      get
      {
        if (goForwardCommand == null)
        {
          goForwardCommand = new M4MRelayCommand(
            () => GoForward(),
            () => CanGoForward());
        }

        return goForwardCommand;
      }
    }

    /// <summary>
    /// Virtual method used by the <see cref="GoBackCommand"/> property
    /// to determine if the <see cref="Frame"/> can go back.
    /// </summary>
    /// <returns>
    /// true if the <see cref="Frame"/> has at least one entry 
    /// in the back navigation history.
    /// </returns>
    public virtual bool CanGoBack()
    {
      return Frame != null && Frame.CanGoBack;
    }

    /// <summary>
    /// Virtual method used by the <see cref="GoForwardCommand"/> property
    /// to determine if the <see cref="Frame"/> can go forward.
    /// </summary>
    /// <returns>
    /// true if the <see cref="Frame"/> has at least one entry 
    /// in the forward navigation history.
    /// </returns>
    public virtual bool CanGoForward()
    {
      return Frame != null && Frame.CanGoForward;
    }

    /// <summary>
    /// Virtual method used by the <see cref="GoBackCommand"/> property
    /// to invoke the <see cref="Windows.UI.Xaml.Controls.Frame.GoBack"/> method.
    /// </summary>
    public virtual void GoBack()
    {
      if (Frame != null && Frame.CanGoBack)
      {
        Frame.GoBack();
      }
    }

    /// <summary>
    /// Virtual method used by the <see cref="GoForwardCommand"/> property
    /// to invoke the <see cref="Windows.UI.Xaml.Controls.Frame.GoForward"/> method.
    /// </summary>
    public virtual void GoForward()
    {
      if (Frame != null && Frame.CanGoForward)
      {
        Frame.GoForward();
      }
    }

    #endregion

    #region Process lifetime management

    public String PageKey { get; set; }

    #endregion
  }

}
