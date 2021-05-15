using System;
using System.Collections.Generic;
using UnoBookRail.Common.Network;

namespace UnoBookRail.Common.Tickets
{
    public class PricingCalculator
    {
        public static List<PricingOption> CalculatePrices(Station start, Station end)
        {
            var options = new List<PricingOption>();

            var distance = new Stations().GetDistanceBetweenStations(start.Id, end.Id);

            options.Add(new PricingOption()
            {
                OptionType = PricingOptionType.AdultSingleTrip,
                Price = CalculateAdultOneWayTrip(distance)
            });
            options.Add(new PricingOption()
            {
                OptionType = PricingOptionType.AdultRoundTrip,
                Price = CalculateAdultRoundTrip(distance)
            });
            options.Add(new PricingOption()
            {
                OptionType = PricingOptionType.ChildSingleTrip,
                Price = CalculateChildOneWayTrip(distance)
            });
            options.Add(new PricingOption()
            {
                OptionType = PricingOptionType.ChildRoundTrip,
                Price = CalculateChildRoundTrip(distance)
            });
            options.Add(new PricingOption()
            {
                OptionType = PricingOptionType.Plus65SingleTrip,
                Price = Calculate65PlusOneWayTrip(distance)
            });
            options.Add(new PricingOption()
            {
                OptionType = PricingOptionType.Plus65RoundTrip,
                Price = Calculate65PlusRoundTrip(distance)
            });

            return options;
        }

        private static string CalculateAdultOneWayTrip(int distance)
        {
            return "$" + (1.10 + Math.Max(distance * 0.35, 2.5)).ToString("N2");
        }
        private static string CalculateAdultRoundTrip(int distance)
        {
            return "$" + (1.30 + Math.Max(distance * 0.6, 5)).ToString("N2");
        }
        private static string CalculateChildOneWayTrip(int distance)
        {
            return "$" + (0.8 + Math.Max(distance * 0.15, 2)).ToString("N2");
        }
        private static string CalculateChildRoundTrip(int distance)
        {
            return "$" + (1.00 + Math.Max(distance * 0.25, 4)).ToString("N2");
        }
        private static string Calculate65PlusOneWayTrip(int distance)
        {
            return "$" + (1.00 + Math.Max(distance * 0.25, 2)).ToString("N2");
        }
        private static string Calculate65PlusRoundTrip(int distance)
        {
            return "$" + (1.20 + Math.Max(distance * 0.45, 4)).ToString("N2");
        }
    }
}
