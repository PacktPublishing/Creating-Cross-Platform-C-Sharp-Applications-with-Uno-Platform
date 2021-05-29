using System.Collections.Generic;
using System.Linq;
using UnoBookRail.Common.DashboardData;
using Windows.UI.Xaml.Controls;

namespace Dashboard.Views
{
    public sealed partial class OperationsPage : Page
    {
        public OperationsPage()
        {
            this.InitializeComponent();
        }

        public string PsngrCount => OperationsInfo.CurrentPassengers;

        private List<PersonCount> Passengers
            => OperationsInfo.Passengers.Select(p
                => new PersonCount(p.Hour, p.Children, p.Adults, p.Seniors)).ToList();
    }
}
