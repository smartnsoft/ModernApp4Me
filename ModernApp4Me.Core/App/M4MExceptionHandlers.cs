using System;
using ModernApp4Me.Core.Log;

namespace ModernApp4Me.Core.App
{

    /// <summary>
    /// A class base for a SnSExceptionHandler which pops-up error dialog boxes and toasts.
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.24</since>
    public abstract class SnSExceptionHandler
    {

        public M4MLogger Logger { get; set; }

        public M4Mi18N I18N { get; set; }

        public virtual void AnalyseException(Exception exception)
        {
            if (exception is M4MBusinessObjectUnavailableException)
            {
                ShowMessageBox(I18N.InhandledProblemHint);
            }
            else if (exception is M4MConnectivityException)
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
