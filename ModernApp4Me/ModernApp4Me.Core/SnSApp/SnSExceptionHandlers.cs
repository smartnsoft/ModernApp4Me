using System;
using ModernApp4Me.Core.SnSLog;

namespace ModernApp4Me.Core.SnSApp
{

    /// <summary>
    /// A class base for a SnSExceptionHandler which pops-up error dialog boxes and toasts.
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.24</since>
    public abstract class SnSExceptionHandler
    {

        public SnSLogger Logger { get; set; }

        public SnSI18N I18N { get; set; }

        public void AnalyseException(Exception exception)
        {
            if (exception is SnSBusinessObjectUnavailableException)
            {
                ShowMessageBox(I18N.InhandledProblemHint);
            }
            else if (exception is SnSConnectivityException)
            {
                ShowMessageBox(I18N.ConnectivityProblemHint);
            }
            else
            {
                ShowMessageBox(I18N.InhandledProblemHint);
            }
        }

        protected abstract void ShowMessageBox(string message);

    }

}
