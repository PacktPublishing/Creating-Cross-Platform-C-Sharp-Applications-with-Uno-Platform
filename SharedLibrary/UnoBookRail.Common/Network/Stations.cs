using System;
using System.Collections.Generic;
using System.Linq;

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
                new Station(101, "Interchange", Branch.MainLine),
                new Station(102, "Main Street", Branch.MainLine),
                new Station(103, "Central Station", Branch.MainLine),
                new Station(104, "Founder Place", Branch.MainLine),
                new Station(105, "Market Street", Branch.MainLine),
                new Station(106, "Memorial Park", Branch.MainLine),
                new Station(107, "Conference Center", Branch.MainLine),
                new Station(108, "Stadium", Branch.MainLine),
                new Station(109, "Green Field", Branch.MainLine),
                new Station(110, "Airport parking", Branch.MainLine),
                new Station(AirportTerminusId, AirportTerminusName, Branch.MainLine, isTerminus: true),

                new Station(201, "Metropolitan Bridge", Branch.NorthBranch),
                new Station(202, "Wagner Street", Branch.NorthBranch),
                new Station(203, "Lacey Boulevard", Branch.NorthBranch),
                new Station(204, "Finney Park", Branch.NorthBranch),
                new Station(205, "Union Square", Branch.NorthBranch),
                new Station(206, "Freedom Road", Branch.NorthBranch),
                new Station(207, "University", Branch.NorthBranch),
                new Station(208, NorthBranchTerminusName, Branch.NorthBranch, isTerminus: true),

                new Station(301, "Waterfront", Branch.SouthBranch),
                new Station(302, "Mendelevich Way", Branch.SouthBranch),
                new Station(303, "Mundy Square", Branch.SouthBranch),
                new Station(304, "Hawker Avenue", Branch.SouthBranch),
                new Station(305, "Ferry Port", Branch.SouthBranch),
                new Station(306, SouthBranchTerminusName, Branch.SouthBranch, isTerminus: true),
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
    }
}
