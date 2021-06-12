using DigitalTicket.ViewModels;
using Windows.UI.Xaml.Controls;

namespace DigitalTicket.Views
{
    public sealed partial class SettingsPage : Page
    {
        private SettingsViewModel settingsVM = new SettingsViewModel();

        public SettingsPage()
        {

            InitializeComponent();

            LanguagesComboBox.ItemsSource = settingsVM.LanguageOptions;
        }
    }
}
