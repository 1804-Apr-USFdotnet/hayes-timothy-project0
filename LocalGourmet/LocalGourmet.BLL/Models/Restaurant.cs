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
            return rating;
        }

        public override string ToString()
        {
            return $"{Name}, {Location}, {Cuisine}, {Specialty}, " +
                $"{PhoneNumber}, {WebAddress}, {Reviews.Count} Reviews, " +
                $"{Type}, {Hours}, AvgRating: {GetAvgRating()}.";
        }
    }
}
