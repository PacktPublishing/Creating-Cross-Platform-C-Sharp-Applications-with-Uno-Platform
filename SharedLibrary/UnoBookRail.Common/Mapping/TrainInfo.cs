using UnoBookRail.Common.Network;

namespace UnoBookRail.Common.Mapping
{
    public class TrainInfo
    {
        public CompassDirection Direction { get; set; }
        public int StationLeft { get; set; }
        public int StationApproaching { get; set; }
        public bool Delayed { get; set; }
        public (float X, float Y) MapPosition { get; set; }
        public bool AtStation => StationLeft == StationApproaching;
    }
}
