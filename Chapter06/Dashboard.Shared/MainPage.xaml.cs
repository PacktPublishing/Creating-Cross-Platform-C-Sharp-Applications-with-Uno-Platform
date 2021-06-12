//using Microsoft.UI.Xaml.Controls;
//using Page = Windows.UI.Xaml.Controls.Page;

using Dashboard.Views;
using System;
using Windows.UI.Xaml.Controls;

namespace Dashboard
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            InnerFrame.Navigate(typeof(FinancePage));
        }

        private void NavItemSelected(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var item = (args.SelectedItem as NavigationViewItem).Content.ToString();

            Type page = null;

            switch (item)
            {
                case "Finance":
                    page = typeof(FinancePage);
                    break;
                case "Operations":
                    page = typeof(OperationsPage);
                    break;
                case "Network":
                    page = typeof(NetworkPage);
                    break;
            }

            if (page != null && InnerFrame.CurrentSourcePageType != page)
            {
                InnerFrame.Navigate(page);
            }
        }

        private void NavBackRequested(object sender, NavigationViewBackRequestedEventArgs e)
        {
            InnerFrame.GoBack();
        }
    }
}
