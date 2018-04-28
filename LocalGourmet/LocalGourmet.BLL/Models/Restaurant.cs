using LocalGourmet.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using LocalGourmet.DAL;
using System.Threading.Tasks;

namespace LocalGourmet.BLL.Models
{
    [DataContract]
    public class Restaurant : IRestaurant
    {
        public Restaurant()
        {
            Reviews = new List<Review>();
        }

        #region Properties
        public int ID { get; set; }
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
        #endregion

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

        public static List<Restaurant> GetTop3(List<Restaurant> restaurants)
        {
            if(restaurants == null || restaurants.Count < 3) { return restaurants; }
            List<Restaurant> top3 = new List<Restaurant>();

            float bestRating = 0.0f;
            float secondBestRating = 0.0f;
            float thirdBestRating = 0.0f;

            int bestIndex = 0;
            int secondBestIndex = 1;
            int thirdBestIndex = 2;

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

        #region CRUD
        // CREATE
        public async Task AddRestaurantAsync()
        {
            DL.Restaurant restaurant = LibraryToData(this);
            RestaurantAccessor ra = new RestaurantAccessor();
            await ra.AddRestaurantAsync(restaurant);
        }

        // READ
        public static List<Restaurant> GetRestaurants()
        {
            RestaurantAccessor restaurantCRUD = new RestaurantAccessor();
            List<DL.Restaurant> dataList = restaurantCRUD.GetRestaurants().ToList();
            List<Restaurant> result = dataList.Select(x => DataToLibrary(x)).ToList();
            return result;
        }

        public static Restaurant GetRestaurantByID(int id)
        {
            RestaurantAccessor restaurantCRUD = new RestaurantAccessor();
            Restaurant r;
            try
            {
                r = DataToLibrary(restaurantCRUD.GetRestaurantByID(id));
            }
            catch
            {
                throw;
            }
            return r;
        }
        
        // UPDATE
        public async Task UpdateRestaurantAsync(string name)
        {
            RestaurantAccessor restaurantCRUD = new RestaurantAccessor();
            try
            {
                await restaurantCRUD.UpdateRestaurantAsync(this.ID, name);
            }
            catch
            {
                throw;
            }
        }

        public void UpdateRestaurant(string name)
        {
            RestaurantAccessor restaurantCRUD = new RestaurantAccessor();
            try
            {
                restaurantCRUD.UpdateRestaurant(this.ID, name);
            }
            catch
            {
                throw;
            }
        }

        // DELETE


        #endregion






        // Deprecated -- only use for serialization testing
        public static List<Restaurant> GetAll()
        {
            List<Restaurant> restaurants = new List<Restaurant>();
            string json = System.IO.File.ReadAllText(@"C:\revature\" +
                @"hayes-timothy-project0\LocalGourmet\LocalGourmet.BLL\" +
                @"Configs\RestaurantsForUnitTest2.json");
            restaurants = Serializer.Deserialize<List<Restaurant>>(json);
            return restaurants;
        }

        #region Sort & Search
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
        #endregion

        #region ToString
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
        #endregion

        public static BLL.Models.Restaurant DataToLibrary(DL.Restaurant dataModel)
        {
            int restID = dataModel.ID;
            // Get a list of all reviews where RestaurantID == restID
            // Call ReviewAccessor's DataToLibrary function on those reviews
            // Put them all in a list
            // assign that list to the new Rest. model's review list.

            var libModel = new BLL.Models.Restaurant()
            {
                ID = dataModel.ID,
                Name = dataModel.Name,
                Location = dataModel.Location,
                Cuisine = dataModel.Cuisine,
                Specialty = dataModel.Specialty,
                PhoneNumber = dataModel.PhoneNumber,
                WebAddress = dataModel.WebAddress,
                Type = dataModel.Type,
                Hours = dataModel.Hours
            };
            return libModel;
        }

        public static DL.Restaurant LibraryToData(BLL.Models.Restaurant libModel)
        {
            var dataModel = new DL.Restaurant();
            {
                dataModel.ID = libModel.ID;
                dataModel.Name = libModel.Name;
                dataModel.Location = libModel.Location;
                dataModel.Cuisine = libModel.Cuisine;
                dataModel.Specialty = libModel.Specialty;
                dataModel.PhoneNumber = libModel.PhoneNumber;
                dataModel.WebAddress = libModel.WebAddress;
                dataModel.Type = libModel.Type;
                dataModel.Hours = libModel.Hours;
            };
            return dataModel;
        }
    }
}
