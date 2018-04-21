using RestaurantReviews.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Models.Interfaces
{
    interface IRestaurant
    {
        string Name { get; set; }
        string Location { get; set; }
        string Cuisine { get; set; }
        float AvgRating { get; set; }
        List<Review> Reviews { get; set; }

    }
}
