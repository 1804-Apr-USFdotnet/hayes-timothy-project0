﻿using LocalGourmet.BLL.Interfaces;
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
    public class Review : IReview
    {
        public Review(string name, string comment)
        {
            ReviewerName = name;
            Comment = comment;
        }

        // Review constructor that assigns the same rating to all
        // rating categories.
        public Review(string name, string comment, int simpleRating)
        {
            ReviewerName = name;
            Comment = comment;
            FoodRating = simpleRating;
            ServiceRating = simpleRating;
            AtmosphereRating = simpleRating;
            PriceRating = simpleRating;
        }

        // Individual rating components
        [DataMember]
        int FoodRating { get; set; }
        [DataMember]
        int ServiceRating { get; set; }
        [DataMember]
        int AtmosphereRating { get; set; }
        [DataMember]
        int PriceRating { get; set; } // Was it a good deal, or too expensive?

        [DataMember]
        public string Comment { get; set; } 
        [DataMember]
        public string ReviewerName { get; set; } 

        // Calculates and returns the overall review rating based on the
        // individual review components.
        public float GetRating()
        {
            return (float)(FoodRating + ServiceRating + 
                AtmosphereRating + PriceRating) / 4.0f;
        }

    }
}
