using DigitalTicket.ViewModels;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace DigitalTicket.Views
{
    public sealed partial class OwnedTicketsPage : Page
    {
        private OwnedTicketsViewModel ownedTicketsVM = new OwnedTicketsViewModel();

        public OwnedTicketsPage()
        {
            this.InitializeComponent();

            if (ownedTicketsVM.Tickets.Count != 0)
            {
                NoTicketsLabel.Visibility = Visibility.Collapsed;
            }
        }
    }
}
