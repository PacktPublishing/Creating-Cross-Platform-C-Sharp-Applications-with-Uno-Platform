using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UnoBookRail.Common.Network;

namespace UnoBookRail.Common.Tests
{
    [TestClass]
    public class GetCurrentTrainsInNetworkTests
    {
        [DataTestMethod]
        [DataRow(5, 0, 0, CompassDirection.West, -1, -1, CompassDirection.East, -1, -1, CompassDirection.East, -1, -1, CompassDirection.East, -1, -1, CompassDirection.East, -1, -1, CompassDirection.East, -1, -1, CompassDirection.East, -1, -1, CompassDirection.East, -1, -1)]
        [DataRow(5, 30, 2, CompassDirection.West, 111, 111, CompassDirection.South, 208, 208, CompassDirection.East, -1, -1, CompassDirection.East, -1, -1, CompassDirection.East, -1, -1, CompassDirection.East, -1, -1, CompassDirection.East, -1, -1, CompassDirection.East, -1, -1)]
        [DataRow(5, 31, 2, CompassDirection.West, 111, 110, CompassDirection.South, 208, 207, CompassDirection.East, -1, -1, CompassDirection.East, -1, -1, CompassDirection.East, -1, -1, CompassDirection.East, -1, -1, CompassDirection.East, -1, -1, CompassDirection.East, -1, -1)]
        [DataRow(11, 59, 8, CompassDirection.North, 208, 208, CompassDirection.East, 111, 111, CompassDirection.South, 303, 303, CompassDirection.East, 108, 109, CompassDirection.West, 103, 102, CompassDirection.East, 103, 103, CompassDirection.West, 108, 107, CompassDirection.North, 302, 302)]
        [DataRow(12, 0, 8, CompassDirection.South, 303, 304, CompassDirection.East, 108, 109, CompassDirection.West, 103, 102, CompassDirection.East, 103, 104, CompassDirection.West, 108, 107, CompassDirection.North, 302, 301, CompassDirection.West, 111, 111, CompassDirection.South, 208, 208)]
        [DataRow(12, 1, 8, CompassDirection.South, 303, 304, CompassDirection.East, 109, 109, CompassDirection.West, 103, 102, CompassDirection.East, 104, 104, CompassDirection.West, 107, 107, CompassDirection.North, 302, 301, CompassDirection.West, 111, 110, CompassDirection.South, 208, 207)]
        public void GetCurrentTrains(int currentHour, int currentMinute, int expectedTrainsInNetwork, CompassDirection train1Dir, int train1Left, int train1Approach, CompassDirection train2Dir, int train2Left, int train2Approach, CompassDirection train3Dir, int train3Left, int train3Approach, CompassDirection train4Dir, int train4Left, int train4Approach, CompassDirection train5Dir, int train5Left, int train5Approach, CompassDirection train6Dir, int train6Left, int train6Approach, CompassDirection train7Dir, int train7Left, int train7Approach, CompassDirection train8Dir, int train8Left, int train8Approach)
        {
            RunTest(
                currentHour, currentMinute,
                expectedTrainsInNetwork,
                train1Dir, train1Left, train1Approach,
                train2Dir, train2Left, train2Approach,
                train3Dir, train3Left, train3Approach,
                train4Dir, train4Left, train4Approach,
                train5Dir, train5Left, train5Approach,
                train6Dir, train6Left, train6Approach,
                train7Dir, train7Left, train7Approach,
                train8Dir, train8Left, train8Approach);
        }

        private void RunTest(int currentHour, int currentMinute, int expectedTrainsInNetwork, CompassDirection train1Dir, int train1Left, int train1Approach, CompassDirection train2Dir, int train2Left, int train2Approach, CompassDirection train3Dir, int train3Left, int train3Approach, CompassDirection train4Dir, int train4Left, int train4Approach, CompassDirection train5Dir, int train5Left, int train5Approach, CompassDirection train6Dir, int train6Left, int train6Approach, CompassDirection train7Dir, int train7Left, int train7Approach, CompassDirection train8Dir, int train8Left, int train8Approach)
        {
            var testTime = new TestTime
            {
                Now = new DateTimeOffset(2021, 1, 1, currentHour, currentMinute, 0, TimeSpan.Zero)
            };

            var sut = new Stations(testTime);

            var trains = sut.GetCurrentTrainsInNetwork();

            Assert.AreEqual(expectedTrainsInNetwork, trains.Count);

            if (expectedTrainsInNetwork > 0)
            {
                Assert.AreEqual(train1Dir, trains[0].Direction);
                Assert.AreEqual(train1Left, trains[0].StationLeft);
                Assert.AreEqual(train1Approach, trains[0].StationApproaching);
            }
            if (expectedTrainsInNetwork > 1)
            {
                Assert.AreEqual(train2Dir, trains[1].Direction);
                Assert.AreEqual(train2Left, trains[1].StationLeft);
                Assert.AreEqual(train2Approach, trains[1].StationApproaching);
            }
            if (expectedTrainsInNetwork > 2)
            {
                Assert.AreEqual(train3Dir, trains[2].Direction);
                Assert.AreEqual(train3Left, trains[2].StationLeft);
                Assert.AreEqual(train3Approach, trains[2].StationApproaching);
            }
            if (expectedTrainsInNetwork > 3)
            {
                Assert.AreEqual(train4Dir, trains[3].Direction);
                Assert.AreEqual(train4Left, trains[3].StationLeft);
                Assert.AreEqual(train4Approach, trains[3].StationApproaching);
            }
            if (expectedTrainsInNetwork > 4)
            {
                Assert.AreEqual(train5Dir, trains[4].Direction);
                Assert.AreEqual(train5Left, trains[4].StationLeft);
                Assert.AreEqual(train5Approach, trains[4].StationApproaching);
            }
            if (expectedTrainsInNetwork > 5)
            {
                Assert.AreEqual(train6Dir, trains[5].Direction);
                Assert.AreEqual(train6Left, trains[5].StationLeft);
                Assert.AreEqual(train6Approach, trains[5].StationApproaching);
            }
            if (expectedTrainsInNetwork > 6)
            {
                Assert.AreEqual(train7Dir, trains[6].Direction);
                Assert.AreEqual(train7Left, trains[6].StationLeft);
                Assert.AreEqual(train7Approach, trains[6].StationApproaching);
            }
            if (expectedTrainsInNetwork > 7)
            {
                Assert.AreEqual(train8Dir, trains[7].Direction);
                Assert.AreEqual(train8Left, trains[7].StationLeft);
                Assert.AreEqual(train8Approach, trains[7].StationApproaching);
            }
        }
    }
}
