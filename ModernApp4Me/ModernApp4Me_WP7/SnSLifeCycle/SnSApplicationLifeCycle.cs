using System.Windows.Navigation;

namespace  ModernApp4Me_WP7.SnSLifeCycle
{
    /// <summary>
    /// Provides functions to work with the application life cycle.
    /// </summary>
    public static class SnSApplicationLifeCycle
    {
        /*******************************************************/
        /** METHODS.
        /*******************************************************/
        /// <summary>
        /// Exits the app.
        /// Can be used only in the OnBackKeyPress function of an PhoneApplicationPage.
        /// </summary>
        public static void ExitApplication(NavigationService navigationService)
        {
            if (!navigationService.CanGoBack) return;

            while (navigationService.RemoveBackEntry() != null)
            {
                navigationService.RemoveBackEntry();
            }
        }
    }
}
