using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Cleaver.Utils {
    public static class Extensions {
        public static string ToQueryString(this NameValueCollection collection)
        {
            return ToQueryString(collection, Encoding.UTF8);
        }

        public static string ToQueryString(this NameValueCollection collection, Encoding encoding) {
            var array = (from key in collection.AllKeys
                         from value in collection.GetValues(key)
                         select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value, encoding))).ToArray();
            return string.Join("&", array);
        }

        public static string ToQueryString(this Dictionary<string, object> dictionary)
        {
            return ToQueryString(dictionary, Encoding.UTF8);
        }

        public static string ToQueryString(this Dictionary<string, object> dictionary, Encoding encoding)
        {
            var array = (from key in dictionary.Keys
                         select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(dictionary[key].ToString(), encoding))).ToArray();
            return string.Join("&", array);
        }

        public static string ToQueryString(this object obj)
        {
            return ToQueryString(obj, Encoding.UTF8);
        }

        public static string ToQueryString(this object obj, Encoding encoding) {
            var array = (from kv in obj.ToKeyValuePairs()
                         select string.Format("{0}={1}", HttpUtility.UrlEncode(kv.Key), HttpUtility.UrlEncode(kv.Value, encoding)));
            return string.Join("&", array);
        }

        public static IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs(this object obj) {
            if (obj == null)
                throw new ArgumentNullException("obj");

            if (obj is IDictionary) {
                foreach (DictionaryEntry kv in (IDictionary)obj)
                    yield return new KeyValuePair<string, string>(kv.Key.ToString(), kv.Value.ToString());
            } else {
                foreach (var prop in obj.GetType().GetProperties()) {
                    yield return new KeyValuePair<string, string>(prop.Name, prop.GetValue(obj, null).ToString());
                }
            }
        }

        public static decimal ToDecimal(this string str) {
            var d = default(decimal);
            if (!decimal.TryParse(str, out d)) {
                d = default(decimal);
            }
            return d;
        }
    }


}
