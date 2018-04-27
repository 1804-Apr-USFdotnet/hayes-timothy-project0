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
            var libModel = new BLL.Models.Restaurant()
            {
                Name = dataModel.Name
            };
            return libModel;
        }

        public static DL.Restaurant LibraryToData(BLL.Models.Restaurant libModel)
        {
            var dataModel = new DL.Restaurant();
            {
                dataModel.Name = libModel.Name;
            };
            return dataModel;
        }
    }
}
