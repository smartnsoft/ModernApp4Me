using System;
using Microsoft.Phone.Controls;
using ModernApp4Me.Core.SnSApp;
using ModernApp4Me.Core.SnSLog;
using ModernApp4Me.WP8.SnSApp;
using ModernApp4Me.WP8.Template.Resources;

namespace ModernApp4Me.WP8.Template
{

    public partial class App
    {

        protected override string GetLanguage()
        {
            return AppResources.ResourceLanguage;
        }

        protected override string GetResourceFlowDirection()
        {
            return AppResources.ResourceFlowDirection;
        }

        protected override PhoneApplicationFrame InitializeRootFrame()
        {
            return new PhoneApplicationFrame();
        }

        protected override void SetupAnalytics()
        {
            //TODO : init the analytics here.
        }

        protected override SnSExceptionHandler SetupExceptionHandlers()
        {
            //TODO : init bugtracker like bugsense here.

            return new SnSDefaultExceptionHandler()
                {
                    RootFrame = RootFrame,
                    Logger = new SnSModernLogger() {LogLevel = Constants.LOG_LEVEL},
                    I18N =
                        new SnSI18N()
                            {
                                DialogBoxErrorTitle = AppResources.Problem,
                                InhandledProblemHint = AppResources.UnhandledProblem,
                                ConnectivityProblemHint = AppResources.ConnectivityProblem
                            }
                };
        }

        protected override void SetupLogger()
        {
            SnSLoggerWrapper.Instance.Logger = new SnSModernLogger() {LogLevel = Constants.LOG_LEVEL};
        }

        protected override void UnhandledExceptionCustom(Exception exception)
        {
            //TODO: report exception with the bug tracker here.
        }

    }

}