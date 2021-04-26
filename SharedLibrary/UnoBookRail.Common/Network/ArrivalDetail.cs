namespace UnoBookRail.Common.Network
{
    public class ArrivalDetail
    {
        public ArrivalDetail()
        {
        }

        public ArrivalDetail(string destination, string timeDue, int minsUntilDue)
        {
            Destination = destination;
            TimeDue = timeDue;
            MinutesUntilDue = minsUntilDue;
        }

        public string Destination { get; set; }

        public string TimeDue { get; set; }

        public int MinutesUntilDue { get; set; }
    }
}
