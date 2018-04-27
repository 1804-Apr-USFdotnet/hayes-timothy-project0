using LocalGourmet.BLL.Models;
using LocalGourmet.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalGourmet.DAL
{
    // CRUD class for Restaurant
    public class RestaurantAccessor
    {
        // CREATE
        public async Task AddRestaurantAsync(BLL.Models.Restaurant item)
        {
            using (var db = new LocalGourmetDBEntities())
            {
                db.Restaurants.Add(LibraryToData(item));
                await db.SaveChangesAsync();
            }
        }

        // READ
        public IEnumerable<BLL.Models.Restaurant> GetRestaurants()
        {
            IEnumerable<BLL.Models.Restaurant> result;
            using (var db = new LocalGourmetDBEntities())
            {
                var dataList = db.Restaurants.ToList();
                result = dataList.Select(x => DataToLibrary(x)).ToList();
            }
            return result;
        }

        public static BLL.Models.Restaurant DataToLibrary(DL.Restaurant dataModel)
        {
            int restID = dataModel.ID;
            // Get a list of all reviews where RestaurantID == restID
            // Call ReviewAccessor's DataToLibrary function on those reviews
            // Put them all in a list
            // assign that list to the new Rest. model's review list.


            var libModel = new BLL.Models.Restaurant()
            {
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
