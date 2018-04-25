using LocalGourmet.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalGourmet.PL
{
    class Program
    {
        static void Main(string[] args)
        {
            // Deserialize Restaurants
            List<Restaurant> restaurants = new List<Restaurant>();
            string json = System.IO.File.ReadAllText(@"C:\revature\" +
                @"hayes-timothy-project0\LocalGourmet\LocalGourmet.BLL\" +
                @"Configs\Restaurants.json");
            restaurants = Serializer.Deserialize<List<Restaurant>>(json);

            List<Restaurant> top3 = new List<Restaurant>();
            top3 = Restaurant.GetTop3();

            // Display Restaurants
            foreach (var restaurant in top3)
            {
                Console.WriteLine(restaurant);
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}
