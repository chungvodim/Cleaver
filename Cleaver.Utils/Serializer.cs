using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
