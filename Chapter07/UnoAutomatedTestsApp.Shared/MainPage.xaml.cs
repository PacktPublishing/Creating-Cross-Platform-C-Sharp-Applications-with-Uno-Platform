using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using UnoBookRail.Common.Auth;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UnoAutomatedTestsApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        private void Username_TextChanged(object sender, TextChangedEventArgs e)
        {
            SignInButton.IsEnabled = UsernameInput.Text.Length > 0 && PasswordInput.Password.Length > 0;
        }
        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            SignInButton.IsEnabled = UsernameInput.Text.Length > 0 && PasswordInput.Password.Length > 0;
        }
        private void SignInButton_Click(object sender, RoutedEventArgs args)
        {
            var signInResult = Authentication.SignIn(UsernameInput.Text, PasswordInput.Password);
            if (!signInResult.IsSuccessful && signInResult.Messages.Count > 0)
            {
                SignInErrorMessageTextBlock.Text = signInResult.Messages[0];
                SignInErrorMessageTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                SignInErrorMessageTextBlock.Visibility = Visibility.Collapsed;
                SignInForm.Visibility = Visibility.Collapsed;
                SignedInLabel.Visibility = Visibility.Visible;
            }
        }

    }
}
