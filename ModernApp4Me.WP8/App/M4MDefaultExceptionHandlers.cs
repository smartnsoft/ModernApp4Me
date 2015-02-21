using System.Windows;
using Microsoft.Phone.Controls;
using ModernApp4Me.Core.App;

namespace ModernApp4Me.WP8.App
{

    /// <summary>
    /// A extension implementation for Windows Phone, which displays MessageBoxes.
    /// </summary>
    /// 
    /// <author>Ludovic Roland</author>
    /// <since>2014.03.24</since>
    public class SnSDefaultExceptionHandler : M4MExceptionHandlers
    {

        public PhoneApplicationFrame RootFrame { get; set; }

        protected override void ShowMessageBox(string message)
        {
            var result = MessageBox.Show(message, I18N.DialogBoxErrorTitle, MessageBoxButton.OK);

            if (result == MessageBoxResult.OK)
            {
                if (RootFrame.CanGoBack == true)
                {
                    RootFrame.GoBack();
                }
                else
                {
                    Application.Current.Terminate();
                }
            }
        }

        protected override void ShowConnectivityMessageBox(string message)
        {
            var result = MessageBox.Show(message, I18N.DialogBoxErrorTitle, MessageBoxButton.OK);

            if (result == MessageBoxResult.OK)
            {
                if (RootFrame.CanGoBack == true)
                {
                    RootFrame.GoBack();
                }
            }
        }

    }

}
