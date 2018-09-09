using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalvageIt.Models;

namespace SalvageIt.UnitTest.Models
{
    [TestClass]
    public class LocationCoordinatesTest
    {
        [TestMethod]
        public void Equality_Equal()
        {
            double a_lat = 12.32;
            double a_long = 5.22;
            LocationCoordinates a = new LocationCoordinates(a_lat, a_long);
            LocationCoordinates same_as_a = new LocationCoordinates(a_lat, a_long);

            Assert.IsTrue(a == same_as_a);
            Assert.IsTrue(a.Equals(same_as_a));
            Assert.IsFalse(a != same_as_a);
        }

        [TestMethod]
        public void Equality_OneIsNull()
        {
            double a_lat = 12.32;
            double a_long = 5.22;
            LocationCoordinates a = new LocationCoordinates(a_lat, a_long);
            LocationCoordinates b = null;

            Assert.IsFalse(a == b);
            Assert.IsFalse(a.Equals(b));
            Assert.IsTrue(a != b);
        }


        [TestMethod]
        public void Equality_BothNull()
        {
            LocationCoordinates a = null;
            LocationCoordinates b = null;

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);
        }

        [TestMethod]
        public void Equality_LatitudeDifferent()
        {
            double a_lat = 12.32;
            double a_long = 5.22;
            LocationCoordinates a = new LocationCoordinates(a_lat, a_long);
            LocationCoordinates b = new LocationCoordinates(-12.32, a_long);

            Assert.IsTrue(a != b);
        }

        [TestMethod]
        public void Equality_LongitudeDifferent()
        {
            double a_lat = 12.32;
            double a_long = 5.22;
            LocationCoordinates a = new LocationCoordinates(a_lat, a_long);
            LocationCoordinates b = new LocationCoordinates(a_lat, -5.22);

            Assert.IsFalse(a == b);
        }
    }
}
