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
        public void TestSerialization()
        {
            Restaurant r = new Restaurant();
            r.Name = "Subway";
            r.Location = "14961 N Florida Ave, Suite 14961, Florida " +
                "Crossing Shopping Cntr, Tampa, FL 33613, USA";
            r.Cuisine = "American";
            r.PhoneNumber = "(813) 963-2670";
            r.WebAddress = "www.subway.com";
            r.Type = "Fast Food";
            r.Hours = "Mon-Sat 8am-10pm, Sun 9am-9pm";

            Restaurant r2 = new Restaurant();
            r2.Name = "Villa Gallace";
            r2.Location = "109 Gulf Blvd, Indian Rocks Beach, FL 33785, USA";
            r2.Cuisine = "Italian";
            r2.PhoneNumber = "(727) 596-0200";
            r2.WebAddress = "www.villagallace.com";
            r2.Type = "Fine Restaurant";
            r2.Hours = "Mon-Sat 5pm-10:30pm, Sun 4pm-10pm";

            Restaurant r3 = new Restaurant();
            r3.Name = "Three Coins Diner";
            r3.Location = "7410 N Nebraska Ave, Tampa, FL 33604, USA";
            r3.Cuisine = "American";
            r3.PhoneNumber = "(813) 239-1256";
            r3.WebAddress = "threecoinsdiner.net";
            r3.Type = "Diner";
            r3.Hours = "Mon-Sun 12am-11:30pm";

            string jsonStr = System.IO.File.ReadAllText(@"C:\revature\" + 
                @"hayes-timothy-project0\LocalGourmet\LocalGourmet.BLL\" +
                @"Models\Restaurants.json");

            List<Restaurant> rl = new List<Restaurant>();
            rl.Add(r);
            rl.Add(r2);
            rl.Add(r3);
            string jsonStrNoWhitespace = Regex.Replace(jsonStr, @"\s+", "");
            string serializedObj = Serializer.Serialize(rl);
            string serializedObjNoWhitespace = Regex.Replace(serializedObj, @"\s+", "");
            Assert.AreEqual(jsonStrNoWhitespace, serializedObjNoWhitespace);
        }

        [TestMethod]
        public void TestDeserialization()
        {
            Restaurant r = new Restaurant();
            r.Name = "Subway";
            r.Location = "14961 N Florida Ave, Suite 14961, Florida " +
                "Crossing Shopping Cntr, Tampa, FL 33613, USA";
            r.Cuisine = "American";
            r.PhoneNumber = "(813) 963-2670";
            r.WebAddress = "www.subway.com";
            r.Type = "Fast Food";
            r.Hours = "Mon-Sat 8am-10pm, Sun 9am-9pm";

            Restaurant r2 = new Restaurant();
            r2.Name = "Villa Gallace";
            r2.Location = "109 Gulf Blvd, Indian Rocks Beach, FL 33785, USA";
            r2.Cuisine = "Italian";
            r2.PhoneNumber = "(727) 596-0200";
            r2.WebAddress = "www.villagallace.com";
            r2.Type = "Fine Restaurant";
            r2.Hours = "Mon-Sat 5pm-10:30pm, Sun 4pm-10pm";

            Restaurant r3 = new Restaurant();
            r3.Name = "Three Coins Diner";
            r3.Location = "7410 N Nebraska Ave, Tampa, FL 33604, USA";
            r3.Cuisine = "American";
            r3.PhoneNumber = "(813) 239-1256";
            r3.WebAddress = "threecoinsdiner.net";
            r3.Type = "Diner";
            r3.Hours = "Mon-Sun 12am-11:30pm";

            string jsonStr = System.IO.File.ReadAllText(@"C:\revature\" + 
                @"hayes-timothy-project0\LocalGourmet\LocalGourmet.BLL\" +
                @"Models\Restaurants.json");

            List<Restaurant> rl = new List<Restaurant>();
            rl.Add(r);
            rl.Add(r2);
            rl.Add(r3);

            List<Restaurant> rList = 
                Serializer.Deserialize<List<Restaurant>>(jsonStr);
            Assert.AreEqual(rl[0].ToString(), rList[0].ToString());
            Assert.AreEqual(rl[1].ToString(), rList[1].ToString());
            Assert.AreEqual(rl[2].ToString(), rList[2].ToString());

        }
    }
}
