using System.Windows.Navigation;

namespace wp4me.SnSApplicationLifeCycleUtils
{
    /// <summary>
    /// Class that provides functions to deal with the application life cycle.
    /// </summary>
    public sealed class SnSApplicationLifeCycle
    {
        /*******************************************************/
        /** METHODS AND FUNCTIONS.
        /*******************************************************/
        /// <summary>
        /// Function that permits to exit the application.
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
