using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Shell;
using ModernApp4Me.Core.App;
using ModernApp4Me.Core.ViewModel;
using Microsoft.Phone.Controls;
using ModernApp4Me.Core.LifeCycle;

namespace ModernApp4Me.WP8.SApp
{

    /// <summary>
    /// The basis class for all PhoneApplicationPages available in the framework.
    /// </summary>
    /// 
    /// <author>Ludovic ROLAND</author>
    /// <since>2014.09.08</since>
    public abstract class M4MPhoneApplicationPage : PhoneApplicationPage
    {

        private const string VIEW_MODEL_KEY = "viewModelKey";

        protected M4MBaseViewModel viewModel;

        protected ProgressIndicator progressIndicator;

        protected bool IsMVVMUsed = true;

        protected bool IsManagingProgressIndicatorItself = false;

        protected Panel mainContener;

        protected Panel connectivityContainer;

        //The State should be called before any await key word to avoid the system.InvalidOperationException: You can only use State between OnNavigatedTo and OnNavigatedFrom Exception
        //http://blog.ariankulp.com/2014/04/you-can-only-use-state-between.html
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            progressIndicator = RetrieveProgressIndicator();

            if (IsManagingProgressIndicatorItself == false)
            {
                progressIndicator.IsVisible = true;
            }

            SendPage();

            mainContener = RetrieveMainContainer();
            connectivityContainer = RetrieveConnectivityContainer();

            LoadQueryString();

            try
            {
                if (viewModel == null && IsMVVMUsed == true)
                {
                    if (State.ContainsKey(M4MPhoneApplicationPage.VIEW_MODEL_KEY) == true)
                    {
                        viewModel = State[M4MPhoneApplicationPage.VIEW_MODEL_KEY] as M4MBaseViewModel;
                    }
                    else
                    {
                        viewModel = await ComputeViewModel();
                    }
                }
            }
            catch (M4MConnectivityException exception)
            {
                OnDisplayConnectivityLayout();
                throw new M4MConnectivityException(exception);
            }

            OnFullfillDisplayObjects();

            if (connectivityContainer != null)
            {
                connectivityContainer.Visibility = Visibility.Collapsed;
            }

            if (mainContener != null)
            {
                mainContener.Visibility = Visibility.Visible;
            }

            if (IsManagingProgressIndicatorItself == false)
            {
                progressIndicator.IsVisible = false;
            }

            base.OnNavigatedTo(e);
        }

        /// <summary>
        /// Override this in order to send the PhoneApplicationPage when it is displayed.
        /// </summary>
        protected virtual void SendPage()
        {
        }

        /// <summary>
        /// Override this in order to personnalize the user experience when a <see cref="M4MConnectivityException"/> occurs.
        /// </summary>
        protected virtual void OnDisplayConnectivityLayout()
        {
            if (IsManagingProgressIndicatorItself == false)
            {
                progressIndicator.IsVisible = false;
            }

            if (connectivityContainer != null)
            {
                connectivityContainer.Visibility = Visibility.Visible;
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if (e.NavigationMode != NavigationMode.Back && viewModel != null)
            {
                State[M4MPhoneApplicationPage.VIEW_MODEL_KEY] = viewModel;
            }
        }

        /// <summary>
        /// This is the place where to load the business objects, from memory, local persistence, via web services, necessary for the entity processing.
        /// This callback is invoked from a background thread, and not the UI thread.
        /// This method is invoked only once during the <see cref="M4MPhoneApplicationPage"/> life cycle.
        /// Never invoke this method, only the framework should, because this is a callback!
        /// </summary>
        /// <returns>a class that implements <see cref="M4MBaseViewModel"/> or null if you set <see cref="M4MPhoneApplicationPage.IsMVVMUsed"/> to false</returns>
        protected abstract Task<M4MBaseViewModel> ComputeViewModel();

        /// <summary>
        /// This is the place where the implementing class can initialize the previously retrieved graphical objects.
        /// This method is invoked only once during the <see cref="M4MPhoneApplicationPage"/> life cycle.
        /// It is ensured that this method will be invoked from the UI thread!
        /// Never invoke this method, only the framework should, because this is a callback!
        /// </summary>
        protected abstract void OnFullfillDisplayObjects();

        /// <summary>
        /// Returns the <see cref="Panel"/> in which data are displayed.
        /// This <see cref="Panel"/> will be displayed or hidden automatically by the <see cref="M4MPhoneApplicationPage"/> during the life cycle.
        /// </summary>
        /// <returns>the <see cref="Panel"/> in which data are displayed</returns>
        protected abstract Panel RetrieveMainContainer();

        /// <summary>
        /// Override this in order to return the <see cref="Panel"/> which displays a particular message when a <see cref="M4MConnectivityException"/> is thrown.
        /// This <see cref="Panel"/> will be displayed or hidden automatically by the <see cref="M4MPhoneApplicationPage"/> during the life cycle.
        /// </summary>
        /// <returns>the <see cref="Panel"/> which display the message or null</returns>
        protected virtual Panel RetrieveConnectivityContainer()
        {
            return null;
        }

        /// <summary>
        /// Returns the <see cref="ProgressIndicator"/> of the <see cref="M4MPhoneApplicationPage"/>.
        /// </summary>
        /// <returns>a <see cref="ProgressIndicator"/> or null if you set <see cref="M4MPhoneApplicationPage.IsManagingProgressIndicatorItself" /> to true</returns>
        protected abstract ProgressIndicator RetrieveProgressIndicator();

        /// <summary>
        /// This is the place where to retrieve data from the <see cref="NavigationContext.QueryString"/>.
        /// This method is invoked before the <see cref="M4MPhoneApplicationPage.ComputeViewModel()"/>.
        /// </summary>
        protected virtual void LoadQueryString()
        {
        }

        /// <summary>
        /// This is the place where you can update the <see cref="M4MPhoneApplicationPage.viewModel"/> after the invokation of the method <see cref="M4MPhoneApplicationPage.Refresh()"/>.
        /// This callback is invoked from a background thread, and not the UI thread.
        /// Never invoke this method, only the framework should, because this is a callback!
        /// </summary>
        protected virtual Task RefreshViewModel()
        {
            return null;
        }

        /// <summary>
        /// Invokes this method when you want to reload the business objects, from memory, local persistence, via web services, necessary for the entity processing.
        /// </summary>
        /// <returns></returns>
        protected async Task Refresh()
        {
            if (IsManagingProgressIndicatorItself == false)
            {
                progressIndicator.IsVisible = true;
            }

            if (connectivityContainer != null && connectivityContainer.Visibility == Visibility.Visible)
            {
                try
                {
                    viewModel = await ComputeViewModel();
                }
                catch (M4MConnectivityException exception)
                {
                    OnDisplayConnectivityLayout();
                    throw new M4MConnectivityException(exception);
                }

                OnFullfillDisplayObjects();

                if (connectivityContainer != null)
                {
                    connectivityContainer.Visibility = Visibility.Collapsed;
                }

                if (mainContener != null)
                {
                    mainContener.Visibility = Visibility.Visible;
                }

                if (IsManagingProgressIndicatorItself == false)
                {
                    progressIndicator.IsVisible = false;
                }
            }
            else
            {
                try
                {
                    await RefreshViewModel();
                }
                catch (M4MConnectivityException exception)
                {
                    if (IsManagingProgressIndicatorItself == false)
                    {
                        progressIndicator.IsVisible = false;
                    }

                    throw new M4MConnectivityException(exception);
                }

            }

            if (IsManagingProgressIndicatorItself == false)
            {
                progressIndicator.IsVisible = false;
            }
        }

    }

}
