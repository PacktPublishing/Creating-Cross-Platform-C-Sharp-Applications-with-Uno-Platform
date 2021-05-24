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
                navigationVM.PropertyChanged += Navigation_PropertyChanged;
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

        private void Navigation_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(navigationVM.PageType))
            {
                ContentFrame.Navigate(navigationVM.PageType);
            }
        }
    }

}
