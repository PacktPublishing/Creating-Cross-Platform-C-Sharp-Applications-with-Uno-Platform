using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UnoBookRail.Common.Network;

namespace UnoBookRail.Common.Tests
{
    [TestClass]
    public class GetMapPointsTests
    {
        [DataTestMethod]
        [DataRow(111, 111, 0, 109.4f, 81.2f)]
        [DataRow(111, 110, 1, 106.6f, 82.0f)]
        [DataRow(111, 110, 2, 103.8f, 82.8f)]
        [DataRow(110, 110, 0, 101.0f, 83.6f)]
        [DataRow(110, 111, 1, 103.8f, 82.8f)]
        [DataRow(110, 111, 2, 106.6f, 82.0f)]
        [DataRow(107, 108, 1, 76.4f, 80.9f)]
        [DataRow(108, 107, 1, 79.0f, 79.6f)]
        [DataRow(207, 208, 1, 8.9f, 19.4f)]
        [DataRow(301, 302, 1, 10.6f, 93.6f)]
        [DataRow(105, 104, 1, 49.8f, 82.6f)]
        [DataRow(101, 102, 1, 20.8f, 83.9f)]
        [DataRow(109, 108, 1, 88.2f, 81.8f)]
        [DataRow(303, 304, 1, 10.4f, 109.4f)]
        [DataRow(201, 101, 1, 13.6f, 78.1f)]
        public void GetCurrentTrains(int stn1, int stn2, int minsSinceLeft, float expectedX, float expectedY)
        {
            var sut = new Stations();

            var actual = sut.GetMapPoints(stn1, stn2, minsSinceLeft);

            Assert.IsTrue(AreFloatsEqual(expectedX, actual.X));
            Assert.IsTrue(AreFloatsEqual(expectedY, actual.Y));
        }

        private static bool AreFloatsEqual(float f1, float f2)
        {
            // Allow for tollerance of up to 0.1
            return Math.Abs(f1 - f2) < 0.1;
        }
    }
}
