using System;
using ModernApp4Me.Core.LifeCycle;
using ModernApp4Me.Core.Log;

namespace ModernApp4Me.Core.App
{

    /// <summary>
    /// A class base that should be extend in order to implement an exception handler which pops-up error dialog boxes.
    /// </summary>
    /// 
    /// <author>Ludovic Roland</author>
    /// <since>2014.03.24</since>
    public abstract class M4MExceptionHandlers
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
                ShowConnectivityMessageBox(I18N.ConnectivityProblemHint);
            }
            else
            {
                ShowMessageBox(I18N.InhandledProblemHint);
            }
        }

        protected abstract void ShowMessageBox(string message);

        protected abstract void ShowConnectivityMessageBox(string message);

    }

}
