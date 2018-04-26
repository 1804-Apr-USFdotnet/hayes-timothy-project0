using LocalGourmet.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace LocalGourmet.PL
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Deserialize Restaurants
            List<Restaurant> restaurants = Restaurant.GetAll();
            List<Restaurant> top3 = Restaurant.GetTop3();

            // Start Console UI
            Console.WriteLine("Local Gourmet App");
            Console.WriteLine("-----------------");
            Console.WriteLine("                 ");

            string input = "help";
            while(input != "quit")
            {
                // Perform command
                switch(input)
                {
                    case "help":
                    case "h":
                        Help();
                        break;
                    case "all":
                    case "a":
                        DisplayWithAllInfo(restaurants);
                        break;
                    case "top3":
                    case "t3":
                        DisplayWithAllInfo(top3);
                        break;
                    case "summary all":
                    case "sa":
                        DisplaySummarized(restaurants);
                        break;
                    case "summary top3":
                    case "st3":
                        DisplaySummarized(top3);
                        break;
                    case "ar":
                        DisplaySummarizedWithReviews(restaurants);
                        break;
                    case "t3r":
                        DisplaySummarizedWithReviews(top3);
                        break;
                }


                // Get next command
                Console.Write("<input> ");
                input = Console.ReadLine().ToLower();
                if(!Valid(input))
                {
                    Console.WriteLine($"[{input}] is an invalid command.");
                    input = "help";
                }
            }



            //List<Restaurant> sorted = Restaurant.SortByNameAsc();
            //List<Restaurant> sorted = Restaurant.SortByCuisineAsc();
            //List<Restaurant> sorted = Restaurant.SortByAvgRatingDesc();
        }

        static void Help()
        {
            Console.WriteLine("List of commands:");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("help         -- display all commands");
            Console.WriteLine("all          -- display all info for all restaurants");
            Console.WriteLine("top3         -- display all info for top3 restaurants");
            Console.WriteLine("summary all  -- display summarized info for all restaurants");
            Console.WriteLine("summary top3 -- display summarized info for top3 restaurants");
            Console.WriteLine("all rev      -- display summarized info and reviews for all restaurants");
            Console.WriteLine("top3 rev     -- display summarized info and reviews for top3 restaurants");
            Console.WriteLine("quit         -- quit the application");
            Console.WriteLine();
        }

        static bool Valid(string input)
        {
            switch(input)
            {
                case "help":
                case "all":
                case "top3":
                case "summary all":
                case "summary top3":
                case "all rev":
                case "top3 rev":
                case "quit":
                    return true;
                default:
                    return false;
            }
        }

        public static void DisplayWithAllInfo(List<Restaurant> list)
        {
            foreach (var restaurant in list)
            {
                Console.WriteLine(restaurant);
                Console.WriteLine();
            }
        }

        public static void DisplaySummarized(List<Restaurant> list)
        {
            foreach (var restaurant in list)
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
