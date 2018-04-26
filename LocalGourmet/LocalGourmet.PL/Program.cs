﻿using LocalGourmet.BLL.Models;
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
            List<Restaurant> restaurants = new List<Restaurant>();
            string json = System.IO.File.ReadAllText(@"C:\revature\" +
                @"hayes-timothy-project0\LocalGourmet\LocalGourmet.BLL\" +
                @"Configs\Restaurants.json");
            restaurants = Serializer.Deserialize<List<Restaurant>>(json);

            //List<Restaurant> top3 = new List<Restaurant>();
            //top3 = Restaurant.GetTop3();

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
                        Help();
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
            Quit();



            //List<Restaurant> sorted = Restaurant.SortByNameAsc();
            //List<Restaurant> sorted = Restaurant.SortByCuisineAsc();
            List<Restaurant> sorted = Restaurant.SortByAvgRatingDesc();



            // Demo of BLL functionality

            //DisplaySummarized(sorted);
            DisplayAllWithAllInfo(restaurants);
            //DisplaySummarized(top3);
            //DisplaySummarizedWithReviews(top3);

            Console.ReadLine();
        }

        static void Help()
        {
            Console.WriteLine("List of commands:");
            Console.WriteLine("help");
            Console.WriteLine("quit");
            Console.WriteLine();
        }

        static bool Valid(string input)
        {
            switch(input)
            {
                case "quit":
                case "help":
                    return true;
                default:
                    return false;
            }
        }

        static void Quit()
        {
            Console.Write("Quitting Local Gourmet App");
            Thread.Sleep(800);
            Console.Write(".");
            Thread.Sleep(800);
            Console.Write(".");
            Thread.Sleep(800);
            Console.Write(".");
        }

        public static void DisplayAllWithAllInfo(List<Restaurant> all)
        {
            foreach (var restaurant in all)
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
