using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnoBookRail.Common.Network;

namespace UnoBookRail.Common.Tests
{
    [TestClass]
    public class GetDistanceBetweenStationsTests
    {
        private readonly Stations _stations;

        public GetDistanceBetweenStationsTests()
        {
            _stations = new Stations();
        }

        [DataTestMethod]
        [DataRow(101, 102, 1)]
        [DataRow(101, 111, 10)]
        [DataRow(101, 201, 1)]
        [DataRow(101, 208, 8)]
        [DataRow(101, 301, 1)]
        [DataRow(101, 306, 6)]
        public void GetDistances_StartingFrom_101(int startStationId, int endStationId, int expectedDistance)
        {
            var actual = _stations.GetDistanceBetweenStations(startStationId, endStationId);

            Assert.AreEqual(expectedDistance, actual);
        }

        [DataTestMethod]
        [DataRow(111, 110, 1)]
        [DataRow(111, 102, 9)]
        [DataRow(111, 201, 11)]
        [DataRow(111, 208, 18)]
        [DataRow(111, 301, 11)]
        [DataRow(111, 306, 16)]
        public void GetDistances_StartingFrom_111(int startStationId, int endStationId, int expectedDistance)
        {
            var actual = _stations.GetDistanceBetweenStations(startStationId, endStationId);

            Assert.AreEqual(expectedDistance, actual);
        }

        [DataTestMethod]
        [DataRow(201, 110, 10)]
        [DataRow(201, 102, 2)]
        [DataRow(201, 208, 7)]
        [DataRow(201, 301, 2)]
        [DataRow(201, 306, 7)]
        public void GetDistances_StartingFrom_201(int startStationId, int endStationId, int expectedDistance)
        {
            var actual = _stations.GetDistanceBetweenStations(startStationId, endStationId);

            Assert.AreEqual(expectedDistance, actual);
        }

        [DataTestMethod]
        [DataRow(208, 110, 17)]
        [DataRow(208, 101, 8)]
        [DataRow(208, 102, 9)]
        [DataRow(208, 203, 5)]
        [DataRow(208, 301, 9)]
        [DataRow(208, 306, 14)]
        public void GetDistances_StartingFrom_208(int startStationId, int endStationId, int expectedDistance)
        {
            var actual = _stations.GetDistanceBetweenStations(startStationId, endStationId);

            Assert.AreEqual(expectedDistance, actual);
        }

        [DataTestMethod]
        [DataRow(301, 110, 10)]
        [DataRow(301, 102, 2)]
        [DataRow(301, 208, 9)]
        [DataRow(301, 303, 2)]
        [DataRow(301, 306, 5)]
        public void GetDistances_StartingFrom_301(int startStationId, int endStationId, int expectedDistance)
        {
            var actual = _stations.GetDistanceBetweenStations(startStationId, endStationId);

            Assert.AreEqual(expectedDistance, actual);
        }

        [DataTestMethod]
        [DataRow(306, 110, 15)]
        [DataRow(306, 101, 6)]
        [DataRow(306, 102, 7)]
        [DataRow(306, 203, 9)]
        [DataRow(306, 301, 5)]
        [DataRow(306, 305, 1)]
        public void GetDistances_StartingFrom_306(int startStationId, int endStationId, int expectedDistance)
        {
            var actual = _stations.GetDistanceBetweenStations(startStationId, endStationId);

            Assert.AreEqual(expectedDistance, actual);
        }
    }
}
