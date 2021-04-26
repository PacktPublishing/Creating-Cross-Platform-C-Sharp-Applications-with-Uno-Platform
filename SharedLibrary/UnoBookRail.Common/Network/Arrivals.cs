using System;
using System.Collections.Generic;

namespace UnoBookRail.Common.Network
{
    public class Arrivals
    {
        public int ForStationId { get; set; }

        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.Now;

        public string DirectionOneName { get; set; }

        public List<ArrivalDetail> DirectionOneDetails { get; set; } = new List<ArrivalDetail>();

        public string DirectionTwoName { get; set; }

        public List<ArrivalDetail> DirectionTwoDetails { get; set; } = new List<ArrivalDetail>();
    }
}
