using System.Collections.Generic;

namespace UnoBookRail.Common.DashboardData
{
    public class FinanceInfo
    {
        public static List<(string Hour, double Sales)> DailySales
        {
            get
            {
                return new List<(string, double)>
                {
                    ("00:00", 60),
                    ("01:00", 250),
                    ("02:00", 280),
                    ("03:00", 320),
                    ("04:00", 440),
                    ("05:00", 8540),
                    ("06:00", 23932),
                    ("07:00", 44780),
                    ("08:00", 66780),
                    ("09:00", 72087),
                    ("10:00", 80314),
                    ("11:00", 88123),
                    ("12:00", 92450),
                    ("13:00", 103009),
                    ("14:00", 106512),
                    ("15:00", 110422),
                    ("16:00", 114580),
                    ("17:00", 121452),
                };
            }
        }

        public static string TrendArrows
        {
            get
            {
                // These arrows represent trend information.
                // The point up for an increase > 1 %, down for a decrease > 1 %, or side to side for anything else
                // In order, they represent:
                // 1st - comparison with the same day last week
                // 2nd - comparison with the equivalent day last month
                // 3rd - comparison with the equivalent day last year
                return "⬆ ↔ ⬇";
            }
        }
    }

}
