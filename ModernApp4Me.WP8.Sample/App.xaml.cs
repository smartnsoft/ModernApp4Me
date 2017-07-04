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
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ModernApp4Me.Core.App;
using ModernApp4Me.Core.Log;
using ModernApp4Me.WP8.App;
using ModernApp4Me.WP8.Debug;
using ModernApp4Me.WP8.Download;
using ModernApp4Me.WP8.Sample.Log;
using ModernApp4Me.WP8.Sample.Resources;

namespace ModernApp4Me.WP8.Sample
{

    /// <author>Ludovic ROLAND</author>
    /// <since>2015.03.05</since>
    public partial class App
    {

        public sealed class MemoryProfilerCustom : M4MMemoryProfiler
        {

            protected override void WriteReport(string report)
            {
                if (Logger.Instance.IsDebugEnabled() == true)
                {
                    Logger.Instance.Debug(report);
                }
            }
        }
        
        public static PhoneApplicationFrame RootFrame { get; private set; }

        public M4MExceptionHandlers ExceptionHandlers { get; private set; }
        
        public M4MMemoryProfiler MemoryProfiler { get; private set; }

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
            SetupApp();
        }

        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
            //We restore the application objects
            if (e.IsApplicationInstancePreserved == false)
            {
                SetupApp();
            }
        }

        private void SetupApp()
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

            //We setup and launch the memory profiler
            MemoryProfiler = new MemoryProfilerCustom();
            MemoryProfiler.BeginRecording();

            //We setup the bitmap downloader
            M4MBitmapDownloader.Configuration = new M4MBitmapDownloader.BitmapDownloaderConfiguration()
            {
                ExpirationDelay = TimeSpan.FromDays(1),
                MemoryCacheCapacity = 50,
                Name = "BitmapDownloader",
                Type = M4MBitmapDownloader.BitmapDownloaderType.PersistentImageCache
            };
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
                var flow = (FlowDirection)Enum.Parse(typeof(FlowDirection), AppResources.ResourceFlowDirection);
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