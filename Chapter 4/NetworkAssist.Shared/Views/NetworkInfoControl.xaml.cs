using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NetworkAssist.Views
{
    public sealed partial class NetworkInfoControl : UserControl
    {
        private readonly Uri Url = new Uri($"{ViewModels.DataService.WebApiDomain}/networkinfo");

        public NetworkInfoControl()
        {
            InitializeComponent();
            TheWebView.Source = Url;
        }

        private void WebViewNavCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            if (!args.IsSuccess)
            {
                HideWebViewShowErrorMsg();
            }
        }

        private void WebViewNavFailed(object sender, WebViewNavigationFailedEventArgs e)
        {
            HideWebViewShowErrorMsg();
        }

        private void HideWebViewShowErrorMsg()
        {
            TheWebView.Visibility = Visibility.Collapsed;
            FailureInfo.Visibility = Visibility.Visible;
        }

        private void RefreshClicked(object sender, RoutedEventArgs e)
        {
            FailureInfo.Visibility = Visibility.Collapsed;
            TheWebView.Visibility = Visibility.Visible;
            TheWebView.Refresh();
        }
    }
}
