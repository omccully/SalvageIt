using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalvageIt.Services;

namespace SalvageIt.UnitTest.Services
{
    [TestClass]
    public class HelpersTest
    {
        [TestMethod]
        public void ToUserFriendlyString()
        {
            // days, hours, mins, secs
            Assert.AreEqual("543 days",
                new TimeSpan(543, 4, 3, 2).ToUserFriendlyString());

            // days, hours, mins, secs
            Assert.AreEqual("5 days",
                new TimeSpan(5, 4, 3, 2).ToUserFriendlyString());

            // hours, mins, secs
            Assert.AreEqual("3 hours",
                new TimeSpan(3, 20, 30).ToUserFriendlyString());

            // hours, mins, secs
            Assert.AreEqual("23 minutes",
                new TimeSpan(0, 23, 11).ToUserFriendlyString());
        }

    }
}
