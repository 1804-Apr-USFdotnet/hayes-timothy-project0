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
        // Serialize obj to JSON string and return it.
        public static string Serialize<T>(T obj)
        {
            string jsonStr = "";

            MemoryStream ms = new MemoryStream();
            try
            {
                // Instantiate DataContractJsonSerializer that will serialize to JSON
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                // Uses WriteObject method to serialize the data members to the stream
                ser.WriteObject(ms, obj);
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

        // Deserialize JSON string and return object.
        public static T Deserialize<T>(string jsonStr)
        {
            T obj = default(T);
            MemoryStream ms = new MemoryStream();
            try
            {
                // Instantiate DataContractJsonSerializer that will serialize to JSON
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                StreamWriter writer = new StreamWriter(ms);
                writer.Write(jsonStr);
                writer.Flush();
                obj = (T)ser.ReadObject(ms); // Deserializes JSON data to a  Object
                ms.Position = 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                ms.Close();
            }
            return obj;
        }
    }
}
