using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Capptain.Agent;
using Microsoft.Phone.Shell;
using ModernApp4Me.Core.SnSApp;
using ModernApp4Me.Core.SnSViewModel;

namespace ModernApp4Me.WP8.SnSApp
{

    /// <author>Ludovic ROLAND</author>
    /// <since>2014.09.08</since>
    public abstract class SnSPage : CapptainPage
    {

        private const string VIEW_MODEL_KEY = "viewModelKey";

        protected SnSViewModelBase viewModel;

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

            OnNavigatedToAnalytics();

            mainContener = RetrieveMainContainer();
            connectivityContainer = RetrieveConnectivityContainer();

            LoadQueryString();

            try
            {
                if (viewModel == null && IsMVVMUsed == true)
                {
                    if (State.ContainsKey(SnSPage.VIEW_MODEL_KEY) == true)
                    {
                        viewModel = State[SnSPage.VIEW_MODEL_KEY] as SnSViewModelBase;
                    }
                    else
                    {
                        viewModel = await ComputeViewModel();
                    }
                }
            }
            catch (SnSConnectivityException exception)
            {
                OnDisplayConnectivityLayout();
                throw new SnSConnectivityException(exception);
            }

            OnFullfillDisplayObjects();

            if (connectivityContainer != null)
            {
                connectivityContainer.Visibility = Visibility.Collapsed;
            }

            mainContener.Visibility = Visibility.Visible;

            if (IsManagingProgressIndicatorItself == false)
            {
                progressIndicator.IsVisible = false;
            }

            base.OnNavigatedTo(e);
        }

        protected virtual void OnNavigatedToAnalytics()
        {
        }

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
                State[SnSPage.VIEW_MODEL_KEY] = viewModel;
            }
        }

        protected abstract Task<SnSViewModelBase> ComputeViewModel();

        protected abstract void OnFullfillDisplayObjects();

        protected abstract Panel RetrieveMainContainer();

        protected virtual Panel RetrieveConnectivityContainer()
        {
            return null;
        }

        protected abstract ProgressIndicator RetrieveProgressIndicator();

        protected virtual void LoadQueryString()
        {
        }

        protected virtual void SaveQueryString()
        {
        }

        protected virtual Task RefreshViewModel()
        {
            return null;
        }

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
                catch (SnSConnectivityException exception)
                {
                    OnDisplayConnectivityLayout();
                    throw new SnSConnectivityException(exception);
                }

                OnFullfillDisplayObjects();

                if (connectivityContainer != null)
                {
                    connectivityContainer.Visibility = Visibility.Collapsed;
                }

                mainContener.Visibility = Visibility.Visible;

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
                catch (SnSConnectivityException exception)
                {
                    if (IsManagingProgressIndicatorItself == false)
                    {
                        progressIndicator.IsVisible = false;
                    }

                    throw new SnSConnectivityException(exception);
                }

            }

            if (IsManagingProgressIndicatorItself == false)
            {
                progressIndicator.IsVisible = false;
            }
        }

    }

}
