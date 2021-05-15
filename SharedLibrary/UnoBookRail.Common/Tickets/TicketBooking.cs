using System;
using System.Collections.Generic;
using System.Text;
using UnoBookRail.Common.Auth;

namespace UnoBookRail.Common.Tickets
{
    public class TicketBooking
    {
        public static Ticket BookPricingOption(PricingOption option, User user, string startStation, string endStation)
        {
            return new Ticket(option.OptionType, user.Identifier, option.Price, DateTime.UtcNow.ToString() + "secret", startStation, endStation);
        }
    }
}
