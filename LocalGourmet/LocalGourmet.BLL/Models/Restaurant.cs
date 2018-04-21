using LocalGourmet.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalGourmet.BLL.Models
{
    class Restaurant : IRestaurant
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Location { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Cuisine { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float AvgRating { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string PhoneNumber { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string WebAddress { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<Review> Reviews { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Type { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Hours { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
