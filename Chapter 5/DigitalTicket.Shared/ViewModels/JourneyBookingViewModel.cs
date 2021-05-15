using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using UnoBookRail.Common.Auth;
using UnoBookRail.Common.Network;
using UnoBookRail.Common.Tickets;

// Uncomment after adding the OwnedTicketsRepository
//using DigitalTicket.Models;

namespace DigitalTicket.ViewModels
{
    public class JourneyBookingViewModel : ObservableObject
    {
        public readonly List<Station> AllStations;
        private Station selectedStartpoint;
        public Station SelectedStartpoint
        {
            get
            {
                return selectedStartpoint;
            }
            set
            {
                SetProperty(ref selectedStartpoint, value);
                UpdateAvailableEndPoints();
            }
        }

        private ObservableCollection<Station> availableDestinations = new ObservableCollection<Station>();
        public ObservableCollection<Station> AvailableDestinations
        {
            get
            {
                return availableDestinations;
            }
            set
            {
                SetProperty(ref availableDestinations, value);
            }
        }

        private Station selectedEndpoint;
        public Station SelectedEndpoint
        {
            get
            {
                return selectedEndpoint;
            }
            set
            {
                SetProperty(ref selectedEndpoint, value);
                if (value != null)
                {
                    UpdateAvailableOptions();
                }
            }
        }

        private IList<JourneyBookingOption> ticketOptions = new List<JourneyBookingOption>();
        public IList<JourneyBookingOption> TicketOptions
        {
            get
            {
                return ticketOptions;
            }
            set
            {
                SetProperty(ref ticketOptions, value);
            }
        }

        public JourneyBookingOption SelectedTicketOption
        {
            get
            {
                return TicketOptions.ElementAt(selectedTicketIndex);
            }
        }

        private int selectedTicketIndex;
        public int SelectedTicketIndex
        {
            get
            {
                return selectedTicketIndex;
            }
            set
            {
                SetProperty(ref selectedTicketIndex, value);
                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(SelectedTicketOption)));
            }
        }

        public readonly ICommand BookJourney;

        private bool bookedTicket;
        public bool BookedTicket
        {
            get
            {
                return bookedTicket;
            }
            set
            {
                SetProperty(ref bookedTicket, value);
            }
        }

        public JourneyBookingViewModel()
        {
            AllStations = new Stations().GetAll();
            AllStations.Sort((stationA, stationB) => { return stationA.Name.CompareTo(stationB.Name); });
            selectedStartpoint = AllStations[0];

            AvailableDestinations = new ObservableCollection<Station>(AllStations);
            AvailableDestinations.Remove(selectedStartpoint);
            SelectedEndpoint = AvailableDestinations[0];

            // Use this until we have added the OwnedTicketsRepository
            BookJourney = new RelayCommand(() =>
            {
                BookedTicket = true;
            });

            // Uncomment after adding the OwnedTicketsRepository class
            //BookJourney = new RelayCommand(() =>
            //{
            //    var ticket = TicketBooking.BookPricingOption(SelectedTicketOption.Option, Users.DemoUser, SelectedStartpoint.Name, SelectedEndpoint.Name);
            //    OwnedTicketsRepository.SaveTicketAsync(new OwnedTicket(ticket));
            //    BookedTicket = true;
            //});
        }

        private void UpdateAvailableOptions()
        {
            TicketOptions = PricingCalculator.CalculatePrices(selectedStartpoint, selectedEndpoint)
                .Select(option => new JourneyBookingOption(option)).ToList();
        }

        private void UpdateAvailableEndPoints()
        {
            // Update the list of end stations to not include the current startstation
            // but to include the previous startstation before selection changed.
            AvailableDestinations.Remove(selectedStartpoint);
            for (int i = 0; i < AllStations.Count; i++)
            {
                if (!AvailableDestinations.Contains(AllStations[i]) && AllStations[i] != selectedStartpoint)
                {
                    if (i == 0 || AvailableDestinations[i - 1] == AllStations[i - 1])
                    {
                        // If we are before the removed element (indices are the same until here)
                        // We insert right here
                        AvailableDestinations.Insert(i, AllStations[i]);
                    }
                    else
                    {
                        // Else, we are past the removed element.
                        // In that case, AllStations elements have an index one higher than
                        // their counterpart in AvailableDestinations
                        AvailableDestinations.Insert(i - 1, AllStations[i]);
                    }
                }
            }

            if (selectedEndpoint is null)
            {
                SelectedEndpoint = AvailableDestinations[0];
            }
        }
    }
}
