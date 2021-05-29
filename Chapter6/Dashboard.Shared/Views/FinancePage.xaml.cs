
using System.Collections.Generic;
using System.Linq;
using UnoBookRail.Common.DashboardData;
using Windows.UI.Xaml.Controls;

namespace Dashboard.Views
{
    public sealed partial class FinancePage : Page
    {
        public FinancePage()
        {
            this.InitializeComponent();
        }

        public List<HourlySales> DailySales
            => FinanceInfo.DailySales.Select(s => new HourlySales(s.Hour, s.Sales)).ToList();

        public string TrendArrows => FinanceInfo.TrendArrows;
    }
}
