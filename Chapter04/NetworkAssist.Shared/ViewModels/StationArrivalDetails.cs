using System.Collections.Generic;
using UnoBookRail.Common.Network;

namespace NetworkAssist.ViewModels
{
    public class StationArrivalDetails : List<ArrivalDetail>
    {
        public StationArrivalDetails(string platform)
        {
            Platform = platform;
        }

        public string Platform { get; set; }
    }
}
