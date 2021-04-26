using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UnoBookRail.Common.Network;

namespace UnoBookRail.Common.Tests
{
    [TestClass]
    public class GetNextArrivalsTests
    {
        [DataTestMethod]
        [DataRow(3, 0, 0, null, "", "", "", "", "", "", 3, "Westbound", "Lakeside", "05:30", "South Point Pier", "05:45", "Lakeside", "06:00")]
        [DataRow(9, 10, 0, null, "", "", "", "", "", "", 3, "Westbound", "South Point Pier", "09:15", "Lakeside", "09:30", "South Point Pier", "09:45")]
        [DataRow(22, 45, 0, null, "", "", "", "", "", "", 1, "Westbound", "Lakeside", "23:00", "", "", "", "")]
        [DataRow(23, 30, 0, null, "", "", "", "", "", "", 0, "Westbound", "", "", "", "", "", "")]
        public void GetNextArrivalsFor_111(int currentHour, int currentMinute, int expectedDestOneCount, string expectedDestOneName, string expDestOneName1, string expDestOneTime1, string expDestOneName2, string expDestOneTime2, string expDestOneName3, string expDestOneTime3, int expectedDestTwoCount, string expectedDestTwoName, string expDestTwoName1, string expDestTwoTime1, string expDestTwoName2, string expDestTwoTime2, string expDestTwoName3, string expDestTwoTime3)
        {
            RunTestsForStation(111, currentHour, currentMinute, expectedDestOneCount, expectedDestOneName, expDestOneName1, expDestOneTime1, expDestOneName2, expDestOneTime2, expDestOneName3, expDestOneTime3, expectedDestTwoCount, expectedDestTwoName, expDestTwoName1, expDestTwoTime1, expDestTwoName2, expDestTwoTime2, expDestTwoName3, expDestTwoTime3);
        }

        [DataTestMethod]
        [DataRow(3, 0, 3, "Eastbound", "Airport (Main Terminal)", "06:24", "Airport (Main Terminal)", "06:36", "Airport (Main Terminal)", "06:54", 3, "Westbound", "Lakeside", "05:33", "South Point Pier", "05:48", "Lakeside", "06:03")]
        [DataRow(9, 10, 3, "Eastbound", "Airport (Main Terminal)", "09:24", "Airport (Main Terminal)", "09:36", "Airport (Main Terminal)", "09:54", 3, "Westbound", "South Point Pier", "09:18", "Lakeside", "09:33", "South Point Pier", "09:48")]
        [DataRow(22, 45, 3, "Eastbound", "Airport (Main Terminal)", "22:54", "Airport (Main Terminal)", "23:06", "Airport (Main Terminal)", "23:24", 2, "Westbound", "South Point Pier", "22:48", "Lakeside", "23:03", "", "")]
        [DataRow(23, 30, 2, "Eastbound", "Airport (Main Terminal)", "23:36", "Airport (Main Terminal)", "23:54", "", "", 0, "Westbound", "", "", "", "", "", "")]
        public void GetNextArrivalsFor_110(int currentHour, int currentMinute, int expectedDestOneCount, string expectedDestOneName, string expDestOneName1, string expDestOneTime1, string expDestOneName2, string expDestOneTime2, string expDestOneName3, string expDestOneTime3, int expectedDestTwoCount, string expectedDestTwoName, string expDestTwoName1, string expDestTwoTime1, string expDestTwoName2, string expDestTwoTime2, string expDestTwoName3, string expDestTwoTime3)
        {
            RunTestsForStation(110, currentHour, currentMinute, expectedDestOneCount, expectedDestOneName, expDestOneName1, expDestOneTime1, expDestOneName2, expDestOneTime2, expDestOneName3, expDestOneTime3, expectedDestTwoCount, expectedDestTwoName, expDestTwoName1, expDestTwoTime1, expDestTwoName2, expDestTwoTime2, expDestTwoName3, expDestTwoTime3);
        }

        [DataTestMethod]
        [DataRow(3, 0, 3, "Eastbound", "Airport (Main Terminal)", "06:08", "Airport (Main Terminal)", "06:20", "Airport (Main Terminal)", "06:38", 3, "Westbound", "Lakeside", "05:49", "South Point Pier", "06:04", "Lakeside", "06:19")]
        [DataRow(9, 10, 3, "Eastbound", "Airport (Main Terminal)", "09:20", "Airport (Main Terminal)", "09:38", "Airport (Main Terminal)", "09:50", 3, "Westbound", "Lakeside", "09:19", "South Point Pier", "09:34", "Lakeside", "09:49")]
        [DataRow(22, 45, 3, "Eastbound", "Airport (Main Terminal)", "22:50", "Airport (Main Terminal)", "23:08", "Airport (Main Terminal)", "23:20", 3, "Westbound", "Lakeside", "22:49", "South Point Pier", "23:04", "Lakeside", "23:19")]
        [DataRow(23, 05, 3, "Eastbound", "Airport (Main Terminal)", "23:08", "Airport (Main Terminal)", "23:20", "Airport (Main Terminal)", "23:38", 1, "Westbound", "Lakeside", "23:19", "", "", "", "")]
        [DataRow(23, 30, 1, "Eastbound", "Airport (Main Terminal)", "23:38", "", "", "", "", 0, "Westbound", "", "", "", "", "", "")]
        public void GetNextArrivalsFor_106(int currentHour, int currentMinute, int expectedDestOneCount, string expectedDestOneName, string expDestOneName1, string expDestOneTime1, string expDestOneName2, string expDestOneTime2, string expDestOneName3, string expDestOneTime3, int expectedDestTwoCount, string expectedDestTwoName, string expDestTwoName1, string expDestTwoTime1, string expDestTwoName2, string expDestTwoTime2, string expDestTwoName3, string expDestTwoTime3)
        {
            RunTestsForStation(106, currentHour, currentMinute, expectedDestOneCount, expectedDestOneName, expDestOneName1, expDestOneTime1, expDestOneName2, expDestOneTime2, expDestOneName3, expDestOneTime3, expectedDestTwoCount, expectedDestTwoName, expDestTwoName1, expDestTwoTime1, expDestTwoName2, expDestTwoTime2, expDestTwoName3, expDestTwoTime3);
        }

        [DataTestMethod]
        [DataRow(3, 0, 3, "Northbound", "Lakeside", "06:18", "Lakeside", "06:48", "Lakeside", "07:18", 3, "Southbound", "Airport (Main Terminal)", "05:39", "Airport (Main Terminal)", "06:09", "Airport (Main Terminal)", "06:39")]
        [DataRow(9, 10, 3, "Northbound", "Lakeside", "09:18", "Lakeside", "09:48", "Lakeside", "10:18", 3, "Southbound", "Airport (Main Terminal)", "09:39", "Airport (Main Terminal)", "10:09", "Airport (Main Terminal)", "10:39")]
        [DataRow(22, 45, 3, "Northbound", "Lakeside", "22:48", "Lakeside", "23:18", "Lakeside", "23:48", 1, "Southbound", "Airport (Main Terminal)", "23:09", "", "", "", "")]
        [DataRow(23, 05, 2, "Northbound", "Lakeside", "23:18", "Lakeside", "23:48", "", "", 1, "Southbound", "Airport (Main Terminal)", "23:09", "", "", "", "")]
        [DataRow(23, 30, 1, "Northbound", "Lakeside", "23:48", "", "", "", "", 0, "Southbound", "", "", "", "", "", "")]
        public void GetNextArrivalsFor_205(int currentHour, int currentMinute, int expectedDestOneCount, string expectedDestOneName, string expDestOneName1, string expDestOneTime1, string expDestOneName2, string expDestOneTime2, string expDestOneName3, string expDestOneTime3, int expectedDestTwoCount, string expectedDestTwoName, string expDestTwoName1, string expDestTwoTime1, string expDestTwoName2, string expDestTwoTime2, string expDestTwoName3, string expDestTwoTime3)
        {
            RunTestsForStation(205, currentHour, currentMinute, expectedDestOneCount, expectedDestOneName, expDestOneName1, expDestOneTime1, expDestOneName2, expDestOneTime2, expDestOneName3, expDestOneTime3, expectedDestTwoCount, expectedDestTwoName, expDestTwoName1, expDestTwoTime1, expDestTwoName2, expDestTwoTime2, expDestTwoName3, expDestTwoTime3);
        }

        [DataTestMethod]
        [DataRow(3, 0, 3, "Northbound", "Airport (Main Terminal)", "05:59", "Airport (Main Terminal)", "06:29", "Airport (Main Terminal)", "06:59", 3, "Southbound", "South Point Pier", "06:26", "South Point Pier", "06:56", "South Point Pier", "07:26")]
        [DataRow(9, 10, 3, "Northbound", "Airport (Main Terminal)", "09:29", "Airport (Main Terminal)", "09:59", "Airport (Main Terminal)", "10:29", 3, "Southbound", "South Point Pier", "09:26", "South Point Pier", "09:56", "South Point Pier", "10:26")]
        [DataRow(22, 45, 1, "Northbound", "Airport (Main Terminal)", "22:59", "", "", "", "", 2, "Southbound", "South Point Pier", "22:56", "South Point Pier", "23:26", "", "")]
        [DataRow(23, 05, 0, "Northbound", "", "", "", "", "", "", 1, "Southbound", "South Point Pier", "23:26", "", "", "", "")]
        [DataRow(23, 30, 0, "Northbound", "", "", "", "", "", "", 0, "Southbound", "", "", "", "", "", "")]
        public void GetNextArrivalsFor_302(int currentHour, int currentMinute, int expectedDestOneCount, string expectedDestOneName, string expDestOneName1, string expDestOneTime1, string expDestOneName2, string expDestOneTime2, string expDestOneName3, string expDestOneTime3, int expectedDestTwoCount, string expectedDestTwoName, string expDestTwoName1, string expDestTwoTime1, string expDestTwoName2, string expDestTwoTime2, string expDestTwoName3, string expDestTwoTime3)
        {
            RunTestsForStation(302, currentHour, currentMinute, expectedDestOneCount, expectedDestOneName, expDestOneName1, expDestOneTime1, expDestOneName2, expDestOneTime2, expDestOneName3, expDestOneTime3, expectedDestTwoCount, expectedDestTwoName, expDestTwoName1, expDestTwoTime1, expDestTwoName2, expDestTwoTime2, expDestTwoName3, expDestTwoTime3);
        }

        private void RunTestsForStation(int stationId, int currentHour, int currentMinute, int expectedDestOneCount, string expectedDestOneName, string expDestOneName1, string expDestOneTime1, string expDestOneName2, string expDestOneTime2, string expDestOneName3, string expDestOneTime3, int expectedDestTwoCount, string expectedDestTwoName, string expDestTwoName1, string expDestTwoTime1, string expDestTwoName2, string expDestTwoTime2, string expDestTwoName3, string expDestTwoTime3)
        {
            var testTime = new TestTime
            {
                Now = new DateTimeOffset(2021, 1, 1, currentHour, currentMinute, 0, TimeSpan.Zero)
            };

            var sut = new Stations(testTime);

            var arrivals = sut.GetNextArrivals(stationId);

            Assert.IsNotNull(arrivals);

            Assert.AreEqual(expectedDestOneName, arrivals.DirectionOneName);

            Assert.AreEqual(expectedDestOneCount, arrivals.DirectionOneDetails.Count);

            if (expectedDestOneCount >= 1)
            {
                Assert.AreEqual(expDestOneName1, arrivals.DirectionOneDetails[0].Destination);
                Assert.AreEqual(expDestOneTime1, arrivals.DirectionOneDetails[0].TimeDue);
            }
            if (expectedDestOneCount >= 2)
            {
                Assert.AreEqual(expDestOneName2, arrivals.DirectionOneDetails[1].Destination);
                Assert.AreEqual(expDestOneTime2, arrivals.DirectionOneDetails[1].TimeDue);
            }
            if (expectedDestOneCount >= 3)
            {
                Assert.AreEqual(expDestOneName3, arrivals.DirectionOneDetails[2].Destination);
                Assert.AreEqual(expDestOneTime3, arrivals.DirectionOneDetails[2].TimeDue);
            }

            Assert.AreEqual(expectedDestTwoName, arrivals.DirectionTwoName);

            Assert.AreEqual(expectedDestTwoCount, arrivals.DirectionTwoDetails.Count);

            if (expectedDestTwoCount >= 1)
            {
                Assert.AreEqual(expDestTwoName1, arrivals.DirectionTwoDetails[0].Destination);
                Assert.AreEqual(expDestTwoTime1, arrivals.DirectionTwoDetails[0].TimeDue);
            }
            if (expectedDestTwoCount >= 2)
            {
                Assert.AreEqual(expDestTwoName2, arrivals.DirectionTwoDetails[1].Destination);
                Assert.AreEqual(expDestTwoTime2, arrivals.DirectionTwoDetails[1].TimeDue);
            }
            if (expectedDestTwoCount >= 3)
            {
                Assert.AreEqual(expDestTwoName3, arrivals.DirectionTwoDetails[2].Destination);
                Assert.AreEqual(expDestTwoTime3, arrivals.DirectionTwoDetails[2].TimeDue);
            }
        }
    }
}
