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


        // Return name and rating only
        public string GetNameAndRating()
        {
            return $"{Name} [{GetAvgRating()}]";
        }

        // Return summary of info
        public string GetSummary()
        {
            return $"{Name}, {Cuisine}, {Reviews.Count} Reviews, " +
                $"{Type}, AvgRating: {GetAvgRating()}.";
        }

        // Return all info
        public override string ToString()
        {
            return $"{Name}, {Cuisine}, {Location}, {Specialty}, " +
                $"{PhoneNumber}, {WebAddress}, {Hours}, " +
                $"{Reviews.Count} Reviews, {Type}, AvgRating: {GetAvgRating()}.";
        }
    }
}
