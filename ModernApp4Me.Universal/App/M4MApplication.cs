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

    public ResourceLoader Loader { get; set; }

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
    /// By default, this method call the method <see cref="SetupDefaultExceptionHandlers"/>
    /// </summary>
    /// <param name="rootFrame">the root <see cref="Frame"/></param>
    protected virtual void SetupApp(Frame rootFrame)
    {
      SetupDefaultExceptionHandlers(rootFrame);
    }

    private void SetupDefaultExceptionHandlers(Frame rootFrame)
    {
      // We let the overidding application register its exception handlers
      SetupExceptionHandlers(rootFrame);

      ExceptionHandlers = new M4MDefaultExceptionHandlers()
      {
        Frame = rootFrame,
        I18N = SetupM4Mi18N()
      };
    }

    /// <summary>
    /// This method will be invoked by the <see cref="SetupDefaultExceptionHandlers" /> method when building the <see cref="M4MDefaultExceptionHandlers" /> instance.
    /// This internationalization instance will be used to populate default dialog boxes texts popped-up by this default <see cref="M4MDefaultExceptionHandlers" />. 
    /// Hence, the method will be invoked at the application start-up.
    /// </summary>
    /// <returns>an instance which contains the internationalized text strings for some built-in error dialog boxes.</returns>
    public abstract M4Mi18N SetupM4Mi18N();

    /// <summary>
    /// This is the place where to register exception handlers like.
    /// The default implementation does nothing
    /// </summary>
    /// <param name="rootFrame"></param>
    public virtual void SetupExceptionHandlers(Frame rootFrame)
    {
    }

    private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
    {
      UnhandledExceptionsCustom(e.Exception);
      e.Handled = true;
    }

    private void UnhandledExceptions(object sender, UnhandledExceptionEventArgs e)
    {
      UnhandledExceptionsCustom(e.Exception);
      e.Handled = true;
    }

    /// <summary>
    /// Default entry point in order to manage errors
    /// </summary>
    /// <param name="exception"></param>
    protected void UnhandledExceptionsCustom(Exception exception)
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

      if (rootFrame.Content == null)
      {
        OnLaunchedCustom(e);

        if (!rootFrame.Navigate(GetMainPageType(), e.Arguments))
        {
          throw new Exception("Failed to create initial page");
        }
      }

      SetupApp(rootFrame);

      Window.Current.Activate();
    }

    /// <summary>
    /// Enables to custom a part on the <see cref="OnLaunched"/> method (for windows phone foe example).
    /// </summary>
    /// <param name="e">Details about the request and the process of the launch.</param>
    public void OnLaunchedCustom(LaunchActivatedEventArgs e)
    {
    }

    /// <summary>
    /// Returns the main page type in order to navigate when the app is launched
    /// </summary>
    /// <returns>the <see cref="Type"/> of the main page</returns>
    public abstract Type GetMainPageType();

    private async void OnSuspending(object sender, SuspendingEventArgs e)
    {
      var deferral = e.SuspendingOperation.GetDeferral();
      await M4MSuspensionManager<SuspensionManagerClass>.Instance.SaveAsync();
      deferral.Complete();
    }

  }

}
