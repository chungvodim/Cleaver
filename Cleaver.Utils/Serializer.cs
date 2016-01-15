using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Cleaver.Utils
{
    public class Serializer
    {
        public static string Serialize(object obj)
        {
            JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
            var TheJson = TheSerializer.Serialize(obj);
            return TheJson;
        }

        public static T Deserialize<T>(string input)
        {
            JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
            var TheJson = TheSerializer.Deserialize<T>(input);
            return TheJson;
        }
        public static T DeserializeJsonObject<T>(string content, Encoding encoding)
        {
            using (Stream stream = new MemoryStream())
            {
                byte[] data = encoding.GetBytes(content);
                stream.Write(data, 0, data.Length);
                stream.Position = 0;
                var deserializer = new DataContractJsonSerializer(typeof(T));
                return (T)deserializer.ReadObject(stream);
            }
        }
    }
}
