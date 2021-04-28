using System.Collections.Generic;

namespace NetworkAssist.ViewModels
{
    public class StationArrivalDetails : List<DisplayedArrivalDetail>
    {
        public StationArrivalDetails(string platform)
        {
            Platform = platform;
        }

        public string Platform { get; set; }
    }
}
