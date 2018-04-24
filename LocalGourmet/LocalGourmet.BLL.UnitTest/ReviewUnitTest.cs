using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using LocalGourmet.BLL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LocalGourmet.BLL.UnitTest
{
    [TestClass]
    public class ReviewUnitTest
    {
        [TestMethod]
        public void TestGetRating()
        {
            // Arrange
            Review r = new Review("John Smith", "Yuck! Nice location though.");
            r.ServiceRating = 3;
            r.PriceRating = 3;
            r.FoodRating = 0;
            r.AtmosphereRating = 4;

            // Act
            float e1 = 2.5f;
            float a1 = r.GetRating();

            // Assert
            Assert.AreEqual(e1, a1);
        }
    }
}
