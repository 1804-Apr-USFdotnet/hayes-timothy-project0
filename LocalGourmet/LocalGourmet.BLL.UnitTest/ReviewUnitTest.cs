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

            // 1000 brought down to max of 5
            Review r2 = new Review("X", "Great!", 1000);

            // 0 + 3 + 3 + 3 = 9....9/4 = 2.25
            Review r3 = new Review("James", "");
            r3.FoodRating = -4;

            // 0 + 2 + 4 + 5 = 11.....11/4= 2.75
            Review r4 = new Review("x", "a", -100, 2, 4, 5);

            // Act
            float e1 = 2.5f;
            float a1 = r.GetRating();

            float e2 = 5f;
            float a2 = r2.GetRating();

            float e3 = 2.25f;
            float a3 = r3.GetRating();

            float e4 = 2.75f;
            float a4 = r4.GetRating();

            // Assert
            Assert.AreEqual(e1, a1);
            Assert.AreEqual(e2, a2);
            Assert.AreEqual(e3, a3);
            Assert.AreEqual(e4, a4);
        }
    }
}
