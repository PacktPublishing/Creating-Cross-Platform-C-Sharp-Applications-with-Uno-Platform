using System;

namespace UnoBookRail.Common.Tickets
{
    public class Ticket
    {
        public readonly PricingOptionType Type;
        public readonly string UserID;
        public readonly string Price;
        public readonly DateTime CreationTime;
        public readonly string TicketID;
        public readonly string StartStation;
        public readonly string EndStation;

        public Ticket(PricingOptionType type, string userID, string price, string ticketID, string startStation, string endStation)
        {
            Type = type;
            UserID = userID;
            Price = price;
            CreationTime = DateTime.Now;
            TicketID = ticketID;
            StartStation = startStation;
            EndStation = endStation;
        }
    }
}
