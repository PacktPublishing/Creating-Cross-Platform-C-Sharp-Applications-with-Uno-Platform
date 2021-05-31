using System;
using System.Collections.Generic;
using System.Linq;
using UnoBookRail.Common.Mapping;

namespace UnoBookRail.Common.Network
{
    public class Stations
    {
        private const int AirportTerminusId = 111;
        private const int NorthBranchTerminusId = 208;
        private const int SouthBranchTerminusId = 306;
        private const string AirportTerminusName = "Airport (Main Terminal)";
        private const string NorthBranchTerminusName = "Lakeside";
        private const string SouthBranchTerminusName = "South Point Pier";

        private readonly ITimeAbstraction _time;

        public static int InterchangeId => 101;

        public Stations(ITimeAbstraction time = null)
        {
            if (time is null)
            {
                _time = new DefaultTimeAbstraction();
            }
            else
            {
                _time = time;
            }
        }

        public List<Station> GetAll()
        {
            return new List<Station>
            {
                new Station(InterchangeId, "Interchange", Branch.MainLine, (17.4f, 83.64f)),
                new Station(102, "Main Street", Branch.MainLine, (24.1f, 84.12f)),
                new Station(103, "Central Station", Branch.MainLine, (33.6f, 84.4f)),
                new Station(104, "Founder Place", Branch.MainLine, (43.1f, 83.9f)),
                new Station(105, "Market Street", Branch.MainLine, (53.1f, 82.0f)),
                new Station(106, "Memorial Park", Branch.MainLine, (63.2f, 78.4f)),
                new Station(107, "Conference Center", Branch.MainLine, (75.1f, 81.5f)),
                new Station(108, "Stadium", Branch.MainLine, (80.3f, 78.9f)),
                new Station(109, "Green Field", Branch.MainLine, (90.8f, 82.7f)),
                new Station(110, "Airport parking", Branch.MainLine, (101.0f, 83.6f)),
                new Station(AirportTerminusId, AirportTerminusName, Branch.MainLine, (109.4f, 81.2f), isTerminus: true),

                new Station(201, "Metropolitan Bridge", Branch.NorthBranch, (11.7f, 75.3f)),
                new Station(202, "Wagner Street", Branch.NorthBranch, (9.0f, 66.7f)),
                new Station(203, "Lacey Boulevard", Branch.NorthBranch, (10.5f, 58.1f)),
                new Station(204, "Finney Park", Branch.NorthBranch, (11.2f, 47.2f)),
                new Station(205, "Union Square", Branch.NorthBranch, (10.3f, 40.3f)),
                new Station(206, "Freedom Road", Branch.NorthBranch, (12.1f, 31.5f)),
                new Station(207, "University", Branch.NorthBranch, (8.6f, 22.4f)),
                new Station(208, NorthBranchTerminusName, Branch.NorthBranch, (10f, 10.25f), isTerminus: true),

                new Station(301, "Waterfront", Branch.SouthBranch, (10.3f, 91.3f)),
                new Station(302, "Mendelevich Way", Branch.SouthBranch, (11.2f, 98.2f)),
                new Station(303, "Mundy Square", Branch.SouthBranch, (11.4f, 107.5f)),
                new Station(304, "Hawker Avenue", Branch.SouthBranch, (8.3f, 113.2f)),
                new Station(305, "Ferry Port", Branch.SouthBranch, (11.0f, 122.3f)),
                new Station(306, SouthBranchTerminusName, Branch.SouthBranch, (8.8f, 128.2f), isTerminus: true),
            };
        }

        public Station GetStation(int stationId)
        {
            return GetAll().FirstOrDefault(s => s.Id == stationId);
        }

        // This contains crude logic to create realistic sample data.
        // It does not include consideration for delays and cancellations.
        public Arrivals GetNextArrivals(int stationId)
        {
            if (!IsValidStationId(stationId))
            {
                throw new ArgumentException("Invalid Station Id", nameof(stationId));
            }

            // between 05:30 and 23:00
            // Trains leave airport every 15 minutes
            //   Alternating destinations (North first)
            // Trains leave Lakeside every 30 minutes (starting 5:30)
            // trains leave south pier every 30 minutes (starting 5:45)

            var response = new Arrivals { ForStationId = stationId };

            var station = GetStation(stationId);

            if (station.Directions == PlatformDirections.EastWest)
            {
                if (!station.IsTerminus)
                {
                    response.DirectionOneName = "Eastbound";

                    response.DirectionOneDetails.AddRange(GetNextArrivalDetails(stationId, CompassDirection.East));
                }

                response.DirectionTwoName = "Westbound";

                response.DirectionTwoDetails.AddRange(GetNextArrivalDetails(stationId, CompassDirection.West));
            }
            else
            {
                if (station.Branch == Branch.NorthBranch)
                {
                    if (!station.IsTerminus)
                    {

                        response.DirectionOneName = "Northbound";

                        response.DirectionOneDetails.AddRange(GetNextArrivalDetails(stationId, CompassDirection.North));
                    }

                    response.DirectionTwoName = "Southbound";

                    response.DirectionTwoDetails.AddRange(GetNextArrivalDetails(stationId, CompassDirection.South));
                }
                else
                {
                    response.DirectionOneName = "Northbound";

                    response.DirectionOneDetails.AddRange(GetNextArrivalDetails(stationId, CompassDirection.North));

                    if (!station.IsTerminus)
                    {
                        response.DirectionTwoName = "Southbound";

                        response.DirectionTwoDetails.AddRange(GetNextArrivalDetails(stationId, CompassDirection.South));
                    }
                }
            }

            return response;
        }

        public int GetDistanceBetweenStations(int startStationId, int endStationId)
        {
            if (!IsValidStationId(startStationId) || !IsValidStationId(endStationId))
            {
                return -1;
            }

            var abs = Math.Abs(startStationId - endStationId);

            if (abs < 11)
            {
                // They're on the same branch
                return abs;
            }

            int distanceFromBranchStart(int stnId)
            {
                var calc = stnId;

                if (calc > 300)
                {
                    calc -= 100;
                }

                if (calc > 200)
                {
                    calc -= 100;
                }

                return calc - 101;
            }

            // Stations on North & South branches will need to connect via the interchange at 101
            var branchChanges = 0;

            if (startStationId > 200)
            {
                branchChanges++;
            }

            if (endStationId > 200)
            {
                branchChanges++;
            }

            return distanceFromBranchStart(startStationId)
                 + distanceFromBranchStart(endStationId)
                 + branchChanges;
        }

        public List<TrainInfo> GetCurrentTrainsInNetwork()
        {
            var results = new List<TrainInfo>();

            var now = _time.GetNow();
            var workingTime = now.AddHours(-1).TimeOfDay;

            var minsPerDeparture = 15;

            var startOfDay = new TimeSpan(5, 30, 0);

            if (now.TimeOfDay >= startOfDay)
            {
                var loopMins = Convert.ToInt32(startOfDay.TotalMinutes);

                // Find the first train departure after the time we're interested in
                while (loopMins < workingTime.TotalMinutes)
                {
                    loopMins += minsPerDeparture;
                }

                while (loopMins <= now.TimeOfDay.TotalMinutes)
                {
                    var timeSinceDeparture = 60 - (loopMins - workingTime.TotalMinutes);

                    if (timeSinceDeparture < 60)
                    {
                        void AddInfoForTrainLeavingTerminus(int terminusId, CompassDirection direction, bool headingNorth = false)
                        {
                            var (left, approaching) = StationsBetweenAfterLeavingTerminus(terminusId, (int)timeSinceDeparture, headingNorth: headingNorth);

                            results.Add(new TrainInfo
                            {
                                Direction = direction,
                                StationLeft = left,
                                StationApproaching = approaching,
                                MapPosition = GetMapPoints(left, approaching, (int)timeSinceDeparture - MinutesFromTerminus(left, terminusId)),
                            });
                        }

                        // Train leaving Airport
                        var timeToTravelMainBranch = MinutesFromTerminus(InterchangeId, AirportTerminusId);

                        var heading = (timeSinceDeparture > timeToTravelMainBranch)
                            ? (loopMins % 30 == 0) ? CompassDirection.North : CompassDirection.South
                            : CompassDirection.West;

                        AddInfoForTrainLeavingTerminus(AirportTerminusId, heading, loopMins % 30 == 0);

                        if (loopMins % 30 == 0)
                        {
                            // Trains leaving lakeside
                            var minsToReachMainBranch = MinutesFromTerminus(InterchangeId, NorthBranchTerminusId);

                            heading = (timeSinceDeparture > minsToReachMainBranch) ? CompassDirection.East : CompassDirection.South;

                            AddInfoForTrainLeavingTerminus(NorthBranchTerminusId, heading);
                        }
                        else
                        {
                            // Trains leaving South Point Pier
                            var minsToReachMainBranch = MinutesFromTerminus(InterchangeId, SouthBranchTerminusId);

                            heading = (timeSinceDeparture > minsToReachMainBranch) ? CompassDirection.East : CompassDirection.North;

                            AddInfoForTrainLeavingTerminus(SouthBranchTerminusId, heading);
                        }
                    }

                    loopMins += minsPerDeparture;
                }
            }

            return results;
        }

        public (float X, float Y) GetMapPoints(int stnLeft, int stnApproaching, int minsSinceLeft)
        {
            var stn1 = GetStation(stnLeft);

            if (stnLeft == stnApproaching)
            {
                return stn1.MapPosition;
            }

            var stn2 = GetStation(stnApproaching);

            var minsBetweenStns = Math.Abs(MinutesFromTerminus(stnLeft, AirportTerminusId) - MinutesFromTerminus(stnApproaching, AirportTerminusId));

            //   [lat1 +(lat2 - lat1) * per, long1 + (long2 - long1) * per];

            var x1 = Math.Min(stn1.MapPosition.X, stn2.MapPosition.X);
            var x2 = Math.Max(stn1.MapPosition.X, stn2.MapPosition.X);

            var y1 = Math.Min(stn1.MapPosition.Y, stn2.MapPosition.Y);
            var y2 = Math.Max(stn1.MapPosition.Y, stn2.MapPosition.Y);

            var perc = (1f / minsBetweenStns * minsSinceLeft);

            var newX = (stn1.MapPosition.X == x1) ? x1 + Math.Abs(x2 - x1) * perc : x2 - Math.Abs(x2 - x1) * perc;
            var newY = (stn1.MapPosition.Y == y1) ? y1 + Math.Abs(y2 - y1) * perc : y2 - Math.Abs(y2 - y1) * perc;

            return (newX, newY);
            //return (x1 + Math.Abs(x2 - x1) * perc,
            //        y1 + Math.Abs(y2 - y1) * perc);
            //return (stn1.MapPosition.X + (stn2.MapPosition.X - stn1.MapPosition.X) + (1f / minsBetweenStns * minsSinceLeft)),
            //        stn1.MapPosition.Y + (stn2.MapPosition.Y - stn1.MapPosition.Y) + (1f / minsBetweenStns * minsSinceLeft));
        }

        private List<ArrivalDetail> GetNextArrivalDetails(int stationId, CompassDirection direction)
        {
            var now = _time.GetNow();

            // As no train runs for longer than an hour,
            // we can go back an hour and look for trains that have started since then.
            var workingTime = now.AddHours(-1).TimeOfDay;

            var station = GetStation(stationId);

            // Allow for the first train of the day on the South Branch starting later than the others.
            var startOfDay = (station.Branch == Branch.SouthBranch)
                ? new TimeSpan(5, 45, 0)
                : new TimeSpan(5, 30, 0);

            // If before trains have started running today, go to when they will start
            if (workingTime < startOfDay)
            {
                workingTime = startOfDay;
            }

            switch (direction)
            {
                case CompassDirection.East:
                    return GetNextEastboundArrivalDetails(station, now, workingTime, startOfDay);

                case CompassDirection.West:
                    return GetNextWestboundArrivalDetails(station, now, workingTime, startOfDay);

                case CompassDirection.North:
                    return GetNextNorthboundArrivalDetails(station, now, workingTime, startOfDay);

                case CompassDirection.South:
                    return GetNextSouthboundArrivalDetails(station, now, workingTime, startOfDay);
            }

            return null;
        }

        private List<ArrivalDetail> GetNextEastboundArrivalDetails(Station station, DateTimeOffset now, TimeSpan workingTime, TimeSpan startOfDay)
        {
            return GetNextArrivalDetailsLogic(station, now, workingTime, startOfDay, 15, null, AirportTerminusName);
        }

        private List<ArrivalDetail> GetNextWestboundArrivalDetails(Station station, DateTimeOffset now, TimeSpan workingTime, TimeSpan startOfDay)
        {
            return GetNextArrivalDetailsLogic(station, now, workingTime, startOfDay, 15, AirportTerminusId, null);
        }

        private List<ArrivalDetail> GetNextNorthboundArrivalDetails(Station station, DateTimeOffset now, TimeSpan workingTime, TimeSpan startOfDay)
        {
            var fromTerminus = station.Branch == Branch.NorthBranch ? AirportTerminusId : SouthBranchTerminusId;

            var destination = station.Branch == Branch.NorthBranch ? NorthBranchTerminusName : AirportTerminusName;

            return GetNextArrivalDetailsLogic(station, now, workingTime, startOfDay, 30, fromTerminus, destination);
        }

        private List<ArrivalDetail> GetNextSouthboundArrivalDetails(Station station, DateTimeOffset now, TimeSpan workingTime, TimeSpan startOfDay)
        {
            var fromTerminus = station.Branch == Branch.SouthBranch ? AirportTerminusId : NorthBranchTerminusId;

            var destination = station.Branch == Branch.SouthBranch ? SouthBranchTerminusName : AirportTerminusName;

            return GetNextArrivalDetailsLogic(station, now, workingTime, startOfDay, 30, fromTerminus, destination);
        }

        private List<ArrivalDetail> GetNextArrivalDetailsLogic(Station station, DateTimeOffset now, TimeSpan workingTime, TimeSpan startOfDay, int minsBetweenTrains, int? sourceStationId, string destStationName)
        {
            var response = new List<ArrivalDetail>();

            var timeToHere = MinutesFromTerminus(station.Id, sourceStationId ?? NorthBranchTerminusId);

            var loopMins = Convert.ToInt32(startOfDay.TotalMinutes);

            var keepGoing = true;

            var destStnNameTemp = destStationName;

            // Find the first train departure after the time we're interested in
            while (loopMins + timeToHere < workingTime.TotalMinutes)
            {
                loopMins += minsBetweenTrains;
            }

            while (keepGoing)
            {
                // If will arrive here in the future
                if (loopMins + timeToHere > now.TimeOfDay.TotalMinutes)
                {
                    var whenHereMins = loopMins + timeToHere;

                    if (sourceStationId == null)
                    {
                        // Trains could have started from the north or south branch
                        // Ones from the north leave on the hour or half past
                        whenHereMins = loopMins % 30 == 0
                                     ? loopMins + MinutesFromTerminus(station.Id, NorthBranchTerminusId)
                                     : loopMins + MinutesFromTerminus(station.Id, SouthBranchTerminusId);
                    }

                    if (destStationName == null)
                    {
                        // If going away from the Airport, determine the destination based on when departed.
                        destStnNameTemp = loopMins % 30 == 0 ? NorthBranchTerminusName : SouthBranchTerminusName;
                    }

                    response.Add(
                        new ArrivalDetail(
                            destStnNameTemp,
                            $"{whenHereMins / 60:00}:{whenHereMins % 60:00}",
                            whenHereMins - Convert.ToInt32(now.TimeOfDay.TotalMinutes)));
                }

                loopMins += minsBetweenTrains;

                if (response.Count >= 3)
                {
                    // We've got the next three arrivals
                    keepGoing = false;
                }

                if (loopMins > (23 * 60))
                {
                    // Reached the end of the day, so no more trains will arrive
                    keepGoing = false;
                }
            }

            return response;
        }

        internal bool IsValidStationId(int stationId)
        {
            return GetAll().Any(s => s.Id == stationId);
        }

        internal int MinutesFromTerminus(int stationId, int terminusId)
        {
            switch (terminusId)
            {
                case 111:
                    switch (stationId)
                    {
                        case 101: return 34;
                        case 102: return 32;
                        case 103: return 28;
                        case 104: return 26;
                        case 105: return 23;
                        case 106: return 19;
                        case 107: return 16;
                        case 108: return 12;
                        case 109: return 8;
                        case 110: return 3;
                        case 111: return 0;
                        case 201: return 37;
                        case 202: return 39;
                        case 203: return 42;
                        case 204: return 44;
                        case 205: return 48;
                        case 206: return 51;
                        case 207: return 53;
                        case 208: return 57;
                        case 301: return 38;
                        case 302: return 41;
                        case 303: return 44;
                        case 304: return 47;
                        case 305: return 50;
                        case 306: return 54;
                        default:
                            return -1;
                    }

                case 208:
                    switch (stationId)
                    {
                        case 101: return 24;
                        case 102: return 26;
                        case 103: return 29;
                        case 104: return 31;
                        case 105: return 34;
                        case 106: return 38;
                        case 107: return 41;
                        case 108: return 45;
                        case 109: return 49;
                        case 110: return 54;
                        case 111: return 57;
                        case 201: return 20;
                        case 202: return 18;
                        case 203: return 15;
                        case 204: return 12;
                        case 205: return 9;
                        case 206: return 7;
                        case 207: return 3;
                        case 208: return 0;
                        default:
                            return -1;
                    }

                case 306:
                    switch (stationId)
                    {
                        case 101: return 21;
                        case 102: return 23;
                        case 103: return 26;
                        case 104: return 28;
                        case 105: return 31;
                        case 106: return 35;
                        case 107: return 38;
                        case 108: return 42;
                        case 109: return 46;
                        case 110: return 51;
                        case 111: return 54;
                        case 301: return 17;
                        case 302: return 14;
                        case 303: return 11;
                        case 304: return 8;
                        case 305: return 4;
                        case 306: return 0;
                        default:
                            return -1;
                    }

                default:
                    return -1;
            }
        }

        internal (int left, int approaching) StationsBetweenAfterLeavingTerminus(int terminusId, int minutes, bool headingNorth = false)
        {
            switch (terminusId)
            {
                case AirportTerminusId:
                    if (headingNorth)
                    {
                        if (minutes >= 57) return (208, 208);
                        if (minutes > 53) return (207, 208);
                        if (minutes == 53) return (207, 207);
                        if (minutes > 51) return (206, 207);
                        if (minutes == 51) return (206, 206);
                        if (minutes > 48) return (205, 206);
                        if (minutes == 48) return (205, 205);
                        if (minutes > 44) return (204, 205);
                        if (minutes == 44) return (204, 204);
                        if (minutes > 42) return (203, 204);
                        if (minutes == 42) return (203, 203);
                        if (minutes > 39) return (202, 203);
                        if (minutes == 39) return (202, 202);
                        if (minutes > 37) return (201, 202);
                        if (minutes == 37) return (201, 201);
                        if (minutes > 34) return (101, 201);
                    }
                    else
                    {
                        if (minutes >= 54) return (306, 306);
                        if (minutes > 50) return (305, 306);
                        if (minutes == 50) return (305, 305);
                        if (minutes > 47) return (304, 305);
                        if (minutes == 47) return (304, 304);
                        if (minutes > 44) return (303, 304);
                        if (minutes == 44) return (303, 303);
                        if (minutes > 41) return (302, 303);
                        if (minutes == 41) return (302, 302);
                        if (minutes > 41) return (302, 303);
                        if (minutes == 41) return (302, 302);
                        if (minutes > 38) return (301, 302);
                        if (minutes == 38) return (301, 301);
                        if (minutes > 34) return (101, 301);
                    }

                    if (minutes > 32) return (102, 101);
                    if (minutes == 32) return (102, 102);
                    if (minutes > 28) return (103, 102);
                    if (minutes == 28) return (103, 103);
                    if (minutes > 26) return (104, 103);
                    if (minutes == 26) return (104, 104);
                    if (minutes > 23) return (105, 104);
                    if (minutes == 23) return (105, 105);
                    if (minutes > 19) return (106, 105);
                    if (minutes == 19) return (106, 106);
                    if (minutes > 16) return (107, 106);
                    if (minutes == 16) return (107, 107);
                    if (minutes > 12) return (108, 107);
                    if (minutes == 12) return (108, 108);
                    if (minutes > 8) return (109, 108);
                    if (minutes == 8) return (109, 109);
                    if (minutes > 3) return (110, 109);
                    if (minutes == 3) return (110, 110);
                    if (minutes > 0) return (111, 110);
                    if (minutes == 0) return (111, 111);

                    break;
                case NorthBranchTerminusId:

                    if (minutes >= 57) return (111, 111);
                    if (minutes > 54) return (110, 111);
                    if (minutes == 54) return (110, 110);
                    if (minutes > 49) return (109, 110);
                    if (minutes == 49) return (109, 109);
                    if (minutes > 45) return (108, 109);
                    if (minutes == 45) return (108, 108);
                    if (minutes > 41) return (107, 108);
                    if (minutes == 41) return (107, 107);
                    if (minutes > 38) return (106, 107);
                    if (minutes == 38) return (106, 106);
                    if (minutes > 34) return (105, 106);
                    if (minutes == 34) return (105, 105);
                    if (minutes > 31) return (104, 105);
                    if (minutes == 31) return (104, 104);
                    if (minutes > 29) return (103, 104);
                    if (minutes == 29) return (103, 103);
                    if (minutes > 26) return (102, 103);
                    if (minutes == 26) return (102, 102);
                    if (minutes > 24) return (101, 102);
                    if (minutes == 24) return (101, 101);
                    if (minutes > 20) return (201, 101);
                    if (minutes == 20) return (201, 201);
                    if (minutes > 18) return (202, 201);
                    if (minutes == 18) return (202, 202);
                    if (minutes > 15) return (203, 202);
                    if (minutes == 15) return (203, 203);
                    if (minutes > 12) return (204, 203);
                    if (minutes == 12) return (204, 204);
                    if (minutes > 9) return (205, 204);
                    if (minutes == 9) return (205, 205);
                    if (minutes > 7) return (206, 205);
                    if (minutes == 7) return (206, 206);
                    if (minutes > 3) return (207, 206);
                    if (minutes == 3) return (207, 207);
                    if (minutes > 0) return (208, 207);
                    if (minutes == 0) return (208, 208);

                    break;
                case SouthBranchTerminusId:

                    if (minutes >= 54) return (111, 111);
                    if (minutes > 51) return (110, 111);
                    if (minutes == 51) return (110, 110);
                    if (minutes > 46) return (109, 110);
                    if (minutes == 46) return (109, 109);
                    if (minutes > 42) return (108, 109);
                    if (minutes == 42) return (108, 108);
                    if (minutes > 38) return (107, 108);
                    if (minutes == 38) return (107, 107);
                    if (minutes > 35) return (106, 107);
                    if (minutes == 35) return (106, 106);
                    if (minutes > 31) return (105, 106);
                    if (minutes == 31) return (105, 105);
                    if (minutes > 28) return (104, 105);
                    if (minutes == 28) return (104, 104);
                    if (minutes > 26) return (103, 104);
                    if (minutes == 26) return (103, 103);
                    if (minutes > 23) return (102, 103);
                    if (minutes == 23) return (102, 102);
                    if (minutes > 21) return (101, 102);
                    if (minutes == 21) return (101, 101);
                    if (minutes > 17) return (301, 101);
                    if (minutes == 17) return (301, 301);
                    if (minutes > 14) return (302, 301);
                    if (minutes == 14) return (302, 302);
                    if (minutes > 11) return (303, 302);
                    if (minutes == 11) return (303, 303);
                    if (minutes > 8) return (304, 303);
                    if (minutes == 8) return (304, 304);
                    if (minutes > 4) return (305, 304);
                    if (minutes == 4) return (305, 305);
                    if (minutes > 0) return (306, 305);
                    if (minutes == 0) return (306, 306);

                    break;
                default:
                    return (-1, -1);
            }

            return (-1, -1);
        }
    }
}
