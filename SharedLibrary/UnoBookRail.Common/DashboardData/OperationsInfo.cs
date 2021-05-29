using System;
using System.Collections.Generic;

namespace UnoBookRail.Common.DashboardData
{
    public class OperationsInfo
    {
        public static List<(string Hour, double Children, double Adults, double Seniors)> Passengers
        {
            get
            {
                return new List<(string, double, double, double)>
                {
                    ("05:00", 240,  1980,  90),
                    ("06:00", 670,  7850,  200),
                    ("07:00", 2400, 12500, 300),
                    ("08:00", 3000, 18000, 420),
                    ("09:00", 250,  8900,  1400),
                    ("10:00", 560,  3400,  2600),
                    ("11:00", 640,  4200,  1490),
                    ("12:00", 1280, 7850,  2350),
                    ("13:00", 1010, 5400,  1980),
                    ("14:00", 680,  3600,  1250),
                    ("15:00", 1610, 4850,  1450),
                    ("16:00", 1400, 5200,  1300),
                    ("17:00", 1315, 16500, 2100),
                    ("18:00", 1600, 14650, 3300),
                    ("19:00", 1430, 8750,  2140),
                    ("20:00", 1100, 7500,  1930),
                    ("21:00", 980,  6080,  1780),
                    ("22:00", 570,  5140,  1300),
                    ("23:00", 260,  2680,  680),
                };
            }
        }

        public static string CurrentPassengers
        {
            get
            {
                return new Random().Next(0, 10000).ToString();
            }
        }
    }
}
