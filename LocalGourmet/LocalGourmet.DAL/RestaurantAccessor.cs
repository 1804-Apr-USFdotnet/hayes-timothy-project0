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
        public async Task AddRestaurantAsync(DL.Restaurant item)
        {
            using (var db = new LocalGourmetDBEntities())
            {
                db.Restaurants.Add(item);
                await db.SaveChangesAsync();
            }
        }

        // READ
        public IEnumerable<DL.Restaurant> GetRestaurants()
        {
            IEnumerable<DL.Restaurant> dataList;
            using (var db = new LocalGourmetDBEntities())
            {
                dataList = db.Restaurants.ToList();
            }
            return dataList;
        }

        public DL.Restaurant GetRestaurantByID(int id)
        {
            DL.Restaurant r;
            using (var db = new LocalGourmetDBEntities())
            {
                try
                {
                    r = db.Restaurants.Find(id);
                }
                catch
                {
                    throw;
                }
            }
            if(r == null) { throw new ArgumentOutOfRangeException("id"); }
            return r;
        }
        
        // UPDATE
        public async Task UpdateRestaurantAsync(int id, string name)
        {
            DL.Restaurant r;
            try
            {
                using (var db = new LocalGourmetDBEntities())
                {
                    r = db.Restaurants.Find(id);
                    if (r == null) { throw new ArgumentOutOfRangeException("id"); }
                    r.Name = name; 
                    await db.SaveChangesAsync();
                }
            }
            catch
            {
                throw;
            }
        }

        public void UpdateRestaurant(int id, string name)
        {
            DL.Restaurant r;
            try
            {
                using (var db = new LocalGourmetDBEntities())
                {
                    r = db.Restaurants.Find(id);
                    if (r == null) { throw new ArgumentOutOfRangeException("id"); }
                    r.Name = name; 
                    db.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }

        // DELETE


    }
}
