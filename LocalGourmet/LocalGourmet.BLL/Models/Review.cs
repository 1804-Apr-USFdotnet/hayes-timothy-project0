using LocalGourmet.DAL;
using LocalGourmet.BLL.Interfaces;
using System;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace LocalGourmet.BLL.Models
{
    [DataContract]
    public class Review : IReview
    {
        #region Constructors
        public Review() { }

        public Review(string name, string comment)
        {
            ReviewerName = name;
            Comment = comment;

            int defaultRating = 3;
            FoodRating = defaultRating;
            ServiceRating = defaultRating;
            AtmosphereRating = defaultRating;
            PriceRating = defaultRating;
        }

        // Review constructor that assigns the same rating to all
        // rating categories.
        public Review(string name, string comment, int simpleRating)
        {
            // Enforce a rating between 0 and 5 inclusive
            simpleRating = 
                simpleRating < 0 ? 0 : (simpleRating > 5 ? 5 : simpleRating);
            ReviewerName = name;
            Comment = comment;
            FoodRating = simpleRating;
            ServiceRating = simpleRating;
            AtmosphereRating = simpleRating;
            PriceRating = simpleRating;
        }

        public Review(string name, string comment, int foodRat, 
            int serviceRat, int atmosphereRat, int priceRat)
        {
            ReviewerName = name;
            Comment = comment;

            // Enforce a rating between 0 and 5 inclusive
            foodRat = 
                foodRat < 0 ? 0 : (foodRat > 5 ? 5 : foodRat);
            serviceRat = 
                serviceRat < 0 ? 0 : (serviceRat > 5 ? 5 : serviceRat);
            atmosphereRat = 
                atmosphereRat < 0 ? 0 : (atmosphereRat > 5 ? 5 : atmosphereRat);
            priceRat = 
                priceRat < 0 ? 0 : (priceRat > 5 ? 5 : priceRat);

            FoodRating = foodRat;
            ServiceRating = serviceRat;
            AtmosphereRating = atmosphereRat;
            PriceRating = priceRat;
        }
        #endregion

        #region Rating Components
        // Individual rating components
        [DataMember]
        private int foodRating;
        public int FoodRating
        {
            get { return foodRating; }
            set
            {
                // Enforce a rating between 0 and 5 inclusive
                foodRating = value < 0 ? 0 : (value > 5 ? 5 : value);
            }
        }

        [DataMember]
        private int serviceRating;
        public int ServiceRating 
        {
            get { return serviceRating; }
            set
            {
                // Enforce a rating between 0 and 5 inclusive
                serviceRating = value < 0 ? 0 : (value > 5 ? 5 : value);
            }
        }

        [DataMember]
        private int atmosphereRating;
        public int AtmosphereRating 
        {
            get { return atmosphereRating;  }
            set
            {
                // Enforce a rating between 0 and 5 inclusive
                atmosphereRating = value < 0 ? 0 : (value > 5 ? 5 : value);
            }
        }
        
        // Was it a good deal, or too expensive?
        [DataMember]
        private int priceRating;
        public int PriceRating 
        {
            get { return priceRating; }
            set
            {
                // Enforce a rating between 0 and 5 inclusive
                priceRating = value < 0 ? 0 : (value > 5 ? 5 : value);
            }
        }
        #endregion

        public int RestaurantID { get; set; }
        // ID indexing is handled by the data source.
        // This ID Property is only for storing the ID when converting
        // the object between layers. Do not set this when creating an
        // object -- it will be ignored.
        public int ID { get; set; }
        [DataMember]
        public string Comment { get; set; } 
        [DataMember]
        public string ReviewerName { get; set; } 

        // Calculates and returns the overall review rating based on the
        // individual review components.
        public float GetRating()
        {
            return (float) Math.Round(((FoodRating + ServiceRating +
                AtmosphereRating + PriceRating) / 4.0), 2);
        }

        public override string ToString()
        {
            return $"[{GetRating()}] {ReviewerName}: {Comment} [Food: " +
                $"{FoodRating}, Service: {ServiceRating}, Atmosphere: " +
                $"{AtmosphereRating}, Price: {PriceRating}]";
        }

        #region CRUD
        // CREATE
        public async Task AddReviewAsync()
        {
            DL.Review review = LibraryToData(this);
            ReviewAccessor ra = new ReviewAccessor();
            await ra.AddReviewAsync(review);
        }

        // READ
        public static List<Review> GetReviews()
        {
            ReviewAccessor reviewCRUD = new ReviewAccessor();
            List<DL.Review> dataList = reviewCRUD.GetReviews().ToList();
            List<Review> result = dataList.Select(x => DataToLibrary(x)).ToList();
            return result;
        }

        public static Review GetReviewByID(int id)
        {
            ReviewAccessor reviewCRUD = new ReviewAccessor();
            Review r;
            try
            {
                r = DataToLibrary(reviewCRUD.GetReviewByID(id));
            }
            catch
            {
                throw;
            }
            return r;
        }

        public static List<Review> GetReviewsByRestaurantID(int restID)
        {
            ReviewAccessor reviewCRUD = new ReviewAccessor();
            try
            {
                return reviewCRUD.GetReviewsByRestaurantID(restID).Select(x => DataToLibrary(x)).ToList();
            }
            catch
            {
                throw;
            }
        }

        // UPDATE
        public async Task UpdateReviewAsync(string reviewerName, 
            string comment, int foodRating, int serviceRating, int priceRating,
            int atmosphereRating, int restaurantID)
        {
            ReviewAccessor reviewCRUD = new ReviewAccessor();
            try
            {
                // Check restaurantID -- Will throw exception if invalid
                Restaurant rest = Restaurant.GetRestaurantByID(restaurantID);

                // Conform rating input to rating bounds
                foodRating = foodRating < 0 ? 0 : 
                    (foodRating > 5 ? 5 : foodRating);
                serviceRating = serviceRating < 0 ? 0 : 
                    (serviceRating > 5 ? 5 : serviceRating);
                priceRating = priceRating < 0 ? 0 : 
                    (priceRating > 5 ? 5 : priceRating);
                atmosphereRating = atmosphereRating < 0 ? 0 : 
                    (atmosphereRating > 5 ? 5 : atmosphereRating);

                await reviewCRUD.UpdateReviewAsync(this.ID, reviewerName,
                    comment, foodRating, serviceRating, priceRating, 
                    atmosphereRating, restaurantID);
            }
            catch
            {
                throw;
            }
        }

        // DELETE
        public async Task DeleteReviewAsync()
        {
            ReviewAccessor reviewCRUD = new ReviewAccessor();
            try
            {
                await reviewCRUD.DeleteReviewAsync(this.ID);
            }
            catch
            {
                throw;
            }
        }


        #endregion

        #region BLL-DL Mappers
        public static BLL.Models.Review DataToLibrary(DL.Review dataModel)
        {
            int revID = dataModel.ID;

            var libModel = new BLL.Models.Review()
            {
                ID = dataModel.ID,
                RestaurantID = dataModel.RestaurantID,
                ReviewerName = dataModel.ReviewerName,
                Comment = dataModel.Comment,
                FoodRating = dataModel.FoodRating,
                ServiceRating = dataModel.ServiceRating,
                AtmosphereRating = dataModel.AtmosphereRating,
                PriceRating = dataModel.PriceRating
            };
            return libModel;
        }

        public static DL.Review LibraryToData(BLL.Models.Review libModel)
        {
            var dataModel = new DL.Review();
            {
                dataModel.ID = libModel.ID;
                dataModel.RestaurantID = libModel.RestaurantID;
                dataModel.ReviewerName = libModel.ReviewerName;
                dataModel.Comment = libModel.Comment;
                dataModel.FoodRating = libModel.FoodRating;
                dataModel.ServiceRating = libModel.ServiceRating;
                dataModel.AtmosphereRating = libModel.AtmosphereRating;
                dataModel.PriceRating = libModel.PriceRating;
            };
            return dataModel;
        }
        #endregion
    }
}
