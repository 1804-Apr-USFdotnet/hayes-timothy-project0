using NLog;
using LocalGourmet.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LocalGourmet.PL
{
    class Program
    {
        public static void Main(string[] args)
        {
            Logger log = LogManager.GetLogger("file");
            log.Info("Start session: " + System.DateTime.Now);

            // Start Console UI
            Console.WriteLine("Local Gourmet App");
            Console.WriteLine("-----------------");

            string input = "";
            string howMuchInfo = "";
            string ordering = "";

            List<Restaurant> restaurants = Restaurant.GetRestaurants();
            List<Restaurant> restaurantsTemp = null;
            // Choose initial restaurant list
            while (input != "quit")
            {
                Console.WriteLine();
                Console.WriteLine("Type [quit] at any time");
                Console.WriteLine("Do you want to view information from [all] " +
                    "restaurants, only the [top3], or [search] by name?");
                Console.WriteLine();
                Console.Write("<input> ");
                input = Console.ReadLine().ToLower();
                log.Info(input);
                while (input != "all" && input != "top3" && input != "search"
                    && input != "quit")
                {
                    Console.WriteLine($"[{input}] is invalid. Choose [all], [top3], [search] or [quit].");
                    Console.Write("<input> ");
                    input = Console.ReadLine().ToLower();
                    log.Info(input);
                }
                switch (input)
                {
                    case "all":
                        restaurantsTemp = restaurants;
                        break;
                    case "top3":
                        restaurantsTemp = Restaurant.GetTop3(restaurants);
                        break;
                    case "search":
                        restaurantsTemp = Restaurant.SearchByName(restaurants);
                        Console.WriteLine("------------------------------");
                        Console.WriteLine(restaurantsTemp.Count + " matches:");
                        Console.WriteLine("------------------------------");
                        foreach (Restaurant r in restaurantsTemp)
                        {
                            Console.WriteLine(r.Name);
                        }
                        break;
                    case "quit":
                        break;
                }
                if (input == "quit") { break; }

                if (restaurantsTemp is null || restaurantsTemp.Count == 0)
                {
                    Console.WriteLine("No restaurants found.");
                    input = "";
                    continue;
                }

                // Choose how much info to see for each restaurant
                Console.WriteLine();
                Console.WriteLine("Do you want to view [all] restaurant info, " +
                    "all info with [rev]iews, [summ]arized info, or " +
                    "summarized info with reviews [summ rev]?");
                Console.Write("<input> ");
                input = Console.ReadLine().ToLower();
                log.Info(input);
                while (input != "all" && input != "rev" && input != "summ"
                    && input != "summ rev" && input != "quit")
                {
                    Console.WriteLine($"[{input}] is invalid. Choose [all], [rev], [summ], [summ rev] or [quit].");
                    Console.Write("<input> ");
                    input = Console.ReadLine().ToLower();
                    log.Info(input);
                }
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
                if (input == "quit") { break; }

                // Choose how to sort the restaurants
                Console.WriteLine();
                Console.WriteLine("Do you want to sort by [name] ascending," +
                    " sort by [cuisine] ascending, or sort by average" +
                    " [rating] descending?");
                Console.Write("<input> ");
                input = Console.ReadLine().ToLower();
                log.Info(input);
                while (input != "name" && input != "cuisine" && input != "rating"
                    && input != "quit")
                {
                    Console.WriteLine($"[{input}] is invalid. Choose [name], [cuisine], [rating] or [quit].");
                    Console.Write("<input> ");
                    input = Console.ReadLine().ToLower();
                    log.Info(input);
                }
                switch (input)
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
                if (input == "quit") { break; }

                Console.WriteLine();
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine();

                // Perform command
                switch (howMuchInfo)
                {
                    case "all":
                        switch (ordering)
                        {
                            case "name":
                                Restaurant.DisplayWithAllInfo(Restaurant.SortByNameAsc(restaurantsTemp));
                                break;
                            case "cuisine":
                                Restaurant.DisplayWithAllInfo(Restaurant.SortByCuisineAsc(restaurantsTemp));
                                break;
                            case "rating":
                                Restaurant.DisplayWithAllInfo(Restaurant.SortByAvgRatingDesc(restaurantsTemp));
                                break;
                        }
                        break;
                    case "rev":
                        switch (ordering)
                        {
                            case "name":
                                Restaurant.DisplayAllInfoWithReviews(Restaurant.SortByNameAsc(restaurantsTemp));
                                break;
                            case "cuisine":
                                Restaurant.DisplayAllInfoWithReviews(Restaurant.SortByCuisineAsc(restaurantsTemp));
                                break;
                            case "rating":
                                Restaurant.DisplayAllInfoWithReviews(Restaurant.SortByAvgRatingDesc(restaurantsTemp));
                                break;
                        }
                        break;
                    case "summ":
                        switch (ordering)
                        {
                            case "name":
                                Restaurant.DisplaySummarized(Restaurant.SortByNameAsc(restaurantsTemp));
                                break;
                            case "cuisine":
                                Restaurant.DisplaySummarized(Restaurant.SortByCuisineAsc(restaurantsTemp));
                                break;
                            case "rating":
                                Restaurant.DisplaySummarized(Restaurant.SortByAvgRatingDesc(restaurantsTemp));
                                break;
                        }
                        break;
                    case "summ rev":
                        switch (ordering)
                        {
                            case "name":
                                Restaurant.DisplaySummarizedWithReviews(Restaurant.SortByNameAsc(restaurantsTemp));
                                break;
                            case "cuisine":
                                Restaurant.DisplaySummarizedWithReviews(Restaurant.SortByCuisineAsc(restaurantsTemp));
                                break;
                            case "rating":
                                Restaurant.DisplaySummarizedWithReviews(Restaurant.SortByAvgRatingDesc(restaurantsTemp));
                                break;
                        }
                        break;
                }
                Console.WriteLine();
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine();
            }
            log.Info("End session: " + System.DateTime.Now);
        }
    }
}
