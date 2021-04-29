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

        public string DisplayedTime
        {
            get
            {
                if (MinutesUntilDue < 2)
                {
                    return "Due";
                }
                else if (MinutesUntilDue > 10)
                {
                    return TimeDue;
                }
                else
                {
                    return $"{MinutesUntilDue} mins";
                }
            }
        }
    }
}
