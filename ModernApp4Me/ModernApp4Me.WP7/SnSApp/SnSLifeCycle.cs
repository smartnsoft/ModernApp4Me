using System.Windows.Navigation;

namespace  ModernApp4Me.WP7.SnSApp
{

    /// <summary>
    /// Provides functions to work with the application life cycle.
    /// </summary>
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.03.24</since>
    public static class SnSLifeCycle
    {

        /// <summary>
        /// Exits the app.
        /// Can be used only in the OnBackKeyPress function of a PhoneApplicationPage.
        /// </summary>
        /// <param name="navigationService"></param>
        public static void ExitApplication(NavigationService navigationService)
        {
            if (navigationService.CanGoBack == false)
            {
                return;
            }

            while (navigationService.RemoveBackEntry() != null)
            {
                navigationService.RemoveBackEntry();
            }
        }

    }

}
