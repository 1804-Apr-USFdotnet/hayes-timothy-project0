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
            // Start Console UI
            Console.WriteLine("Local Gourmet App");
            Console.WriteLine("-----------------");
            Console.WriteLine("                 ");

            string input = "";
            List<Restaurant> restaurants = null;
            string howMuchInfo = "";
            string ordering = "";

            while(input != "quit")
            {
                Console.WriteLine("Type [quit] at any time");
                Console.WriteLine("Do you want to view information from [all] " +
                    "restaurants, only the [top3], or [search] by name?");
                Console.Write("<input> ");
                input = Console.ReadLine().ToLower();
                switch(input)
                {
                    case "all":
                        restaurants = Restaurant.GetAll();
                        break;
                    case "top3":
                        restaurants = Restaurant.GetTop3();
                        break;
                    case "search":
                        restaurants = Restaurant.SearchByName(Restaurant.GetAll());
                        break;
                    case "quit":
                        break;
                }
                if(input=="quit") { break; }

                if(restaurants is null || restaurants.Count == 0)
                {
                    Console.WriteLine("No restaurants found.");
                    input = "";
                    continue;
                }

                Console.WriteLine("Do you want to view [all] restaurant info, " +
                    "all info with [rev]iews, [summ]arized info, or " +
                    "summarized info with reviews [summ rev]?");
                Console.Write("<input> ");
                input = Console.ReadLine().ToLower();
                switch (input)
                {
                    case "all":
                        howMuchInfo = "all";
                        break;
                    case "rev":
                        howMuchInfo = "rev";
                        break;
                    case "summ":
                        howMuchInfo = "summ";
                        break;
                    case "summ rev":
                        howMuchInfo = "summ rev";
                        break;
                    case "quit":
                        break;
                }
                if(input=="quit") { break; }

                Console.WriteLine("Do you want to sort by [name] ascending," +
                    " sort by [cuisine] ascending, or sort by average" +
                    " [rating] descending?");
                Console.Write("<input> ");
                input = Console.ReadLine().ToLower();
                switch(input)
                {
                    case "name":
                        ordering = "name";
                        break;
                    case "cuisine":
                        ordering = "cuisine";
                        break;
                    case "rating":
                        ordering = "rating";
                        break;
                    case "quit":
                        break;
                }
                if(input=="quit") { break; }

                // Perform command
                switch(howMuchInfo)
                {
                    case "all":
                        switch(ordering)
                        {
                            case "name":
                                DisplayWithAllInfo(Restaurant.SortByNameAsc(restaurants));
                                break;
                            case "cuisine":
                                DisplayWithAllInfo(Restaurant.SortByCuisineAsc(restaurants));
                                break;
                            case "rating":
                                DisplayWithAllInfo(Restaurant.SortByAvgRatingDesc(restaurants));
                                break;
                        }
                        break;
                    case "rev":
                        switch(ordering)
                        {
                            case "name":
                                DisplayAllInfoWithReviews(Restaurant.SortByNameAsc(restaurants));
                                break;
                            case "cuisine":
                                DisplayAllInfoWithReviews(Restaurant.SortByCuisineAsc(restaurants));
                                break;
                            case "rating":
                                DisplayAllInfoWithReviews(Restaurant.SortByAvgRatingDesc(restaurants));
                                break;
                        }
                        break;
                    case "summ":
                        switch(ordering)
                        {
                            case "name":
                                DisplaySummarized(Restaurant.SortByNameAsc(restaurants));
                                break;
                            case "cuisine":
                                DisplaySummarized(Restaurant.SortByCuisineAsc(restaurants));
                                break;
                            case "rating":
                                DisplaySummarized(Restaurant.SortByAvgRatingDesc(restaurants));
                                break;
                        }
                        break;
                    case "summ rev":
                        switch(ordering)
                        {
                            case "name":
                                DisplaySummarizedWithReviews(Restaurant.SortByNameAsc(restaurants));
                                break;
                            case "cuisine":
                                DisplaySummarizedWithReviews(Restaurant.SortByCuisineAsc(restaurants));
                                break;
                            case "rating":
                                DisplaySummarizedWithReviews(Restaurant.SortByAvgRatingDesc(restaurants));
                                break;
                        }
                        break;
                }
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

        public static void DisplayAllInfoWithReviews(List<Restaurant> list)
        {
            foreach (var restaurant in list)
            {
                Console.WriteLine(restaurant);
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
