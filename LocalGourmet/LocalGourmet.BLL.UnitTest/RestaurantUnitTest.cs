using System;
using System.IO;
using Newtonsoft.Json;
using LocalGourmet.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LocalGourmet.BLL.Models;

namespace LocalGourmet.BLL.UnitTest
{
    [TestClass]
    public class RestaurantUnitTest
    {
        [TestMethod]
        public void TestJSONData()
        {
            // Arrange
            // read file into a string and deserialize JSON to a type
            Restaurant[] restaurants = JsonConvert.DeserializeObject<Restaurant[]>(File.ReadAllText(@"C:\revature\hayes-timothy-project0\LocalGourmet\LocalGourmet.BLL\Restaurants.json"));

            // Act

            // Assert
        }
    }
}
