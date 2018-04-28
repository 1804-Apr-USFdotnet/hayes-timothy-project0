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

        [TestMethod]
        public void TestGetTop3()
        {
            // Arrange
            List<Restaurant> restaurants = new List<Restaurant>();
            string json = System.IO.File.ReadAllText(@"C:\revature\" + 
                @"hayes-timothy-project0\LocalGourmet\LocalGourmet.BLL\" +
                @"Configs\RestaurantsForUnitTest2.json");
            restaurants = Serializer.Deserialize<List<Restaurant>>(json);

            // Act
            List<Restaurant> expected = new List<Restaurant>();
            expected.Add(restaurants[1]);
            expected.Add(restaurants[2]);
            expected.Add(restaurants[7]);
            List<Restaurant> actual = Restaurant.GetTop3(Restaurant.GetAll());

            // Assert
            Assert.AreEqual(expected[0].ToString(), actual[0].ToString());
            Assert.AreEqual(expected[1].ToString(), actual[1].ToString());
            Assert.AreEqual(expected[2].ToString(), actual[2].ToString());
        }

        [TestMethod]
        public void TestSearchByName()
        {
            // Arrange
            List<Restaurant> restaurants = new List<Restaurant>();
            string json = System.IO.File.ReadAllText(@"C:\revature\" + 
                @"hayes-timothy-project0\LocalGourmet\LocalGourmet.BLL\" +
                @"Configs\RestaurantsForUnitTest2.json");
            restaurants = Serializer.Deserialize<List<Restaurant>>(json);

            // Act
            string s1 = "sub";
            List<Restaurant> a1 = Restaurant.SearchByName(Restaurant.GetAll(), s1);

            string s2 = "CO";
            List<Restaurant> a2 = Restaurant.SearchByName(Restaurant.GetAll(), s2);

            // Assert
            Assert.AreEqual("Subway", a1[0].Name);
            Assert.AreEqual(1, a1.Count);

            Assert.AreEqual("Three Coins Diner", a2[0].Name);
            Assert.AreEqual("Tampa Bay Brewing Company", a2[1].Name);
            Assert.AreEqual("Columbia Restaurant", a2[2].Name);
            Assert.AreEqual(3, a2.Count);
        }

    }
}
