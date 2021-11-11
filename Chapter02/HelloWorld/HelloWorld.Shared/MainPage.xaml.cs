using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HelloWorld
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ChangeTextButton_Click(object sender, RoutedEventArgs e)
        {
#if __ANDROID__ || __IOS__
            helloTextBlock.Text = "Hello from C# on mobile!";
#elif HAS_UNO_WASM || __WASM__
            helloTextBlock.Text = "Hello from C# on WASM!";
#else
            helloTextBlock.Text = HelloWorld.Helpers.Greetings.GetStandardGreeting();
#endif
        }

    }
}
