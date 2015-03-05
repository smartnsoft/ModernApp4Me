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
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ModernApp4Me.Core.App;
using ModernApp4Me.Core.Log;
using ModernApp4Me.WP8.App;
using ModernApp4Me.WP8.Sample.Log;
using ModernApp4Me.WP8.Sample.Resources;

namespace ModernApp4Me.WP8.Sample
{

    /// <author>Ludovic ROLAND</author>
    /// <since>2015.03.05</since>
    public partial class App
    {
        
        public static PhoneApplicationFrame RootFrame { get; private set; }

        public static M4MExceptionHandlers ExceptionHandlers { get; private set; }

        public App()
        {
            UnhandledException += Application_UnhandledException;

            InitializeComponent();
            InitializePhoneApplication();
            InitializeLanguage();

            if (Debugger.IsAttached)
            {
                Current.Host.Settings.EnableFrameRateCounter = true;
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }
        }

        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
            //We setup the exception handler
            ExceptionHandlers = new M4MDefaultExceptionHandlers()
            {
                RootFrame = RootFrame,
                I18N = new M4Mi18N()
                {
                    DialogBoxErrorTitle = AppResources.Problem,
                    InhandledProblemHint = AppResources.UnhandledProblem,
                    ConnectivityProblemHint = AppResources.ConnectivityProblem,
                    BusinessObjectUnavailableHint = AppResources.BusinessObjectProblem
                    
                }
            };

            //We setup the logger
            Logger.Instance.LogLevel = M4MLogger.M4MLogLevel.Debug;
        }

        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
        }

        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
        }

        private void Application_Closing(object sender, ClosingEventArgs e)
        {
        }

        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            //We handle the exception and invoke the UnhandledExceptionCustom method
            UnhandledExceptionCustom(e.Exception);
            e.Handled = true;
        }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            //We handle the exception and invoke the UnhandledExceptionCustom method
            UnhandledExceptionCustom(e.ExceptionObject);
            e.Handled = true;
        }

        private void UnhandledExceptionCustom(Exception exception)
        {
            ExceptionHandlers.AnalyseException(exception);
        }



        #region Initialisation de l'application téléphonique

        private bool phoneApplicationInitialized = false;

        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
            {
                return;
            }

            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;
            RootFrame.Navigated += CheckForResetNavigation;
            phoneApplicationInitialized = true;
        }

        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            if (RootVisual != RootFrame)
            {
                RootVisual = RootFrame;
            }

            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        private void CheckForResetNavigation(object sender, NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Reset)
            {
                RootFrame.Navigated += ClearBackStackAfterReset;
            }
        }

        private void ClearBackStackAfterReset(object sender, NavigationEventArgs e)
        {
            RootFrame.Navigated -= ClearBackStackAfterReset;

            if (e.NavigationMode != NavigationMode.New && e.NavigationMode != NavigationMode.Refresh)
            {
                return;
            }

            while (RootFrame.RemoveBackEntry() != null)
            {
                ;
            }
        }

        #endregion

        private void InitializeLanguage()
        {
            try
            {
                RootFrame.Language = XmlLanguage.GetLanguage(AppResources.ResourceLanguage);
                FlowDirection flow = (FlowDirection)Enum.Parse(typeof(FlowDirection), AppResources.ResourceFlowDirection);
                RootFrame.FlowDirection = flow;
            }
            catch
            {
                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }

                throw;
            }
        }
    }
}