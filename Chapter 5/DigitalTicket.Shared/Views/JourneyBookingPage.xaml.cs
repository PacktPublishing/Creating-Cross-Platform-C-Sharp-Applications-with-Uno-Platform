using DigitalTicket.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DigitalTicket.Views
{
    public sealed partial class JourneyBookingPage : Page
    {
        private JourneyBookingViewModel journeyBookingVM = new JourneyBookingViewModel();

        public JourneyBookingPage()
        {
            this.InitializeComponent();
            journeyBookingVM.PropertyChanged += JourneyBookingViewMode_PropertyChanged;

            Loaded += JournyBookingPage_Loaded;
        }

        private void JournyBookingPage_Loaded(object sender, RoutedEventArgs e)
        {
            SelectedTicketRadioButtons.ItemsSource = journeyBookingVM.TicketOptions;

            // Workaround for https://github.com/unoplatform/uno/issues/5886
            SelectedTicketRadioButtons.UpdateLayout();
            SelectedTicketRadioButtons.SelectedIndex = journeyBookingVM.SelectedTicketIndex;
        }

        private void JourneyBookingViewMode_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(journeyBookingVM.TicketOptions))
            {
                // Update ItemsSource
                SelectedTicketRadioButtons.ItemsSource = journeyBookingVM.TicketOptions;
                // Ensure RadioButtons are updated
                SelectedTicketRadioButtons.UpdateLayout();
                // Restore selection
                SelectedTicketRadioButtons.SelectedIndex = journeyBookingVM.SelectedTicketIndex;
            }
            else if (e.PropertyName == nameof(journeyBookingVM.BookedTicket) && journeyBookingVM.BookedTicket)
            {
                ConfirmBookingPanel.Visibility = Visibility.Collapsed;
                TicketBookedInfoBar.IsOpen = true;
            }
        }

        private void SelectedTicketRadioButtons_SelectionChanged(object sender, SelectionChangedEventArgs eventArgs)
        {
            if (SelectedTicketRadioButtons.SelectedIndex != -1)
            {
                journeyBookingVM.SelectedTicketIndex = SelectedTicketRadioButtons.SelectedIndex;
            }
        }

        private void BookTicketButton_Click(object sender, RoutedEventArgs e)
        {
            journeyBookingVM.SelectedTicketIndex = SelectedTicketRadioButtons.SelectedIndex;
            journeyBookingVM.BookedTicket = false;
            TicketBookedInfoBar.IsOpen = false;
            ConfirmBookingPanel.Visibility = Visibility.Visible;
        }

        private void CancelBookingProcessButton_Click(object sender, RoutedEventArgs e)
        {
            ConfirmBookingPanel.Visibility = Visibility.Collapsed;
        }
    }
}
