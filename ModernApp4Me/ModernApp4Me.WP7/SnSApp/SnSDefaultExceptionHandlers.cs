using System.Windows;
using Microsoft.Phone.Controls;

namespace ModernApp4Me.WP7.SnSApp
{

    public class SnSDefaultExceptionHandler : SnSExceptionHandler
    {

        public PhoneApplicationFrame RootFrame { get; set; }

        protected override void ShowMessageBox(string message)
        {
            var result = MessageBox.Show(message, I18N.DialogBoxErrorTitle, MessageBoxButton.OK);

            if (result == MessageBoxResult.OK)
            {
                if (RootFrame.CanGoBack == true)
                {
                    RootFrame.RemoveBackEntry();
                }
            }
        }

    }

}
