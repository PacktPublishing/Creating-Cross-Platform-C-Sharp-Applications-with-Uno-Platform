using System.Collections.Generic;
using System.Linq;
using UnoBookRail.Common.Network;

namespace UnoBookRail.Common.Mapping
{
    public static class ImageMap
    {
        public const float Height = 142f;
        public const float Width = 120f;

        public static List<(float X, float Y)> Stations
            => new Stations().GetAll().Select(s => s.MapPosition).ToList();

        public static List<(float X, float Y)> GetStations(Branch branch)
        {
            switch (branch)
            {
                case Branch.MainLine:
                    return new Stations().GetAll()
                                         .Where(s => s.Branch == Branch.MainLine)
                                         .OrderBy(s => s.Id)
                                         .Select(s => s.MapPosition).ToList();
                case Branch.NorthBranch:
                    return new Stations().GetAll()
                                         .Where(s => s.Branch == Branch.NorthBranch
                                                  || s.Id == Network.Stations.InterchangeId)
                                         .OrderBy(s => s.Id)
                                         .Select(s => s.MapPosition).ToList();
                case Branch.SouthBranch:
                    return new Stations().GetAll()
                                         .Where(s => s.Branch == Branch.SouthBranch
                                                  || s.Id == Network.Stations.InterchangeId)
                                         .OrderBy(s => s.Id)
                                         .Select(s => s.MapPosition).ToList();
            }

            return null;
        }

        public static List<TrainInfo> GetTrainsInNetwork()
        {
            // This logic is in the Stations class as it uses much of the scheduling logic already there.
            return new Stations().GetCurrentTrainsInNetwork();
        }
    }
}
