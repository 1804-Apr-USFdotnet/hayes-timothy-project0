using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using LocalGourmet.BLL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LocalGourmet.BLL.UnitTest
{
    [TestClass]
    public class RestaurantUnitTest
    {
        [TestMethod]
        public void TestRestaurantGetAvgRating()
        {
            // Arrange
            List<Restaurant> restaurants = new List<Restaurant>();
            string json = System.IO.File.ReadAllText(@"C:\revature\" + 
                @"hayes-timothy-project0\LocalGourmet\LocalGourmet.BLL\" +
                @"Configs\RestaurantsForUnitTest.json");
            restaurants = Serializer.Deserialize<List<Restaurant>>(json);
            Restaurant subway = restaurants[0];
            float expectedRating = 3.0f;

            // Act
            float actualRating = subway.GetAvgRating();

            // Assert
            Assert.AreEqual(expectedRating, actualRating);

        }

    }
}
