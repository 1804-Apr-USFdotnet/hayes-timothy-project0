﻿using LocalGourmet.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalGourmet.BLL.Models
{
    class Restaurant : IRestaurant
    {
        public string Name { get; set; } 
        public string Location { get; set; }
        public string Cuisine { get; set; }
        public float AvgRating { get; set; }
        public string PhoneNumber { get; set; }
        public string WebAddress { get; set; }
        public List<Review> Reviews { get; set; }
        public string Type { get; set; }
        public string Hours { get; set; }

        public float GetAvgRating()
        {
            throw new NotImplementedException();
        }
    }
}