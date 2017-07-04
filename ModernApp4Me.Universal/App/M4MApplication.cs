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
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ModernApp4Me.Core.App;
using ModernApp4Me.Universal.LifeCycle;
using Windows.ApplicationModel;
using Windows.UI.Xaml.Navigation;

namespace ModernApp4Me.Universal.App
{

  /// <author>Ludovic Roland</author>
  /// <since>2015.10.28</since>
  /// <summary>
  /// An abstract <see cref="Application"/> to be implemented when using the framework, because it initializes some of the components, and eases the development.
  /// </summary>
  public abstract class M4MApplication<SuspensionManagerClass> : Application
    where SuspensionManagerClass : M4MSuspensionManager<SuspensionManagerClass>, new()
  {

    public M4MExceptionHandlers ExceptionHandlers { get; set; }

    //public ResourceLoader Loader { get; set; }

    protected M4MApplication()
    {
      Suspending += OnSuspending;
      UnhandledException += UnhandledExceptions;
    }

    /// <summary>
    /// This method will be invoked at the end of the {@link #onCreate()} method, once the framework initialization is over. You can override this
    /// method, which does nothing by default, in order to initialize your application specific variables, invoke some services.
    ///  Keep in mind that this method should complete very quickly, in order to prevent from hanging the GUI thread, and thus causing a bad end-user
    /// experience
    /// By default, this method call the method <see cref="SetupLocalExceptionHandlers"/>
    /// </summary>
    /// <param name="rootFrame">the root <see cref="Frame"/></param>
    protected virtual void SetupApp(Frame rootFrame)
    {
      // We let the overidding application register its exception handlers
      SetupExceptionHandlers(rootFrame);
      ExceptionHandlers = SetupLocalExceptionHandlers(rootFrame);
    }

    /// <summary>
    /// This method will be invoked by the <see cref="SetupLocalExceptionHandlers" /> method when building the <see cref="M4MDefaultExceptionHandlers" /> instance.
    /// This internationalization instance will be used to populate default dialog boxes texts popped-up by this default <see cref="M4MDefaultExceptionHandlers" />. 
    /// Hence, the method will be invoked at the application start-up.
    /// </summary>
    /// <returns>an instance which contains the internationalized text strings for some built-in error dialog boxes.</returns>
    public abstract M4Mi18N SetupM4Mi18N();

    /// <summary>
    /// Returns the main page type in order to navigate when the app is launched
    /// </summary>
    /// <returns>the <see cref="Type"/> of the main page</returns>
    public abstract Type GetMainPageType();

    /// <summary>
    /// This is the place where to register exception handlers like HockeyApp, Capptain, etc.
    /// </summary>
    /// <param name="rootFrame"></param>
    public virtual void SetupExceptionHandlers(Frame rootFrame)
    {
    }

    /// <summary>
    /// This is the place where to register a local exception handlers.
    /// </summary>
    /// <param name="rootFrame"></param>
    /// <returns>an instance of <see cref="M4MExceptionHandlers"/> </returns>
    protected virtual M4MExceptionHandlers SetupLocalExceptionHandlers(Frame rootFrame)
    {
      return new M4MDefaultExceptionHandlers()
      {
        Frame = rootFrame,
        I18N = SetupM4Mi18N()
      };
    }

    /// <summary>
    /// Default implementation of a navigation exception.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
    {
      UnhandledExceptionsCustom(sender, e.Exception);
      e.Handled = true;
    }

    /// <summary>
    /// Default implementation of an unhandled exception.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void UnhandledExceptions(object sender, UnhandledExceptionEventArgs e)
    {
      UnhandledExceptionsCustom(sender, e.Exception);
      e.Handled = true;
    }

    /// <summary>
    /// Default entry point in order to manage errors
    /// </summary>
    /// <param name="exception"></param>
    protected virtual void UnhandledExceptionsCustom(object sender, Exception exception)
    {
      ExceptionHandlers.AnalyseException(exception);
    }

    /// <summary>
    /// Invokes when the app is launched normally by an end user.
    /// </summary>
    /// <param name="e">Details about the request and the process of the launch.</param>
    protected async override void OnLaunched(LaunchActivatedEventArgs e)
    {
      var rootFrame = Window.Current.Content as Frame;

      if (rootFrame == null)
      {
        rootFrame = new Frame {CacheSize = 2};
        rootFrame.NavigationFailed += OnNavigationFailed;

        M4MSuspensionManager<SuspensionManagerClass>.Instance.RegisterFrame(rootFrame, "AppFrame");

        if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
        {
          try
          {
            await M4MSuspensionManager<SuspensionManagerClass>.Instance.RestoreAsync();
          }
          catch (M4MSuspensionManagerException)
          {
            // Something went wrong restoring state.
            // Assume there is no state and continue
          }
        }

        Window.Current.Content = rootFrame;
      }

      SetupApp(rootFrame);

      if (rootFrame.Content == null)
      {
        OnLaunchedCustom(e);

        if (!rootFrame.Navigate(GetMainPageType(), e.Arguments))
        {
          throw new Exception("Failed to create initial page");
        }
      }

      Window.Current.Activate();
    }

    /// <summary>
    /// Enables to custom a part on the <see cref="OnLaunched"/> method (for windows phone foe example).
    /// </summary>
    /// <param name="e">Details about the request and the process of the launch.</param>
    public virtual void OnLaunchedCustom(LaunchActivatedEventArgs e)
    {
    }

    protected virtual async void OnSuspending(object sender, SuspendingEventArgs e)
    {
      var deferral = e.SuspendingOperation.GetDeferral();
      await M4MSuspensionManager<SuspensionManagerClass>.Instance.SaveAsync();
      deferral.Complete();
    }

  }

}
