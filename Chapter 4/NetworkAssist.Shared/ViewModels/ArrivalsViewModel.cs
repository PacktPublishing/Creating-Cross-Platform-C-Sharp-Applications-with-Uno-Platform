using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using UnoBookRail.Common.Network;
using Windows.UI.Xaml.Data;

namespace NetworkAssist.ViewModels
{
    public class ArrivalsViewModel : Microsoft.Toolkit.Mvvm.ComponentModel.ObservableObject
    {
        private static readonly DataService _data = DataService.Instance;

        private List<Station> _listOfStations;
        private ObservableCollection<StationArrivalDetails> _arrivals
            = new ObservableCollection<StationArrivalDetails>();
        private Station _selectedStation = null;
        private string _dataTimestamp;
        private bool _isBusy;
        private bool _showErrorMsg;

        public ArrivalsViewModel()
        {
            ListOfStations = _data.GetAllStations();

            RefreshCommand = new AsyncRelayCommand(async () => { await LoadArrivalsDataAsync(); });
            SelectionChangedCommand = new AsyncRelayCommand(async () => { await LoadArrivalsDataAsync(); });
        }

        public List<Station> ListOfStations
        {
            get => _listOfStations;
            set => SetProperty(ref _listOfStations, value);
        }

        public bool ShowErrorMsg
        {
            get => _showErrorMsg;
            set => SetProperty(ref _showErrorMsg, value);
        }

        public Station SelectedStation
        {
            get => _selectedStation;

            set
            {
                if (SetProperty(ref _selectedStation, value))
                {
                    OnPropertyChanged(nameof(ShowNoStnMsg));
                }
            }
        }

        public ObservableCollection<StationArrivalDetails> Arrivals
        {
            get => _arrivals;
            set => SetProperty(ref _arrivals, value);
        }

        public string DataTimestamp
        {
            get => _dataTimestamp;
            set => SetProperty(ref _dataTimestamp, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public IEnumerable<object> ArrivalsViewSource => new CollectionViewSource()
        {
            Source = Arrivals,
            IsSourceGrouped = true
        }.View;

        public bool ShowNoStnMsg => SelectedStation == null;

        public ICommand RefreshCommand { get; }

        public ICommand SelectionChangedCommand { get; }

        public async Task LoadArrivalsDataAsync(int stationId = 0)
        {
            if (stationId < 1)
            {
                stationId = SelectedStation?.Id ?? 0;
            }
            else
            {
                // We've changed station so clear current details
                // Don't want to show data for the previosuly selected station
                Arrivals.Clear();
                DataTimestamp = string.Empty;
                ShowErrorMsg = false;
            }

            if (stationId > 0)
            {
                IsBusy = true;

                try
                {
                    var arr = await _data.GetArrivalsForStationAsync(stationId);

                    ShowErrorMsg = false;

                    // Check that data is for what we want
                    //  - avoid race conditions if make multiple requests and change station before one request returns
                    if (arr.ForStationId == stationId)
                    {
                        DataTimestamp = $"Updated at {arr.Timestamp:t}";

                        Arrivals.Clear();

                        if (!string.IsNullOrEmpty(arr.DirectionOneName))
                        {
                            var d1details = new StationArrivalDetails(arr.DirectionOneName);
                            d1details.AddRange(arr.DirectionOneDetails);

                            Arrivals.Add(d1details);
                        }

                        if (!string.IsNullOrEmpty(arr.DirectionTwoName))
                        {
                            var d2details = new StationArrivalDetails(arr.DirectionTwoName);
                            d2details.AddRange(arr.DirectionTwoDetails);

                            Arrivals.Add(d2details);
                        }
                    }
                }
                catch (Exception exc)
                {
                    // Log this or take other appropriate action
                    System.Diagnostics.Debug.WriteLine(exc);
                    ShowErrorMsg = true;
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }
    }
}
