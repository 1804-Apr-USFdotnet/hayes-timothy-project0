using LocalGourmet.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace LocalGourmet.BLL.Models
{
    [DataContract]
    public class Restaurant : IRestaurant
    {
        public Restaurant()
        {
            Reviews = new List<Review>();
        }

        [DataMember]
        public string Name { get; set; } 
        [DataMember]
        public string Location { get; set; }
        [DataMember]
        public string Cuisine { get; set; }
        [DataMember]
        public string Specialty { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
        [DataMember]
        public string WebAddress { get; set; }
        [DataMember]
        public List<Review> Reviews { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string Hours { get; set; }

        public float GetAvgRating()
        {
            if(Reviews.Count == 0) { return 0.0f; }
            float rating = 0.0f;
            foreach (var review in Reviews)
            {
                rating += review.GetRating();
            }
            rating /= Reviews.Count;
            return (float) Math.Round(rating, 2);
        }

        public static List<Restaurant> GetTop3()
        {
            List<Restaurant> top3 = new List<Restaurant>();

            List<Restaurant> restaurants = new List<Restaurant>();
            string json = System.IO.File.ReadAllText(@"C:\revature\" + 
                @"hayes-timothy-project0\LocalGourmet\LocalGourmet.BLL\" +
                @"Configs\Restaurants.json");
            restaurants = Serializer.Deserialize<List<Restaurant>>(json);

            float bestRating = 0.0f;
            float secondBestRating = 0.0f;
            float thirdBestRating = 0.0f;

            int bestIndex = -1;
            int secondBestIndex = -1;
            int thirdBestIndex = -1;

            int index = 0;

            // Find top 3
            foreach (Restaurant r in restaurants)
            {
                float rating = r.GetAvgRating();
                // New Best
                if(rating > bestRating)
                {
                    thirdBestRating = secondBestRating;
                    secondBestRating = bestRating;
                    bestRating = rating;

                    thirdBestIndex = secondBestIndex;
                    secondBestIndex = bestIndex;
                    bestIndex = index;
                // New Second Best
                } else if (rating <= bestRating && rating > secondBestRating)
                {
                    thirdBestRating = secondBestRating;
                    secondBestRating = rating;

                    thirdBestIndex = secondBestIndex;
                    secondBestIndex = index;
                // New Third Best
                } else if (rating <= secondBestRating && rating > thirdBestRating)
                {
                    thirdBestRating = rating;
                    thirdBestIndex = index;
                }
                index++;
            }

            top3.Add(restaurants.ElementAt(bestIndex));
            top3.Add(restaurants.ElementAt(secondBestIndex));
            top3.Add(restaurants.ElementAt(thirdBestIndex));

            return top3;
        }

        public static List<Restaurant> GetAll()
        {
            List<Restaurant> restaurants = new List<Restaurant>();
            string json = System.IO.File.ReadAllText(@"C:\revature\" + 
                @"hayes-timothy-project0\LocalGourmet\LocalGourmet.BLL\" +
                @"Configs\Restaurants.json");
            restaurants = Serializer.Deserialize<List<Restaurant>>(json);
            return restaurants;
        }

        public static List<Restaurant> SortByAvgRatingDesc(List<Restaurant> list)
        {
            return list.OrderByDescending(x => x.GetAvgRating()).ToList();
        }

        public static List<Restaurant> SortByNameAsc(List<Restaurant> list)
        {
            return list.OrderBy(x => x.Name).ToList();
        }

        public static List<Restaurant> SortByCuisineAsc(List<Restaurant> list)
        {
            return list.OrderBy(x => x.Cuisine).ToList();
        }

        // Helper Search method for use with console UI
        public static List<Restaurant> SearchByName(List<Restaurant> list)
        {
            Console.WriteLine("Enter (partial) restaurant name:");
            Console.Write("<input> ");
            string search = Console.ReadLine();
            return Restaurant.SearchByName(list, search);
        }

        // Return a list of all restaurants whose names were partially
        // matched by the search string.
        public static List<Restaurant> SearchByName(List<Restaurant> list, string search)
        {
            List<Restaurant> matches = new List<Restaurant>();
            if (search == "") { return matches; } // return with zero matches
            var result = (from r in list
                          where r.Name.ToLower().Contains(search.ToLower())
                          select r).ToList();
            foreach (var item in result)
            {
                matches.Add(item);
            }
            return matches;
        }

        // Return name and rating only
        public string GetNameAndRating()
        {
            return $"{Name} [{GetAvgRating()}]";
        }

        // Return summary of info
        public string GetSummary()
        {
            return $"{Name}, {Cuisine}, {Reviews.Count} Reviews, " +
                $"{Type}, AvgRating: {GetAvgRating()}";
        }

        // Return all info
        public override string ToString()
        {
            return $"{Name}, {Cuisine}, {Type}, {Specialty}, " +
                $"AvgRating: {GetAvgRating()}, {Reviews.Count} Reviews, " +
                $"{Location}, {PhoneNumber}, {WebAddress}, {Hours}";
        }
    }
}
