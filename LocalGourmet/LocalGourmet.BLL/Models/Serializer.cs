using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace LocalGourmet.BLL.Models
{
    public static class Serializer
    {
        public static string Serialize(Restaurant r)
        {
            string jsonStr = "";

            MemoryStream ms = new MemoryStream();
            try
            {
                // Instantiate DataContractJsonSerializer that will serialize to JSON
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Restaurant));
                // Uses WriteObject method to serialize the data members to the stream
                ser.WriteObject(ms, r);
                ms.Position = 0; // sets position of memory stream
                StreamReader sr = new StreamReader(ms);
                jsonStr = sr.ReadToEnd();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                ms.Close();
            }

            return jsonStr;
        }
    }
}
