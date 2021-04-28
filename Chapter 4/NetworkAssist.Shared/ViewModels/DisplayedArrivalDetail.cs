using UnoBookRail.Common.Network;

namespace NetworkAssist.ViewModels
{
    public class DisplayedArrivalDetail
    {
        public DisplayedArrivalDetail(ArrivalDetail arrivalDetail)
        {
            this.Destination = arrivalDetail.Destination;

            if (arrivalDetail.MinutesUntilDue < 2)
            {
                DisplayedTime = "Due";
            }
            else if (arrivalDetail.MinutesUntilDue > 10)
            {
                DisplayedTime = arrivalDetail.TimeDue;
            }
            else
            {
                DisplayedTime = $"{arrivalDetail.MinutesUntilDue} mins";
            }
        }

        public string Destination { get; internal set; }

        public string DisplayedTime { get; internal set; }
    }
}
