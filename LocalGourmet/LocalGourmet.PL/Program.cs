using LocalGourmet.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalGourmet.PL
{
    class Program
    {
        static void Main(string[] args)
        {
            Restaurant r = new Restaurant();
            r.Name = "Subway";
            Restaurant r2 = new Restaurant();
            r2.Name = "Burgers";
            List<Restaurant> rList = new List<Restaurant>();
            rList.Add(r);
            rList.Add(r2);
            Console.WriteLine(Serializer.Serialize(rList));
            Console.WriteLine();

            string jsonStr = System.IO.File.ReadAllText(@"C:\revature\" + 
                @"hayes-timothy-project0\LocalGourmet\LocalGourmet.BLL\" +
                @"Models\Restaurants.json");
            List<Restaurant> rList2 = 
                Serializer.Deserialize<List<Restaurant>>(jsonStr);
            Console.WriteLine(rList2[0]);
            Console.WriteLine();
            Console.WriteLine(rList2[1]);
            Console.WriteLine();
            Console.WriteLine(rList2[2]);
            Console.ReadLine();
        }
    }
}
