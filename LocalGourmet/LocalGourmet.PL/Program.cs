using NLog;
using LocalGourmet.BLL.Models;
using System;
using System.Collections.Generic;

namespace LocalGourmet.PL
{
    class Program
    {
        // Returns a custom list of reviews, with RestaurantIDs from 1-10
        public static Review[] GenerateReviews(int howMany)
        {
            string[] names = new string[4945];
            string nameString = System.IO.File.ReadAllText(@"C:\revature\hayes-timothy-project0\LocalGourmet\LocalGourmet.BLL\Configs\Names.txt");
            System.IO.StringReader r = new System.IO.StringReader(nameString);
            for(int i = 0; i < 4945; i++)
            {
                names[i] = r.ReadLine();
            }

            Review r1 = new Review("", "I'm never coming here again!", 0, 1, 1, 1);
            Review r2 = new Review("", "I'd rather eat bread and water.", 1, 2, 1, 0);
            Review r3 = new Review("", "Bleh!", 1, 2, 1, 2);
            Review r4 = new Review("", "I hope no one saw me eat here.", 2, 3, 1, 2);
            Review r5 = new Review("", "Better than starving...", 2, 3, 2, 3);
            Review r6 = new Review("", "At least it was cheap.", 3, 2, 4, 3);
            Review r7 = new Review("", "I'd come here again.", 3, 4, 3, 4);
            Review r8 = new Review("", "I had great expectations...", 2, 3, 4, 4);
            Review r9 = new Review("", "Wow!", 4, 4, 5, 4);
            Review r10 = new Review("", "Best restaurant ever!", 5, 5, 5, 5);

            Review[] revs = new Review[10];
            revs[0] = r1;
            revs[1] = r2;
            revs[2] = r3;
            revs[3] = r4;
            revs[4] = r5;
            revs[5] = r6;
            revs[6] = r7;
            revs[7] = r8;
            revs[8] = r9;
            revs[9] = r10;

            int revIndex;
            string firstName, lastName;
            Random rnd = new Random();
            Review[] customReviews = new Review[howMany];
            for(int i = 0; i < howMany; i++)
            {
                revIndex = rnd.Next(10);
                Review customRev = new Review();
                Review q = revs[revIndex];
                customRev.Comment = q.Comment;
                customRev.FoodRating = q.FoodRating;
                customRev.ServiceRating = q.ServiceRating;
                customRev.AtmosphereRating = q.AtmosphereRating;
                customRev.PriceRating = q.PriceRating;
                firstName = names[rnd.Next(4945)];
                lastName = names[rnd.Next(4945)];
                customRev.ReviewerName = $"{firstName} {lastName}";
                customRev.RestaurantID = rnd.Next(1, 11);
                customReviews[i] = customRev;
                Console.WriteLine($"Added review {i + 1} out of {howMany}");
            }
            return customReviews;
        }

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
            while(input != "quit")
            {
                Console.WriteLine();
                Console.WriteLine("Type [quit] at any time");
                Console.WriteLine("Do you want to view information from [all] " +
                    "restaurants, only the [top3], or [search] by name?");
                Console.WriteLine();
                Console.Write("<input> ");
                input = Console.ReadLine().ToLower();
                log.Info(input);
                while(input != "all" && input != "top3" && input != "search"
                    && input != "quit")
                {
                    Console.WriteLine($"[{input}] is invalid. Choose [all], [top3], [search] or [quit].");
                    Console.Write("<input> ");
                    input = Console.ReadLine().ToLower();
                    log.Info(input);
                }
                switch(input)
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
                if(input=="quit") { break; }

                if(restaurantsTemp is null || restaurantsTemp.Count == 0)
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
                while(input != "all" && input != "rev" && input != "summ"
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
                if(input=="quit") { break; }

                // Choose how to sort the restaurants
                Console.WriteLine();
                Console.WriteLine("Do you want to sort by [name] ascending," +
                    " sort by [cuisine] ascending, or sort by average" +
                    " [rating] descending?");
                Console.Write("<input> ");
                input = Console.ReadLine().ToLower();
                log.Info(input);
                while(input != "name" && input != "cuisine" && input != "rating"
                    && input != "quit")
                {
                    Console.WriteLine($"[{input}] is invalid. Choose [name], [cuisine], [rating] or [quit].");
                    Console.Write("<input> ");
                    input = Console.ReadLine().ToLower();
                    log.Info(input);
                }
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

                Console.WriteLine();
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine();
                // Perform command
                switch(howMuchInfo)
                {
                    case "all":
                        switch(ordering)
                        {
                            case "name":
                                DisplayWithAllInfo(Restaurant.SortByNameAsc(restaurantsTemp));
                                break;
                            case "cuisine":
                                DisplayWithAllInfo(Restaurant.SortByCuisineAsc(restaurantsTemp));
                                break;
                            case "rating":
                                DisplayWithAllInfo(Restaurant.SortByAvgRatingDesc(restaurantsTemp));
                                break;
                        }
                        break;
                    case "rev":
                        switch(ordering)
                        {
                            case "name":
                                DisplayAllInfoWithReviews(Restaurant.SortByNameAsc(restaurantsTemp));
                                break;
                            case "cuisine":
                                DisplayAllInfoWithReviews(Restaurant.SortByCuisineAsc(restaurantsTemp));
                                break;
                            case "rating":
                                DisplayAllInfoWithReviews(Restaurant.SortByAvgRatingDesc(restaurantsTemp));
                                break;
                        }
                        break;
                    case "summ":
                        switch(ordering)
                        {
                            case "name":
                                DisplaySummarized(Restaurant.SortByNameAsc(restaurantsTemp));
                                break;
                            case "cuisine":
                                DisplaySummarized(Restaurant.SortByCuisineAsc(restaurantsTemp));
                                break;
                            case "rating":
                                DisplaySummarized(Restaurant.SortByAvgRatingDesc(restaurantsTemp));
                                break;
                        }
                        break;
                    case "summ rev":
                        switch(ordering)
                        {
                            case "name":
                                DisplaySummarizedWithReviews(Restaurant.SortByNameAsc(restaurantsTemp));
                                break;
                            case "cuisine":
                                DisplaySummarizedWithReviews(Restaurant.SortByCuisineAsc(restaurantsTemp));
                                break;
                            case "rating":
                                DisplaySummarizedWithReviews(Restaurant.SortByAvgRatingDesc(restaurantsTemp));
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
                Console.WriteLine();
                List<Review> reviews = restaurant.Reviews;
                foreach (var r in reviews)
                {
                    Console.WriteLine(r);
                }
                Console.WriteLine();
                Console.WriteLine("*********************************************");
                Console.WriteLine("*********************************************");
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        public static void DisplayAllInfoWithReviews(List<Restaurant> list)
        {
            foreach (var restaurant in list)
            {
                Console.WriteLine(restaurant);
                Console.WriteLine();
                List<Review> reviews = restaurant.Reviews;
                foreach (var r in reviews)
                {
                    Console.WriteLine(r);
                }
                Console.WriteLine();
                Console.WriteLine("*********************************************");
                Console.WriteLine("*********************************************");
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}
