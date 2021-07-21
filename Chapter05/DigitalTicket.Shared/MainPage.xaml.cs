using DigitalTicket.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using DigitalTicket.Views;
using Windows.UI.Xaml.Navigation;
using System;

namespace DigitalTicket
{

    public sealed partial class MainPage : Page
    {
        public NavigationViewModel navigationVM = new NavigationViewModel();

        public MainPage()
        {
            InitializeComponent();

            if (navigationVM.PageType is null)
            {
                AppNavigation.SelectedItem = JourneyBookingItem;
                navigationVM.PageType = typeof(JourneyBookingPage);
                navigationVM.PageTypeChanged += NavigationVM_PageTypeChanged;
            }

            ContentFrame.Navigate(navigationVM.PageType);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is Type navigateToType)
            {
                if (navigateToType == typeof(SettingsPage))
                {
                    AppNavigation.SelectedItem = AppNavigation.SettingsItem;
                }
                navigationVM.PageType = navigateToType;
                ContentFrame.Navigate(navigateToType);
            }
        }

        private void NavigationVM_PageTypeChanged(object sender, EventArgs e)
        {
            ContentFrame.Navigate(navigationVM.PageType);
        }
    }

}
