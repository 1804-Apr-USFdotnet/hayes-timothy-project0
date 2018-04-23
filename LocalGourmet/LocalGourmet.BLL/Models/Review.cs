using LocalGourmet.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalGourmet.BLL.Models
{
    public class Review : IReview
    {
        // Individual rating components
        int FoodRating { get; set; }
        int ServiceRating { get; set; }
        int AtmosphereRating { get; set; }
        int PriceRating { get; set; } // Was it a good deal, or too expensive?

        // Calculates and returns the overall review rating based on the
        // individual review components.
        public float GetRating()
        {
            throw new NotImplementedException();
        }

        public string Comment { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ReviewerName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
