using System;
using System.Collections.Generic;
using LocalGourmet.BLL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LocalGourmet.BLL.UnitTest
{
    [TestClass]
    public class RestaurantUnitTest
    {
        [TestMethod]
        public void TestSerialization()
        {
            Restaurant r = new Restaurant();
            r.Name = "Subway";
            Restaurant r2 = new Restaurant();
            r2.Name = "Dominos";
            Restaurant r3 = new Restaurant();
            r3.Name = "Italianos";
            List<Restaurant> rl = new List<Restaurant>();
            rl.Add(r);
            rl.Add(r2);
            rl.Add(r3);
            Assert.AreEqual("x", Serializer.Serialize(rl));
        }

        [TestMethod]
        public void TestDeserialization()
        {

        }
    }
}
