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
                    case "all sum":
                    case "as":
                        DisplaySummarized(restaurants);
                        break;
                    case "top3 sum":
                    case "t3s":
                        DisplaySummarized(top3);
                        break;
                    case "all sum rev":
                    case "asr":
                        DisplaySummarizedWithReviews(restaurants);
                        break;
                    case "top3 sum rev":
                    case "t3sr":
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
            Console.WriteLine("help [h]                -- display all commands");
            Console.WriteLine("all [a]                 -- display all info for all restaurants");
            Console.WriteLine("top3 [t3]               -- display all info for top3 restaurants");
            Console.WriteLine("all sum [as]            -- display summarized info for all restaurants");
            Console.WriteLine("top3 sum [t3s]          -- display summarized info for top3 restaurants");
            Console.WriteLine("all sum rev [asr]       -- display summarized info and reviews for all restaurants");
            Console.WriteLine("top3 sum rev [t3sr]     -- display summarized info and reviews for top3 restaurants");
            Console.WriteLine("quit                    -- quit the application");
            Console.WriteLine();
        }

        static bool Valid(string input)
        {
            switch(input)
            {
                case "help":
                case "h":
                case "all":
                case "a":
                case "top3":
                case "t3":
                case "all sum":
                case "as":
                case "top3 sum":
                case "t3s":
                case "all sum rev":
                case "asr":
                case "top3 sum rev":
                case "t3sr":
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
