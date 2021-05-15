using DigitalTicket.Utils;
using UnoBookRail.Common.Tickets;

namespace DigitalTicket.ViewModels
{
    public class JourneyBookingOption
    {
        public readonly string Title;

        public readonly string Price;

        public readonly PricingOption Option;

        public JourneyBookingOption(PricingOption option)
        {
            Title = LocalizedResources.GetString(option.OptionType.ToString() + "Label");
            Price = option.Price;
            Option = option;
        }
    }
}
