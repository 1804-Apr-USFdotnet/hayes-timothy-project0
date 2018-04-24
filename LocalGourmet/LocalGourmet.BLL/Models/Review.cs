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
    public class Review : IReview
    {
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
            return 0.0f;
        }

    }
}
