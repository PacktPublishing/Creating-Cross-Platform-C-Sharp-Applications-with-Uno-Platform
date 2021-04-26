namespace UnoBookRail.Common.Network
{
    public class Station
    {
        public Station()
        {
        }

        public Station(int id, string name, Branch branch, bool isTerminus = false)
        {
            Id = id;
            Name = name;
            Branch = branch;
            IsTerminus = isTerminus;
        }

        public int Id { get; internal set; }

        public string Name { get; internal set; }

        public bool IsTerminus { get; internal set; }

        public Branch Branch { get; internal set; }

        public PlatformDirections Directions
            => Branch == Branch.MainLine ? PlatformDirections.EastWest
                                         : PlatformDirections.NorthSouth;
    }
}
