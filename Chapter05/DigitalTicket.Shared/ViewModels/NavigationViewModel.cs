using DigitalTicket.Views;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using System;

namespace DigitalTicket.ViewModels
{
    public class NavigationViewModel : ObservableObject
    {
        private Type pageType;
        public Type PageType
        {
            get
            {
                return pageType;
            }
            set
            {
                SetProperty(ref pageType, value);
                PageTypeChanged?.Invoke(null, new EventArgs());
            }
        }

        public event EventHandler PageTypeChanged;

        public void NavigationView_SelectionChanged(NavigationView navigationView, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                PageType = typeof(SettingsPage);
            }
            else if(args.SelectedItem != null)
            {
                switch ((args.SelectedItem as NavigationViewItem).Tag.ToString())
                {
                    case "JourneyPlanner":
                        PageType = typeof(JourneyBookingPage);
                        break;
                    case "OwnedTickets":
                        PageType = typeof(OwnedTicketsPage);
                        break;
                }
            }
        }
    }

}
