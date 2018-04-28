using NLog;
using LocalGourmet.BLL.Models;
using System;
using System.Collections.Generic;

namespace LocalGourmet.PL
{
    class Program
    {
        // Returns a custom list of 1000 reviews, with RestaurantIDs from 1-10
        public static List<Review> ReviewScript()
        {
            List<string> names = new List<string>();
            string nameString = System.IO.File.ReadAllText(@"C:\Users\tjhay\Downloads\Names.txt");
            System.IO.StringReader r = new System.IO.StringReader(nameString);
            for(int i = 1; i <= 4945; i++)
            {
                names.Add(r.ReadLine());
            }

            Review r1 = new Review("", "I'm never coming here again!", 0, 0, 0, 0);
            Review r2 = new Review("", "I'd rather eat bread and water.", 1, 0, 1, 0);
            Review r3 = new Review("", "Bleh!", 1, 1, 1, 1);
            Review r4 = new Review("", "I hope no one saw me eat here.", 1, 2, 1, 2);
            Review r5 = new Review("", "Better than starving...", 2, 2, 2, 2);
            Review r6 = new Review("", "At least it was cheap.", 2, 2, 3, 3);
            Review r7 = new Review("", "I'd come here again.", 3, 3, 3, 4);
            Review r8= new Review("", "I had great expectations...", 2, 3, 4, 4);
            Review r9 = new Review("", "Wow!", 4, 4, 4, 4);
            Review r10 = new Review("", "Best restaurant ever!", 5, 5, 4, 4);

            List<Review> revs = new List<Review>();
            revs.Add(r1);
            revs.Add(r2);
            revs.Add(r3);
            revs.Add(r4);
            revs.Add(r5);
            revs.Add(r6);
            revs.Add(r7);
            revs.Add(r8);
            revs.Add(r9);
            revs.Add(r10);

            int revIndex;
            string firstName, lastName;
            Random rnd = new Random();
            List<Review> customReviews = new List<Review>();
            for(int i = 0; i < 1000; i++)
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
                customReviews.Add(customRev);
                Console.WriteLine($"Added review {i + 1} out of 1000");
            }
            return customReviews;
        }

        public static void Main(string[] args)
        {
            //List<Review> revs = Review.GetReviews();
            //foreach (var item in revs)
            //{
            //    Console.WriteLine(item);
            //}

            //List<Review> customReviews = ReviewScript();
            //foreach (var item in customReviews)
            //{
            //    item.AddReviewAsync();
            //}

            //Restaurant z = Restaurant.GetRestaurantByID(1);
            //Console.WriteLine(z);
            //z.UpdateRestaurantAsync(z.Name, z.Location, "American", 
            //    z.Specialty, z.PhoneNumber, z.WebAddress, z.Type, z.Hours);
            //z = Restaurant.GetRestaurantByID(1);
            //Console.WriteLine(z);

            //Review x = Review.GetReviewByID(1);
            //x.DeleteReviewAsync();

            Logger log = LogManager.GetLogger("file");
            log.Info("Start session: " + System.DateTime.Now);

            // Start Console UI
            Console.WriteLine("Local Gourmet App");
            Console.WriteLine("-----------------");

            string input = "";
            List<Restaurant> restaurants = null;
            string howMuchInfo = "";
            string ordering = "";

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
                        restaurants = Restaurant.GetRestaurants();
                        break;
                    case "top3":
                        restaurants = Restaurant.GetTop3(Restaurant.GetRestaurants());
                        break;
                    case "search":
                        restaurants = Restaurant.SearchByName(Restaurant.GetRestaurants());
                        Console.WriteLine("------------------------------");
                        Console.WriteLine(restaurants.Count + " matches:");
                        Console.WriteLine("------------------------------");
                        foreach (Restaurant r in restaurants)
                        {
                            Console.WriteLine(r.Name);
                        }
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
