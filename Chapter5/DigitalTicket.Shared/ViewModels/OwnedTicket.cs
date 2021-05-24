using DigitalTicket.Utils;
using SQLite;
using System.Windows.Input;
using UnoBookRail.Common.Tickets;

namespace DigitalTicket.ViewModels
{
    public class OwnedTicket
    {
        [PrimaryKey, AutoIncrement]
        public int DBId { get; set; }
        public string TicketName => LocalizedResources.GetString(Type.ToString() + "Label");
        public PricingOptionType Type { get; set; }
        public string TicketID { get; set; }
        public string StartStation { get; set; }
        public string EndStation { get; set; }

        public ICommand ShowQRCodeCommand;

        // Constructor used by SQLite
        public OwnedTicket() { }

        public OwnedTicket(Ticket ticket)
        {
            Type = ticket.Type;
            TicketID = ticket.TicketID;
            StartStation = ticket.StartStation;
            EndStation = ticket.EndStation;
        }
    }
}
