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

            // Demo of BLL functionality

            //DisplayAllWithAllInfo(restaurants);
            //DisplayTop3Summarized(top3);
            //DisplaySummarizedWithReviews(top3);

            Console.ReadLine();
        }

        public static void DisplayAllWithAllInfo(List<Restaurant> all)
        {
            foreach (var restaurant in all)
            {
                Console.WriteLine(restaurant);
                Console.WriteLine();
            }
        }

        public static void DisplayTop3Summarized(List<Restaurant> top3)
        {
            foreach (var restaurant in top3)
            {
                Console.WriteLine(restaurant.GetSummary());
                Console.WriteLine();
            }
        }

        public static void DisplaySummarizedWithReviews(List<Restaurant> list)
        {
            foreach (var restaurant in list)
            {
                Console.WriteLine(restaurant.GetSummary());
                List<Review> reviews = restaurant.Reviews;
                foreach (var r in reviews)
                {
                    Console.WriteLine(r);
                }
                Console.WriteLine();
            }
        }
    }
}
