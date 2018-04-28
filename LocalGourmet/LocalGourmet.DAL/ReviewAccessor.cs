using LocalGourmet.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalGourmet.DAL
{
    public class ReviewAccessor
    {
        // CREATE
        public async Task AddReviewAsync(DL.Review item)
        {
            using (var db = new LocalGourmetDBEntities())
            {
                db.Reviews.Add(item);
                await db.SaveChangesAsync();
            }
        }
    }
}
